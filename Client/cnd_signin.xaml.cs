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
    public enum cnd_signin_Result { invalid = -1, inactive, Active, WaitForActive, cancle, Signup_clicked }
    public sealed partial class cnd_signin : ContentDialog
    {
        public cnd_signin_Result result { get; private set; }
        public cnd_signin()
        {
            this.InitializeComponent();
        }
        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            txt_username.Focus(FocusState.Programmatic);
        }

        private async void Btn_signin_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_username.Text) && !string.IsNullOrEmpty(txt_password.Password))
            {
                int res = await Account.Signin(txt_username.Text, txt_password.Password);
                switch (res)
                {
                    case -1://√
                        result = cnd_signin_Result.invalid;
                        lbl_Error.Text = "نام کاربری یا رمز عبور اشتباه است";
                        lbl_Error.Visibility = Visibility.Visible;
                        break;
                    case 0://????
                        result = cnd_signin_Result.inactive;
                        lbl_Error.Text = "حساب کاربری غیر فعال است";
                        lbl_Error.Visibility = Visibility.Visible;
                        break;
                    case 1://√
                        result = cnd_signin_Result.Active;
                        this.Hide();
                        break;
                    default://goto activation
                        result = cnd_signin_Result.WaitForActive;
                        this.Hide();
                        break;

                }
            }
            else
            {
                lbl_Error.Text = "نام کاربری یا رمز عبور نمی تواند خالی باشد";
                lbl_Error.Visibility = Visibility.Visible;
            }
        }
        private void Btn_signup_Click(object sender, RoutedEventArgs e)
        {
            this.result = cnd_signin_Result.Signup_clicked;
            this.Hide();
        }
        private void Btn_hide_Click(object sender, RoutedEventArgs e)
        {
            this.result = cnd_signin_Result.cancle;
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
                btn_signin.Focus(FocusState.Keyboard);
        }

    }
}
