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

namespace testui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Addcompetition : Page
    {
        private Admin acc;
        private Sport sport = new Sport();
        public Addcompetition()
        {
            this.InitializeComponent();
            Useful.SetTitlebar(st_titlebar);
        }
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            st_sport.Width = Frame.ActualWidth / 4;
            lst_league.Height = st_sport.ActualHeight - cmb_sport.ActualHeight - 24;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            acc = e.Parameter as Admin;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await sport.initial();
            switch (acc.accessabilty)
            {
                case AdminAccessabilty.Administrator:
                    cmb_sport.Items.Add("پرش با ویلچر");
                    cmb_sport.Items.Add("کانتر شبکه");
                    cmb_sport.Items.Add("فوتبال دستی نابینایان");
                    break;
                case AdminAccessabilty.CounterShabake:
                    cmb_sport.Items.Add("کانتر شبکه");
                    cmb_sport.SelectedIndex = 0;
                    break;
                case AdminAccessabilty.FootbalDastiNabinayan:
                    cmb_sport.Items.Add("فوتبال دستی نابینایان");
                    cmb_sport.SelectedIndex = 0;
                    break;
                case AdminAccessabilty.PareshBaVilcher:
                    cmb_sport.Items.Add("پرش با ویلچر");
                    cmb_sport.SelectedIndex = 0;
                    break;
            }
        }

        private void Cmb_sport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lst_league.Items.Clear();

            var jj = sport.Name[Useful.Fa_En(cmb_sport.SelectedValue.ToString())].GetArray();
            foreach (var league in jj)
                lst_league.Items.Add(Useful.En_Fa(league.GetString()));
        }

    }
}
