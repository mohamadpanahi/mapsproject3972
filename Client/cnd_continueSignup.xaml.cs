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
    public enum cnd_coontinueSignup_Result { cancle, Signup }
    public sealed partial class cnd_continueSignup : ContentDialog
    {
        public string user{get; private set;}
        public string pass{get; private set;}
        public cnd_coontinueSignup_Result result { get; private set; }
        public cnd_continueSignup(string user, string pass)
        {
            this.InitializeComponent();
            this.user = user;
            this.pass = pass;
        }
        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            dat_birth.Date = DateTime.Now;
        }

        private void Btn_hide_Click(object sender, RoutedEventArgs e)
        {
            this.result = cnd_coontinueSignup_Result.cancle;
            this.Hide();
        }

        private void Rbtn_player_Checked(object sender, RoutedEventArgs e)
        {
            st_playerinfo.Visibility = Visibility.Visible;
        }
        private void Rbtn_player_Unchecked(object sender, RoutedEventArgs e)
        {
            st_playerinfo.Visibility = Visibility.Collapsed;
        }

        private void Txt_name_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && !string.IsNullOrEmpty(txt_name.Text))
                txt_email.Focus(FocusState.Keyboard);
        }
        private void Txt_email_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && !string.IsNullOrEmpty(txt_email.Text))
                txt_phone.Focus(FocusState.Keyboard);
        }
        private void Txt_phone_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && !string.IsNullOrEmpty(txt_phone.Text))
                txt_code.Focus(FocusState.Keyboard);
        }
        private void Txt_code_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && !string.IsNullOrEmpty(txt_code.Text))
                rbtn_man.Focus(FocusState.Keyboard);
        }
        private void Txt_stature_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && !string.IsNullOrEmpty(txt_stature.Text))
                txt_weight.Focus(FocusState.Keyboard);
        }
        private void Txt_weight_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && !string.IsNullOrEmpty(txt_weight.Text))
                cmb_sport.Focus(FocusState.Keyboard);
        }

        private void Txt_name_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            bool b = args.Cancel = args.NewText.Any(c => !((char.IsLetter(c) || c == ' ') && (CoreTextServicesManager.GetForCurrentView().InputLanguage.LanguageTag == "fa")));
            if (b)
            {
                lbl_Error.Visibility = Visibility.Visible;
                lbl_Error.Text = "نام باید تنها شامل حروف فارسی باشد";
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
        private void Txt_phone_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsNumber(c));
        }
        private void Txt_code_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsNumber(c));
        }
        private void Txt_stature_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsNumber(c));
        }
        private void Txt_weight_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsNumber(c));
        }

        private async void Btn_signup_Click(object sender, RoutedEventArgs e)
        {
            if (txt_name.Text != "" && txt_email.Text != "" && txt_phone.Text != "" && txt_code.Text != "" && (rbtn_player.IsChecked == false || (txt_stature.Text != "" && txt_weight.Text != "" && cmb_sport.SelectedIndex != -1)))
            {
                if (rbtn_player.IsChecked == true)
                    await Account.Signup(user, pass, txt_email.Text, txt_name.Text, txt_phone.Text, txt_code.Text, rbtn_man.IsChecked == true, dat_birth.Date, txt_stature.Text, txt_weight.Text, cmb_sport.SelectedValue as string);
                else
                    await Account.Signup(user, pass, txt_email.Text, txt_name.Text, txt_phone.Text, txt_code.Text, rbtn_man.IsChecked == true);
                this.result = cnd_coontinueSignup_Result.Signup;
                this.Hide();
            }
            else
            {
                lbl_Error.Text = "لطفا تمامی قسمت ها را پر کنید";
                lbl_Error.Visibility = Visibility.Visible;
            }
        }
    }
}
