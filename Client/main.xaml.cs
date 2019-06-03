﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
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
    public sealed partial class main : Page
    {
        public main()
        {
            this.InitializeComponent();
            Useful.SetTitlebar(st_titlebar);
        }

        private async void Btn_signin_Click(object sender, RoutedEventArgs e)
        {
            int newviewid = 0;
            var currentAV = ApplicationView.GetForCurrentView();
            var newAV = CoreApplication.CreateNewView();
            await newAV.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                            () =>
                            {
                                var frame = new Frame();
                                frame.Navigate(typeof(Signin));
                                Window.Current.Content = frame;
                                Window.Current.Activate();

                                newviewid = ApplicationView.GetForCurrentView().Id;
                            });
            //IsHitTestVisible = false;
            bool shown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newviewid);
        }
        private void Btn_signup_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Signup1));
        }
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            st_titlebar.Width = ActualWidth;
        }
    }
}
