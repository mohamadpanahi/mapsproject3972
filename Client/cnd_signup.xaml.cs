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
    public enum cnd_signup_Result { cancle, ContinueSignup }
    public sealed partial class cnd_signup : ContentDialog
    {
        public cnd_signup_Result result { get; private set; }
        public cnd_signup()
        {
            this.InitializeComponent();
        }
        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            txt_username.Focus(FocusState.Programmatic);
        }

        private void Btn_hide_Click(object sender, RoutedEventArgs e)
        {
            this.result = cnd_signup_Result.cancle;
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
                txt_confirmpass.Focus(FocusState.Keyboard);
        }

        private void Txt_confirmpass_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && !string.IsNullOrEmpty(txt_password.Password))
                btn_continue.Focus(FocusState.Keyboard);
        }

        private async void Btn_continue_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_username.Text) || string.IsNullOrEmpty(txt_password.Password) || string.IsNullOrEmpty(txt_confirmpass.Password))
            {
                lbl_Error.Visibility = Visibility.Visible;
                lbl_Error.Text = "لطفا تمامی قسمت ها را پر کنید";
            }
            else if (txt_password.Password != txt_confirmpass.Password)
            {
                lbl_Error.Visibility = Visibility.Visible;
                lbl_Error.Text = "رمز عبور با تکرار آن مطابقت ندارد";
                txt_password.Password = txt_confirmpass.Password = "";
            }
            else if (await Account.Checkusername(txt_username.Text))
            {
                lbl_Error.Visibility = Visibility.Visible;
                lbl_Error.Text = "این نام کاربری قبلا استفاده شده است";
            }
            else
            {
                result = cnd_signup_Result.ContinueSignup;
                this.Hide();
            }
        }
    }
}
