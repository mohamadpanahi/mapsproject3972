using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace testcsh
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Signup2 : Page
    {
        string[] info;
        public Signup2()
        {
            this.InitializeComponent();
            dat_birth.Date = DateTime.Now;
            st_playerinfo.Visibility = Visibility.Collapsed;

            //set st_titlebar as titlebar
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(st_titlebar);

            //define titlebar color
            ApplicationViewTitleBar formattableTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            formattableTitleBar.ButtonBackgroundColor = Colors.Transparent;
            formattableTitleBar.ButtonHoverBackgroundColor = Color.FromArgb(20, 50, 50, 50);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            info = e.Parameter as string[];
            base.OnNavigatedTo(e);
        }
        private void Btn_back_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack();
        }

        private void Rbtn_player_Checked(object sender, RoutedEventArgs e)
        {
            st_playerinfo.Visibility = Visibility.Visible;
        }

        private void Rbtn_player_Unchecked(object sender, RoutedEventArgs e)
        {
            st_playerinfo.Visibility = Visibility.Collapsed;

        }

        private async void Btn_signup_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            if (txt_name.Text == "")
            {
                ok = false;
                txt_name.Header = "نام و نام خانوادگی باید پر شود";
            }
            else
                txt_name.Header = "";
            if (txt_email.Text == "")
            {
                ok = false;
                txt_email.Header = "رایانامه باید پر شود";
            }
            else
                txt_email.Header = "";
            if (rbtn_player.IsChecked == true)
            {
                if (txt_stature.Text == "")
                {
                    ok = false;
                    txt_stature.Header = "قد باید پر شود";
                }
                else
                    txt_stature.Header = "";
                if (txt_weight.Text == "")
                {
                    ok = false;
                    txt_weight.Header = "وزن باید پر شود";
                }
                else
                    txt_weight.Header = "";
                if (cmb_sport.SelectedIndex == -1)
                {
                    ok = false;
                    cmb_sport.Header = "ورزش باید انتخاب شود";
                }
                else
                    cmb_sport.Header = "";
            }
            if (ok)
            {
                string req = "type=usersignup&user=" + info[0] + "&pass=" + info[1] + "&email=" + txt_email.Text + "&info=";
                req += "$name$:$" + Useful.Fa_En(txt_name.Text) + "$";
                if (txt_phone.Text != "")
                    req += ",$phone$:$" + txt_phone.Text + "$";
                if (txt_code.Text != "")
                    req += ",$code$:$" + txt_code.Text + "$";
                req += ",$gender$:$" + (rbtn_man.IsChecked == true ? "male" : "female") + "$";

                req += ",$isplayer$:" + (rbtn_player.IsChecked == true ? "true" : "false");
                if (rbtn_player.IsChecked == true)
                {
                    req += ",$birth$:$" + dat_birth.Date.Year + " " + dat_birth.Date.Month + " " + dat_birth.Date.Day + "$";
                    req += ",$stature$:$" + txt_stature.Text + "$";
                    req += ",$weight$:$" + txt_weight.Text + "$";
                    req += ",$sport$:$" + Useful.Fa_En(cmb_sport.SelectedValue.ToString()) + "$";
                }

                server s = new server("1379", req);
                string res = await s.get();
                btn_signup.Content = res;
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            st_titlebar.Width = ActualWidth - st_button.ActualWidth;
        }
    }
}
