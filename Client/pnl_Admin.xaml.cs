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
    public sealed partial class pnl_Admin : Page
    {
        private Admin acc;
        public pnl_Admin()
        {
            this.InitializeComponent();
            Useful.SetTitlebar();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            acc = e.Parameter as Admin;
        }
        private void Nav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer == btn_addleague)
                frm.Navigate(typeof(AddLeague), acc);
            else if(args.InvokedItemContainer == btn_deleteleague)
                frm.Navigate(typeof(DeleteLeague), acc);
            else if (args.InvokedItemContainer == btn_endleague)
                frm.Navigate(typeof(EndLeague), acc);
            else if (args.InvokedItemContainer == btn_addcompetetion)
                frm.Navigate(typeof(Addcompetition), acc);
            else if (args.InvokedItemContainer == btn_deletecompetition)
                frm.Navigate(typeof(DeleteCompetition), acc);
            else if (args.InvokedItemContainer == btn_editcompetition)
                frm.Navigate(typeof(EditCompetition), acc);
            else if (args.InvokedItemContainer == btn_editresult)
                frm.Navigate(typeof(EditResult), acc);
            else if (args.InvokedItemContainer == btn_signout)
                Frame.Navigate(typeof(home));

        }
    }
}
