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
using Windows.Data.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace testui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LeagueResult : Page
    {
        private Sport sport = new Sport();
        public LeagueResult()
        {
            this.InitializeComponent();
            Useful.SetTitlebar(st_titlebar);
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await sport.initial();
            foreach (string sport in sport.Name.Keys)
                cmb_sport.Items.Add(Useful.En_Fa(sport));
        }
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            lst_league.Height = this.ActualHeight - 76;
            st_sport.Width = this.ActualWidth / 4;
        }

        private void Cmb_sport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lst_league.Items.Clear();

            var jj = sport.Name[Useful.Fa_En(cmb_sport.SelectedValue.ToString())].GetArray();
            foreach (var league in jj)
            {
                lst_league.Items.Add(Useful.En_Fa(league.GetString()));
            }
        }

        private void Btn_back_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack();
        }

        private async void Lst_league_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StackPanel[] stacks = await sport.Rank("ts", "tl");
            //StackPanel[] aa = await sport.Rank(Useful.Fa_En(cmb_sport.SelectedValue.ToString()), Useful.Fa_En(lst_league.SelectedValue.ToString()));
            foreach (var st in stacks)
            {
                lst_table.Items.Add(st);
            }
        }
    }
}