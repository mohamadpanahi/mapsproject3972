using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace testui
{
    class Useful
    {
        public static void SetTitlebar(UIElement titlebar = null)
        {
            //set st_titlebar as titlebar
            if (titlebar != null)
            {
                CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
                coreTitleBar.ExtendViewIntoTitleBar = true;
                Window.Current.SetTitleBar(titlebar);
            }

            //define titlebar color
            ApplicationViewTitleBar formattableTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            formattableTitleBar.ButtonBackgroundColor = Colors.Transparent;
            formattableTitleBar.ButtonForegroundColor = Colors.Black;

            formattableTitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            formattableTitleBar.ButtonInactiveForegroundColor = Colors.DarkGray;

            formattableTitleBar.ButtonHoverBackgroundColor = Colors.DarkGray;

            formattableTitleBar.ButtonPressedBackgroundColor = Colors.DimGray;
        }

        public static string Fa_En(string fa)
        {
            string en = "";

            foreach (char ch in fa)
            {
                switch (ch)
                {
                    case 'ا':
                        en += 'a'; break;
                    case 'ب':
                        en += 'b'; break;
                    case 'پ':
                        en += 'c'; break;
                    case 'ت':
                        en += 'd'; break;
                    case 'ث':
                        en += 'e'; break;
                    case 'ج':
                        en += 'f'; break;
                    case 'چ':
                        en += 'g'; break;
                    case 'ح':
                        en += 'h'; break;
                    case 'خ':
                        en += 'i'; break;
                    case 'د':
                        en += 'j'; break;
                    case 'ذ':
                        en += 'k'; break;
                    case 'ر':
                        en += 'l'; break;
                    case 'ز':
                        en += 'm'; break;
                    case 'ژ':
                        en += 'n'; break;
                    case 'س':
                        en += 'o'; break;
                    case 'ش':
                        en += 'p'; break;
                    case 'ص':
                        en += 'q'; break;
                    case 'ض':
                        en += 'r'; break;
                    case 'ط':
                        en += 's'; break;
                    case 'ظ':
                        en += 't'; break;
                    case 'ع':
                        en += 'u'; break;
                    case 'غ':
                        en += 'v'; break;
                    case 'ف':
                        en += 'w'; break;
                    case 'ق':
                        en += 'x'; break;
                    case 'ک':
                        en += 'y'; break;
                    case 'گ':
                        en += 'z'; break;
                    case 'ل':
                        en += 'A'; break;
                    case 'م':
                        en += 'B'; break;
                    case 'ن':
                        en += 'C'; break;
                    case 'و':
                        en += 'D'; break;
                    case 'ه':
                        en += 'E'; break;
                    case 'ی':
                        en += 'F'; break;

                    case 'آ':
                        en += 'G'; break;
                    case 'ء':
                        en += 'H'; break;
                    case 'ئ':
                        en += 'I'; break;
                    default:
                        en += ch; break;
                }
            }

            return en;
        }
        public static string En_Fa(string en)
        {
            string fa = "";

            foreach (char ch in en)
            {
                switch (ch)
                {
                    case 'a':
                        fa += 'ا'; break;
                    case 'b':
                        fa += 'ب'; break;
                    case 'c':
                        fa += 'پ'; break;
                    case 'd':
                        fa += 'ت'; break;
                    case 'e':
                        fa += 'ث'; break;
                    case 'f':
                        fa += 'ج'; break;
                    case 'g':
                        fa += 'چ'; break;
                    case 'h':
                        fa += 'ح'; break;
                    case 'i':
                        fa += 'خ'; break;
                    case 'j':
                        fa += 'د'; break;
                    case 'k':
                        fa += 'ذ'; break;
                    case 'l':
                        fa += 'ر'; break;
                    case 'm':
                        fa += 'ز'; break;
                    case 'n':
                        fa += 'ژ'; break;
                    case 'o':
                        fa += 'س'; break;
                    case 'p':
                        fa += 'ش'; break;
                    case 'q':
                        fa += 'ص'; break;
                    case 'r':
                        fa += 'ض'; break;
                    case 's':
                        fa += 'ط'; break;
                    case 't':
                        fa += 'ظ'; break;
                    case 'u':
                        fa += 'ع'; break;
                    case 'v':
                        fa += 'غ'; break;
                    case 'w':
                        fa += 'ف'; break;
                    case 'x':
                        fa += 'ق'; break;
                    case 'y':
                        fa += 'ک'; break;
                    case 'z':
                        fa += 'گ'; break;
                    case 'A':
                        fa += 'ل'; break;
                    case 'B':
                        fa += 'م'; break;
                    case 'C':
                        fa += 'ن'; break;
                    case 'D':
                        fa += 'و'; break;
                    case 'E':
                        fa += 'ه'; break;
                    case 'F':
                        fa += 'ی'; break;

                    case 'G':
                        fa += 'آ'; break;
                    case 'H':
                        fa += 'ء'; break;
                    case 'I':
                        fa += 'ئ'; break;
                    default:
                        fa += ch; break;
                }
            }

            return fa;
        }
    }
}
