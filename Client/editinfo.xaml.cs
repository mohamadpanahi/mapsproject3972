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
    public sealed partial class editinfo : Page
    {
        private User acc;
        
        public editinfo()
        {
            this.InitializeComponent();
            Useful.SetTitlebar(st_titlebar);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            acc = e.Parameter as User;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txt_code.PlaceholderText = txt_code.Text = acc.Code;
            txt_email.PlaceholderText = txt_email.Text = acc.Email;
            txt_name.PlaceholderText = txt_name.Text = acc.Name;
            txt_phone.PlaceholderText = txt_phone.Text = acc.Phone;

            txt_password.Password = txt_confirmpassword.Password = acc.Password;

            if (acc.IsMan) rbtn_man.IsChecked = true;
            else rbtn_woman.IsChecked = true;
        }
        
        private void Txt_name_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && !string.IsNullOrEmpty(txt_name.Text))
                txt_password.Focus(FocusState.Keyboard);
        }
        private void Txt_password_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && !string.IsNullOrEmpty(txt_password.Password))
                txt_confirmpassword.Focus(FocusState.Keyboard);
        }
        private void Txt_confirmpassword_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && !string.IsNullOrEmpty(txt_confirmpassword.Password))
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
        private void Txt_name_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            bool b = false;
            if (args.NewText.Length == 1)
            {
                b = args.Cancel = args.NewText.Any(c => !((char.IsLetter(c) || c == ' ') && (CoreTextServicesManager.GetForCurrentView().InputLanguage.LanguageTag == "fa")));
            }
            if (b)
            {
                lbl_error.Visibility = Visibility.Visible;
                lbl_error.Text = "نام باید تنها شامل حروف فارسی باشد";
            }
            else
                lbl_error.Visibility = Visibility.Collapsed;
        }
        private void Txt_email_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            bool b = args.Cancel = args.NewText.Any(c => !((char.IsLetterOrDigit(c) || c == '.' || c == '@') && (CoreTextServicesManager.GetForCurrentView().InputLanguage.LanguageTag == "en-US")));
            if (b)
            {
                lbl_error.Visibility = Visibility.Visible;
                lbl_error.Text = "رایانامه نا معتبر است";
            }
            else
                lbl_error.Visibility = Visibility.Collapsed;
        }
        private void Txt_phone_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsNumber(c));
        }
        private void Txt_code_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsNumber(c));
        }

        private async void Btn_edit_Click(object sender, RoutedEventArgs e)
        {
            if (txt_name.Text == "" || txt_email.Text == "" || txt_phone.Text == "" || txt_code.Text == "" || txt_password.Password == "" || txt_confirmpassword.Password == "" || txt_password.Password != txt_confirmpassword.Password)
            {
                lbl_error.Text = "بی شعور گاو یعنی نمی فهمی باید همه اینا را پر کنی!\nببین با کیا شدیم 80 ملیون!";
                lbl_error.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_error.Visibility = Visibility.Collapsed;

                bool res = await acc.edit(acc.Username, acc.Password, txt_name.Text, txt_email.Text, txt_code.Text, txt_phone.Text, (bool)rbtn_man.IsChecked, txt_password.Password);
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
