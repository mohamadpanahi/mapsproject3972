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
using Windows.UI.Text.Core;

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

            Useful.SetTitlebar(st_titlebar);
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(st_main.ActualWidth + 24, st_main.ActualHeight + 32));

            ApplicationView.PreferredLaunchViewSize = new Size(st_main.ActualWidth + 24, st_main.ActualHeight + 32);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.Auto;

            txt_user.Focus(FocusState.Programmatic);
            if (!this.Frame.CanGoBack)
                btn_back.Visibility = Visibility.Collapsed;
        }
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            st_titlebar.Width = ActualWidth - st_button.ActualWidth;
        }

        private async void Btn_signin_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (txt_user.Text != "" && txt_pass.Password != "")
            {
                server s = new server("1379", "type=usersignin&user=" + txt_user.Text + "&pass=" + txt_pass.Password);
                string res = await s.get();
                if (res == "1")
                    Window.Current.CoreWindow.Close();
                else
                {
                    lbl_error.Visibility = Visibility.Visible;
                    lbl_error.Text = "😝نام کاربری یا رمز عبور اشتباه است";
                }                
            }
            else
            {
                lbl_error.Visibility = Visibility.Visible;
                lbl_error.Text = "نام کابری و رمز عبور نمی تواند خالی باشد";
            }
            
        }
        private void Btn_back_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack();
        }
        private void Btn_retrieve_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RetrievePass));
        }
        private void Btn_activeaccount_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ActiveAccount));
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
        private void Txt_user_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            bool b = args.Cancel = args.NewText.Any(c => !((char.IsLetterOrDigit(c) || c == '.' || c == '_') && (CoreTextServicesManager.GetForCurrentView().InputLanguage.LanguageTag == "en-US")));
            if (b)
            {
                lbl_error.Visibility = Visibility.Visible;
                lbl_error.Text = "نام کاربری باید شامل حروف a-z , اعداد 0-9 و نقطه باشد";
            }
            else
                lbl_error.Visibility = Visibility.Collapsed;
        }
    }
}
