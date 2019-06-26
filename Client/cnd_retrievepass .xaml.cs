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
    public enum cnd_retrievepass_Result { cancle, retrieve }
    public sealed partial class cnd_retrievepass : ContentDialog
    {
        public cnd_retrievepass_Result result { get; private set; }
        public cnd_retrievepass(string user)
        {
            this.InitializeComponent();
            this.txt_username.Text = user;
        }

        private void Btn_hide_Click(object sender, RoutedEventArgs e)
        {
            this.result = cnd_retrievepass_Result.cancle;
            this.Hide();
        }
        private async void Btn_retrievepass_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_username.Text) || string.IsNullOrEmpty(txt_email.Text))
            {
                lbl_Error.Text = "لطفا تمامی قسمت ها را تکمیل کنید";
                lbl_Error.Visibility = Visibility.Visible;
            }
            else if (await Account.Retrievepass(txt_username.Text, txt_email.Text))
            {
                this.Hide();
                result = cnd_retrievepass_Result.retrieve;
            }
            else
            {
                lbl_Error.Text = "نام کاربری یا رایانامه اشتباه است";
                lbl_Error.Visibility = Visibility.Visible;
            }
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
        private void Txt_email_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            bool b = args.Cancel = args.NewText.Any(c => !((char.IsLetterOrDigit(c) || c == '.' || c == '@') && (CoreTextServicesManager.GetForCurrentView().InputLanguage.LanguageTag == "en-US")));
            if (b)
            {
                lbl_Error.Visibility = Visibility.Visible;
                lbl_Error.Text = "رایانامه نا معتبر است";
            }
            else
                lbl_Error.Visibility = Visibility.Collapsed;
        }
        private void Txt_username_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && !string.IsNullOrEmpty(txt_username.Text))
                txt_email.Focus(FocusState.Keyboard);
        }
        private void Txt_email_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && !string.IsNullOrEmpty(txt_email.Text))
                btn_retrievepass.Focus(FocusState.Keyboard);
        }
    }
}
