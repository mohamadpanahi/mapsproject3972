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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace testui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class home : Page
    {
        public async void cnd_signin_show()
        {
            var signin = new cnd_signin();
            await signin.ShowAsync();

            if (signin.result == cnd_signin_Result.Active)
            {

            }
            else if (signin.result == cnd_signin_Result.Signup_clicked)
                cnd_signup_show();

        }
        public async void cnd_signup_show()
        {
            var signup = new cnd_signup();
            await signup.ShowAsync();

            if (signup.result == cnd_signup_Result.ContinueSignup)
                cnd_continueSignup_show();
        }
        public async void cnd_continueSignup_show()
        {
            var signup2 = new cnd_continueSignup();
            await signup2.ShowAsync();




        }
        public home()
        {
            this.InitializeComponent();
            Useful.SetTitlebar(st_titlebar);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            st_titlebar.Width = this.ActualWidth;
        }

        private void Btn_signin_Click(object sender, RoutedEventArgs e)
        {
            cnd_signin_show();
        }
        private void Btn_signup_Click(object sender, RoutedEventArgs e)
        {
            cnd_signup_show();
        }
    }
}
