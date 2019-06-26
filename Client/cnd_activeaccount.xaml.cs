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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace testui
{
    public enum cnd_activeAccount_Result { Cancle, SendCode, Active }

    public sealed partial class cnd_ActiveAccount : ContentDialog
    {
        public string user { get; private set; }
        public string pass { get; private set; }
        public cnd_activeAccount_Result result { get; private set; }
        public cnd_ActiveAccount(string user, string pass)
        {
            this.InitializeComponent();
            txt_username.Text = this.user = user;
            txt_password.Password = this.pass = pass;
        }
        private void ContentDialog_Closed(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
            this.user = txt_username.Text;
            this.pass = txt_password.Password;
        }

        private async void Btn_sendcode_Click(object sender, RoutedEventArgs e)
        {
            if (txt_username.Text == "" || txt_password.Password == "")
            {
                lbl_Error.Text = "نام کاربری یا رمز عبور نمی تواند خالی باشد";
                lbl_Error.Visibility = Visibility.Visible;
            }
            else switch (await Account.Signin(txt_username.Text, txt_password.Password))
            {
                case -1:
                    lbl_Error.Text = "نام کاربری یا رمز عبور اشتباه است";
                    lbl_Error.Visibility = Visibility.Visible;
                    break;
                case 0:
                    result = cnd_activeAccount_Result.SendCode;
                    await Account.Generatecode(txt_username.Text, txt_password.Password);
                    this.Hide();
                    break;
                case 1:
                    result = cnd_activeAccount_Result.Active;
                    this.Hide();
                    break;
                default:
                    result = cnd_activeAccount_Result.SendCode;
                    this.Hide();
                    break;
            }
        }
        private void Btn_hide_Click(object sender, RoutedEventArgs e)
        {
            this.result = cnd_activeAccount_Result.Cancle;
            this.Hide();
        }

        private void Txt_username_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            bool b = args.Cancel = args.NewText.Any(c => !((char.IsLetterOrDigit(c) || c == '.' || c == '_') && (CoreTextServicesManager.GetForCurrentView().InputLanguage.LanguageTag == "en-US")));
            if (b)
            {
                lbl_Error.Visibility = Visibility.Visible;
                lbl_Error.Text = "نام کاربری باید شامل حروف a-z , اعداد 0-9 و نقطه باشد";
            }
            else
                lbl_Error.Visibility = Visibility.Collapsed;
        }
        private void Txt_username_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && !string.IsNullOrEmpty(txt_username.Text))
                txt_password.Focus(FocusState.Keyboard);
        }
        private void Txt_password_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && !string.IsNullOrEmpty(txt_password.Password))
                btn_sendcode.Focus(FocusState.Keyboard);
        }
    }
}
