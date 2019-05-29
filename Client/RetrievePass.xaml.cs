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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace testcsh
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RetrievePass : Page
    {
        public RetrievePass()
        {
            this.InitializeComponent();
        }

        private async void Btn_retrieve_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (txt_user.Text != "" && txt_email.Text != "")
            {
                server s = new server("1379", "type=userretrieve&user=" + txt_user.Text + "&email=" + txt_email.Text);
                string res = await s.get();
                txt_user.Text = res;
            }
        }
    }
}
