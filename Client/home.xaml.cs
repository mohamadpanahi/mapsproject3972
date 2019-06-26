using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
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
                Account acc = await Account.GetUserInfo(signin.user, signin.pass);
                if (acc.GetType() == typeof(Admin))
                    Frame.Navigate(typeof(pnl_Admin), acc);
            }
            else if (signin.result == cnd_signin_Result.inactive || signin.result==cnd_signin_Result.WaitForActive)
                cnd_activeCode_show(signin.user, signin.pass);
            else if (signin.result == cnd_signin_Result.Signup_clicked)
                cnd_signup_show();
            else if (signin.result == cnd_signin_Result.Retrieve_clicked)
                cnd_retrievepass_show(signin.user);
            else if (signin.result == cnd_signin_Result.ActiveAccount_clicked)
                cnd_activeAccount_show(signin.user, signin.pass);
        }
        public async void cnd_signup_show()
        {
            var signup = new cnd_signup();
            await signup.ShowAsync();
            
            if (signup.result == cnd_signup_Result.ContinueSignup)
                cnd_continueSignup_show(signup.user, signup.pass);
        }
        public async void cnd_continueSignup_show(string user, string pass)
        {
            var signup2 = new cnd_continueSignup(user, pass);
            await signup2.ShowAsync();

            if (signup2.result == cnd_coontinueSignup_Result.Signup)
                cnd_activeCode_show(user, pass);
        }
        public async void cnd_retrievepass_show(string user)
        {
            var rp = new cnd_retrievepass(user);
            await rp.ShowAsync();

            if (rp.result == cnd_retrievepass_Result.retrieve)
                btn_signin.Content = "retrieve";
        }
        public async void cnd_activeAccount_show(string user, string pass)
        {
            var aa = new cnd_ActiveAccount(user, pass);
            await aa.ShowAsync();

            if (aa.result == cnd_activeAccount_Result.SendCode)
                cnd_activeCode_show(aa.user, aa.pass);
            else if(aa.result==cnd_activeAccount_Result.Active)
                btn_signin.Label = "Welcome";
            //incomplete -> goto panel
        }
        public async void cnd_activeCode_show(string user, string pass)
        {
            var ac = new cnd_activeCode(user, pass);
            await ac.ShowAsync();

            if (ac.result == cnd_activeCode_result.active)
                btn_signin.Label = "Welcome";
                //incomplete -> goto panel
        }
        public home()
        {
            this.InitializeComponent();
            Useful.SetTitlebar(st_titlebar);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            st_titlebar.Width = this.ActualWidth - st_button.ActualWidth;
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
