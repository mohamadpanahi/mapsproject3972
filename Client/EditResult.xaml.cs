using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace testui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditResult : Page
    {
        private Admin acc;
        private Sport sport = new Sport();
        private JsonObject leagueinfo;
        public EditResult()
        {
            this.InitializeComponent();
            Useful.SetTitlebar(st_titlebar);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            acc = e.Parameter as Admin;
        }
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            st_sport.Width = Frame.ActualWidth / 4;

            lst_league.Height = st_sport.ActualHeight - cmb_sport.ActualHeight - 24;
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            st_pareshbavilcher.Visibility = Visibility.Collapsed;
            st_counter.Visibility = Visibility.Collapsed;

            await sport.initial();
            switch (acc.accessabilty)
            {
                case AdminAccessabilty.Administrator:
                    cmb_sport.Items.Add("پرش با ویلچر");
                    cmb_sport.Items.Add("کانتر شبکه");
                    cmb_sport.Items.Add("فوتبال دستی نابینایان");
                    break;
                case AdminAccessabilty.CounterShabake:
                    cmb_sport.Items.Add("کانتر شبکه");
                    cmb_sport.SelectedIndex = 0;
                    break;
                case AdminAccessabilty.FootbalDastiNabinayan:
                    cmb_sport.Items.Add("فوتبال دستی نابینایان");
                    cmb_sport.SelectedIndex = 0;
                    break;
                case AdminAccessabilty.PareshBaVilcher:
                    cmb_sport.Items.Add("پرش با ویلچر");
                    cmb_sport.SelectedIndex = 0;
                    break;
            }
        }

        private void Cmb_sport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            st_pareshbavilcher.Visibility = Visibility.Collapsed;
            st_counter.Visibility = Visibility.Collapsed;
            if (cmb_sport.SelectedItem.ToString() == "پرش با ویلچر")
                st_pareshbavilcher.Visibility = Visibility.Visible;
            else if (cmb_sport.SelectedItem.ToString() == "کانتر شبکه")
                st_counter.Visibility = Visibility.Visible;

            lst_league.Items.Clear();

            var jj = sport.Name[Useful.Fa_En(cmb_sport.SelectedValue.ToString())].GetArray();
            foreach (var league in jj)
                lst_league.Items.Add(Useful.En_Fa(league.GetString()));
        }
        private async void Lst_league_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbl_error.Visibility = Visibility.Collapsed;
            if (lst_league.SelectedIndex == -1) return;
            try
            {
                leagueinfo = await sport.leagueinfo(cmb_sport.SelectedItem.ToString(), lst_league.SelectedItem.ToString());
                JsonArray teams = leagueinfo["rank"].GetArray();

                cmb_t1.Items.Clear();
                int n = teams.Count;
                for (int i = 0; i < n; i++)
                    cmb_t1.Items.Add(Useful.En_Fa(teams.GetStringAt(Convert.ToUInt32(i))));
            }
            catch
            {
                lbl_error.Text = "هیچ مسابقه ای وجود ندارد";
                lbl_error.Visibility = Visibility.Visible;
            }
        }
        private void Cmb_t1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmb_t2.Items.Clear();
            for (int i = 0; i < cmb_t1.Items.Count; i++)
                if (i != cmb_t1.SelectedIndex)
                    cmb_t2.Items.Add(cmb_t1.Items[i]);
        }
        private void Cmb_t2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbl_error.Visibility = Visibility.Collapsed;
            txt_r1.Text = txt_r2.Text = txt_wk1.Text = txt_wk2.Text = txt_fault1.Text = txt_fault2.Text = "";
            if (cmb_t2.SelectedIndex == -1) return;
            try
            {
                JsonObject competition = leagueinfo["competition"].GetObject()[Useful.Fa_En(cmb_t1.SelectedValue.ToString()) + '-' + Useful.Fa_En(cmb_t2.SelectedValue.ToString())].GetObject();
                //fill date & time
                JsonObject temp = competition["date"].GetObject();
                date.Date = new DateTimeOffset((int)temp["y"].GetNumber(), (int)temp["m"].GetNumber(), (int)temp["d"].GetNumber(), 0, 0, 0, new TimeSpan());

                temp = competition["time"].GetObject();
                time.Time = new TimeSpan((int)temp["h"].GetNumber(), (int)temp["m"].GetNumber(), 0);

                txt_place.Text = Useful.En_Fa(competition["place"].GetString());

                //fill textboxes
                try
                {
                    bool isdash = false;
                    foreach (var i in competition["result"].GetString())
                    {
                        if (i == '-')
                        {
                            isdash = true;
                            continue;
                        }
                        if (isdash)
                            txt_r2.Text += i;
                        else
                            txt_r1.Text += i;
                    }
                }
                catch
                {
                    txt_r1.Text = txt_r2.Text = "0";
                }
                if (cmb_sport.SelectedItem.ToString() == "پرش با ویلچر")
                {
                    txt_fault1.Text = competition["t1"].GetObject()["fault"].GetNumber().ToString();
                    txt_fault2.Text = competition["t2"].GetObject()["fault"].GetNumber().ToString();
                }
                else if (cmb_sport.SelectedItem.ToString() == "کانتر شبکه")
                {
                    txt_wk1.Text = competition["t1"].GetObject()["wk"].GetNumber().ToString();
                    txt_wk2.Text = competition["t2"].GetObject()["wk"].GetNumber().ToString();
                }
                //check time
                if (new DateTimeOffset(date.Date.Year, date.Date.Month, date.Date.Day, time.Time.Hours, time.Time.Minutes, 0, new TimeSpan()) > DateTimeOffset.Now)
                {
                    btn_edit.IsEnabled = false;
                    lbl_error.Text = "مسابقه مورد نظر هنوز شروع نشده است!";
                    lbl_error.Visibility = Visibility.Visible;
                }
                else
                {
                    btn_edit.IsEnabled = true;
                }
            }
            catch
            {
                lbl_error.Text = "مسابقه مورد نظر وجود ندارد";
                lbl_error.Visibility = Visibility.Visible;
            }
        }

        private async void Btn_edit_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_t1.SelectedIndex == -1 || cmb_t2.SelectedIndex == -1 || txt_r1.Text == "" || txt_r2.Text == "" || (st_counter.Visibility == Visibility.Visible && (txt_wk1.Text == "" || txt_wk2.Text == "")) || (st_pareshbavilcher.Visibility == Visibility.Visible && (txt_fault1.Text == "" || txt_fault2.Text == ""))) 
            {
                lbl_error.Text = "بی شعور گاو یعنی نمی فهمی باید همه اینا را پر کنی!\nببین با کیا شدیم 80 ملیون!";
                lbl_error.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_error.Visibility = Visibility.Collapsed;

                string info = "";
                if (cmb_sport.SelectedItem.ToString() == "پرش با ویلچر")
                    info = $"$t1$:{"{"}$fault$:{txt_fault1.Text}{"}"}, $t2$:{"{"}$fault$:{txt_fault2.Text}{"}"}";
                else if (cmb_sport.SelectedItem.ToString() == "کانتر شبکه")
                    info = $"$t1$:{"{"}$wk$:{txt_wk1.Text},$lk$:{txt_wk2.Text}{"}"}, $t2$:{"{"}$wk$:{txt_wk2.Text},$lk$:{txt_wk1.Text}{"}"}";
                else if (cmb_sport.SelectedItem.ToString() == "فوتبال دستی نابینایان")
                    info = $"$t1$:{"{"}$wg$:{txt_r1.Text},$lg$:{txt_r2.Text}{"}"}, $t2$:{"{"}$wg$:{txt_r2.Text},$lg$:{txt_r1.Text}{"}"}";

                bool res = await sport.editresultcompetition(cmb_sport.SelectedValue.ToString(), lst_league.SelectedValue.ToString(), cmb_t1.SelectedValue.ToString(), cmb_t2.SelectedValue.ToString(), txt_r1.Text + "-" + txt_r2.Text, info);
                if (res)
                {
                    Frame.Navigate(typeof(intro));
                }
                else
                {
                    lbl_error.Text = "خاک تو سر خرت.\nمگه نگفتم اونورو نگا کن تکراری نباشه.\nاحمق خنده دار بنجل کچل";
                    lbl_error.Visibility = Visibility.Visible;
                }
            }
        }

        private void Txt_r1_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsNumber(c));
        }
        private void Txt_r2_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsNumber(c));
        }
        private void Txt_fault1_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsNumber(c));
        }
        private void Txt_fault2_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsNumber(c));
        }
        private void Txt_wk1_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsNumber(c));
        }
        private void Txt_wk2_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsNumber(c));
        }
    }
}
