using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.ApplicationModel.Core;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace testcsh
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Signin : Page
    {
        public Signin()
        {
            this.InitializeComponent();
            /*st_txt.Width = this.ActualWidth / 2;
            st_main.Width = st_into.Width = st_lbl.Width + st_txt.Width + 12;

            st_into.Height = st_lbl.Height = st_txt.Height = txt_user.Height + txt_pass.Height + 12;
            st_main.Height = st_into.Height + btn_signin.Height + 12;*/

            //set st_titlebar as titlebar
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(st_titlebar);
            //define titlebar color
            ApplicationViewTitleBar formattableTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            formattableTitleBar.ButtonBackgroundColor = Colors.Transparent;
            formattableTitleBar.ButtonHoverBackgroundColor = Color.FromArgb(20, 50, 50, 50);
            
        }
        

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var page = ApplicationView.GetForCurrentView();
            page.SetPreferredMinSize(new Size(500, 300));
        }

        private async void Btn_signin_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (txt_user.Text != "" && txt_pass.Password != "")
            {
                server s = new server("1379", "type=usersignin&user=" + txt_user.Text + "&pass=" + txt_pass.Password);
                string res = await s.get();
                if (res == "1")
                    txt_user.Text = ":) welcome";
                else if (res == "-1")
                    txt_user.Text = ":(";
                else if (res == "0")
                    txt_user.Text = ":| inactive";
                else
                    txt_user.Text = ":O enter code in appropriate place";
            }
        }

        private void Btn_retrieve_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RetrievePass));
        }

        private void Btn_activeaccount_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RetrieveAccount));
        }

        private void Btn_back_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack();
        }

        private void Btn_signup_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Signup1));
        }

        private void Btn_activecode_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ActiveAccount));
        }

        private void Txt_user_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
                txt_pass.Focus(FocusState.Keyboard);
        }

        private void Txt_pass_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Btn_signin_ClickAsync(txt_pass, e);
            }
        }

        private void St_main_Loaded(object sender, RoutedEventArgs e)
        {
            txt_user.Focus(FocusState.Programmatic);
        }
    }
}
