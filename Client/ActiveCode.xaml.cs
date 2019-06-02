using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
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
    public sealed partial class ActiveCode : Page
    {
        string[] info;
        public ActiveCode()
        {
            this.InitializeComponent();

            //set st_titlebar as titlebar
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(st_titlebar);

            //define titlebar color
            ApplicationViewTitleBar formattableTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            formattableTitleBar.ButtonBackgroundColor = Colors.Transparent;
            formattableTitleBar.ButtonHoverBackgroundColor = Color.FromArgb(20, 50, 50, 50);
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            info = e.Parameter as string[];
            base.OnNavigatedTo(e);
        }

        private void Btn_back_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack();
        }

        private async void Btn_active_Click(object sender, RoutedEventArgs e)
        {
            server s = new server("1379", "type=useractive&user=" + info[0] + "&pass=" + info[1] + "&code=" + txt_code.Text);
            string res = await s.get();
            if (res == "1") txt_code.Text = "Activated";
            else txt_code.Text = "Invalid code";
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            st_titlebar.Width = ActualWidth - st_button.ActualWidth;
        }
    }
}
