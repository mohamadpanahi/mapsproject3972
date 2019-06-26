using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace testui
{
    public enum cnd_activeCode_result { cansle, active}
    public sealed partial class cnd_activeCode : ContentDialog
    {
        private string user, pass;
        public cnd_activeCode_result result { get; private set; }
        public cnd_activeCode(string user, string pass)
        {
            this.InitializeComponent();
            this.user = user;
            this.pass = pass;
        }

        private void Txt_code_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsNumber(c));
        }
        private void Txt_code_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (txt_code.Text.Length == 6 && e.Key == Windows.System.VirtualKey.Enter)
                btn_active.Focus(FocusState.Keyboard);
        }

        private void Btn_hide_Click(object sender, RoutedEventArgs e)
        {
            this.result = cnd_activeCode_result.cansle;
            this.Hide();
        }
        private async void Btn_generate_Click(object sender, RoutedEventArgs e)
        {
            await Account.Generatecode(user, pass);
            lbl_Error.Text = "کد فعال سازی ارسال شد";
            lbl_Error.Visibility = Visibility.Visible;
        }
        private async void Btn_active_Click(object sender, RoutedEventArgs e)
        {
            if(txt_code.Text=="")
            {
                lbl_Error.Text = "کد فعال سازی را وارد کنید";
                lbl_Error.Visibility = Visibility.Visible;
            }
            if( await Account.Activeaccount(user, pass, txt_code.Text))
            {
                result = cnd_activeCode_result.active;
                this.Hide();
            }
            else
            {
                lbl_Error.Text = "کد فعال سازی اشتباه است";
                lbl_Error.Visibility = Visibility.Visible;
            }
        }
    }
}
