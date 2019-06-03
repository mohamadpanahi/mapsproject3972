﻿using System;
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
    public sealed partial class ActiveAccount : Page
    {
        public ActiveAccount()
        {
            this.InitializeComponent();

            Useful.SetTitlebar(st_titlebar);
        }

        private async void Btn_send_Click(object sender, RoutedEventArgs e)
        {
            if (txt_user.Text != "" && txt_pass.Password != "")
            {
                //incomplate class -> 
                bool result = await Account.Generatecode(txt_user.Text, txt_pass.Password);
                string[] info = { txt_user.Text, txt_pass.Password };
                
                if (result)
                    this.Frame.Navigate(typeof(ActiveCode), info);
            }
        }

        private void Btn_back_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            st_titlebar.Width = ActualWidth - st_button.ActualWidth;
        }
    }
}
