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
    public sealed partial class DeleteCompetition : Page
    {
        private Admin acc;
        private Sport sport = new Sport();
        private JsonObject leagueinfo;
        public DeleteCompetition()
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
            lst_league.Items.Clear();

            var jj = sport.Name[Useful.Fa_En(cmb_sport.SelectedValue.ToString())].GetArray();
            foreach (var league in jj)
                lst_league.Items.Add(Useful.En_Fa(league.GetString()));
        }
        private async void Lst_league_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            leagueinfo = await sport.leagueinfo(cmb_sport.SelectedItem.ToString(), lst_league.SelectedItem.ToString());
            JsonArray teams = leagueinfo["rank"].GetArray();

            cmb_t1.Items.Clear();
            int n = teams.Count;
            for (int i = 0; i < n; i++)
                cmb_t1.Items.Add(teams.GetStringAt(Convert.ToUInt32(i)));
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
            try
            {
                lbl_error.Visibility = Visibility.Collapsed;
                JsonObject competition = leagueinfo["competition"].GetObject()[cmb_t1.SelectedValue.ToString() + '-' + cmb_t2.SelectedValue.ToString()].GetObject();

                JsonObject temp = competition["date"].GetObject();
                date.Date = new DateTimeOffset((int)temp["y"].GetNumber(), (int)temp["m"].GetNumber(), (int)temp["d"].GetNumber(), 0, 0, 0, new TimeSpan());

                temp = competition["time"].GetObject();
                time.Time = new TimeSpan((int)temp["h"].GetNumber(), (int)temp["m"].GetNumber(), 0);

                txt_place.Text = Useful.En_Fa(competition["place"].GetString());
            }
            catch
            {
                lbl_error.Text = "مسابقه مورد نظر وجود ندارد";
                lbl_error.Visibility = Visibility.Visible;
            }
        }

        private async void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_t1.SelectedIndex == -1 || cmb_t2.SelectedIndex == -1)
            {
                lbl_error.Text = "بی شعور گاو یعنی نمی فهمی باید همه اینا را پر کنی!\nببین با کیا شدیم 80 ملیون!";
                lbl_error.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_error.Visibility = Visibility.Collapsed;

                bool res = await sport.deletecompetition(cmb_sport.SelectedValue.ToString(), lst_league.SelectedValue.ToString(), cmb_t1.SelectedValue.ToString(), cmb_t2.SelectedValue.ToString());
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
    }
}
