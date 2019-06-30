using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class DeleteLeague : Page
    {
        private Admin acc;
        private Sport sport = new Sport();
        public DeleteLeague()
        {
            this.InitializeComponent();
            Useful.SetTitlebar(st_titlebar);
        }

        private void Cmb_sport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lst_league.Items.Clear();

            var jj = sport.Name[Useful.Fa_En(cmb_sport.SelectedValue.ToString())].GetObject();
            foreach (var league in jj)
            {
                StackPanel stack = new StackPanel { Orientation = Orientation.Horizontal };
                stack.Children.Add(new SymbolIcon { Symbol = league.Value.GetBoolean() ? Symbol.Play : Symbol.Pause });
                stack.Children.Add(new TextBlock { Text = Useful.En_Fa(league.Key), Margin = new Thickness(24, 0, 0, 0) });

                lst_league.Items.Add(stack);
            }
        }

        private async void Btn_activeleague_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_sport.SelectedIndex == -1)
            {
                lbl_error.Text = "بی شعور گاو یعنی نمی فهمی باید همه اینا را پر کنی!\nببین با کیا شدیم 80 ملیون!";
                lbl_error.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_error.Visibility = Visibility.Collapsed;

                bool res = await sport.activeleague(Useful.Fa_En(cmb_sport.SelectedValue.ToString()), ((lst_league.SelectedItem as StackPanel).Children.Last() as TextBlock).Text);
                if (!res)
                {
                    lbl_error.Text = "هههههههههه";
                    lbl_error.Visibility = Visibility.Visible;
                }
                else
                {
                    lst_league.Focus(FocusState.Programmatic);
                    await sport.initial2();
                    Cmb_sport_SelectionChanged(cmb_sport, null);
                }
            }
        }
        private async void Btn_deleteleague_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_sport.SelectedIndex == -1)
            {
                lbl_error.Text = "بی شعور گاو یعنی نمی فهمی باید همه اینا را پر کنی!\nببین با کیا شدیم 80 ملیون!";
                lbl_error.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_error.Visibility = Visibility.Collapsed;

                bool res = await sport.inactiveleague(Useful.Fa_En(cmb_sport.SelectedValue.ToString()), ((lst_league.SelectedItem as StackPanel).Children.Last() as TextBlock).Text);
                if (!res)
                {
                    lbl_error.Text = "هههههههههه";
                    lbl_error.Visibility = Visibility.Visible;
                }
                else
                {
                    btn_deleteleague.IsEnabled = false;
                    btn_activeleague.IsEnabled = false;
                    await sport.initial2();
                    Cmb_sport_SelectionChanged(cmb_sport, null);
                }
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            lst_league.Width = Frame.ActualWidth / 4;
            st.Width = Frame.ActualWidth / 4;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            acc = e.Parameter as Admin;
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await sport.initial2();
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

        private void Lst_league_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lst_league.SelectedItem != null)
            {
                var jj = sport.Name[Useful.Fa_En(cmb_sport.SelectedValue.ToString())].GetObject();
                if (jj[Useful.Fa_En(((lst_league.SelectedItem as StackPanel).Children.Last() as TextBlock).Text)].GetBoolean())
                {
                    btn_activeleague.IsEnabled = false;
                    btn_deleteleague.IsEnabled = true;
                }
                else
                {
                    btn_activeleague.IsEnabled = true;
                    btn_deleteleague.IsEnabled = false;
                }
            }
        }
        private void Lst_league_LosingFocus(UIElement sender, LosingFocusEventArgs args)
        {
            if (args.NewFocusedElement != btn_activeleague && args.NewFocusedElement != btn_deleteleague)
            {
                lst_league.SelectedIndex = -1;
                btn_activeleague.IsEnabled = false;
                btn_deleteleague.IsEnabled = false;
            }
        }
    }
}
