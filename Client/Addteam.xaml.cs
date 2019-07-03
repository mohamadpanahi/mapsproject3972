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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace testui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Addteam : Page
    {
        private Player acc;
        private Sport sport = new Sport();
        public Addteam()
        {
            this.InitializeComponent();
            Useful.SetTitlebar(st_titlebar);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            acc = e.Parameter as Player;
        }
        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            lst_league.Width = Frame.ActualWidth / 4;
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txt_captain.Text = acc.Username;
            await sport.initial();
            JsonArray league;
            lst_league.Items.Clear();

            if(acc.sport == PlayerSport.PareshBaVilcher)
            {
                st_counter.Visibility = Visibility.Collapsed;
                st_football.Visibility = Visibility.Collapsed;

                league = sport.Name["clp ba DFAgl"].GetArray();
            }
            else if (acc.sport == PlayerSport.CounterShabake)
            {
                st_counter.Visibility = Visibility.Visible;
                st_football.Visibility = Visibility.Collapsed;

                league = sport.Name["yaCdl pbyE"].GetArray();
            }
            else/* if (acc.sport == PlayerSport.FootbalDastiNabinayan)*/
            {
                st_counter.Visibility = Visibility.Collapsed;
                st_football.Visibility = Visibility.Visible;

                league = sport.Name["wDdbaA jodF CabFCaFaC"].GetArray();
            }

            foreach (var l in league)
                lst_league.Items.Add(Useful.En_Fa(l.GetString()));
        }

        private async void Btn_addteam_Click(object sender, RoutedEventArgs e)
        {
            if (txt_team.Text == "" || txt_captain.Text == "" || (acc.sport == PlayerSport.FootbalDastiNabinayan && txt_fmem1.Text == "") || (acc.sport == PlayerSport.CounterShabake && (txt_cmem1.Text == "" || txt_cmem2.Text == "" || txt_cmem3.Text == "" || txt_cmem4.Text == "")))
            {
                lbl_error.Text = "بی شعور گاو یعنی نمی فهمی باید همه اینا را پر کنی!\nببین با کیا شدیم 80 ملیون!";
                lbl_error.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_error.Visibility = Visibility.Collapsed;

                bool res = false;
                switch (acc.sport)
                {
                    case PlayerSport.PareshBaVilcher:
                        res = await acc.addteam(lst_league.SelectedValue.ToString(), txt_team.Text, txt_captain.Text);
                        break;
                    case PlayerSport.FootbalDastiNabinayan:
                        res = await acc.addteam(lst_league.SelectedValue.ToString(), txt_team.Text, txt_captain.Text,txt_fmem1.Text);
                        break;
                    case PlayerSport.CounterShabake:
                        res = await acc.addteam(lst_league.SelectedValue.ToString(), txt_team.Text, txt_captain.Text, txt_cmem1.Text, txt_cmem2.Text, txt_cmem3.Text, txt_cmem4.Text);
                        break;
                }
                if (res)
                {
                    Frame.Navigate(typeof(intro));
                }
                else
                {
                    lbl_error.Text = "خاک تو سر خرت.\nمگه نگفتم اونورو نگا کن تکراری نباشه.\nاحمق خنده دار بنجل کچل";
                    lbl_error.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
