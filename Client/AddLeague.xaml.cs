using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text.Core;
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
    public sealed partial class AddLeague : Page
    {
        private Admin acc;
        private Sport sport = new Sport();
        public AddLeague()
        {
            this.InitializeComponent();
            Useful.SetTitlebar(st_titlebar);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            lst_league.Width = Frame.ActualWidth / 4;
            st.Width = Frame.ActualWidth / 4;
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
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            acc = e.Parameter as Admin;
        }

        private void Cmb_sport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lst_league.Items.Clear();

            var jj = sport.Name[Useful.Fa_En(cmb_sport.SelectedValue.ToString())].GetArray();
            foreach (var league in jj)
                lst_league.Items.Add(Useful.En_Fa(league.GetString()));
        }
        private async void Btn_addleague_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_sport.SelectedIndex == -1 || txt_league.Text == "" || txt_teamnum.Text == "")
            {
                lbl_error.Text = "بی شعور گاو یعنی نمی فهمی باید همه اینا را پر کنی!\nببین با کیا شدیم 80 ملیون!";
                lbl_error.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_error.Visibility = Visibility.Collapsed;

                bool res = await sport.addLeague(cmb_sport.SelectedValue.ToString(), txt_league.Text, Convert.ToInt32(txt_teamnum.Text));
                if (!res)
                {
                    lbl_error.Text = "خاک تو سر خرت.\nمگه نگفتم اونورو نگا کن تکراری نباشه.\nاحمق خنده دار بنجل کچل";
                    lbl_error.Visibility = Visibility.Visible;
                }
                else
                {
                    Frame.Navigate(typeof(intro));
                }
            }
        }

        private void Txt_league_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            bool b = args.Cancel = args.NewText.Any(c => !((char.IsLetter(c) || c == ' ') && (CoreTextServicesManager.GetForCurrentView().InputLanguage.LanguageTag == "fa")));
            if (b)
            {
                lbl_error.Visibility = Visibility.Visible;
                lbl_error.Text = "نام لیگ باید تنها شامل حروف فارسی باشد";
            }
            else
                lbl_error.Visibility = Visibility.Collapsed;
        }
        private void Txt_teamnum_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !(char.IsNumber(c)));
        }
        private void Txt_league_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
                txt_teamnum.Focus(FocusState.Keyboard);
        }
        private void Txt_teamnum_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
                btn_addleague.Focus(FocusState.Keyboard);
        }
    }
}
