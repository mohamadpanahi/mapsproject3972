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
    public enum cnd_coontinueSignup_Result { cancle, ContinueSignup }
    public sealed partial class cnd_continueSignup : ContentDialog
    {
        public cnd_coontinueSignup_Result result { get; private set; }
        public cnd_continueSignup()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
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
    }
}
