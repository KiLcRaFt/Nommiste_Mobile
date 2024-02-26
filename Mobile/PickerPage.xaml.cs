using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.WebRequestMethods;

namespace Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickerPage : ContentPage
    {
        Picker picker;
        WebView webView;
        StackLayout st;
        string[] lehed = new string[3] { "https://github.com/KiLcRaFt", "https://moodle.edu.ee/course/view.php?id=37973", "https://www.tthk.ee/" };
        string[] nimetused = new string[3] { "Github", "Moodle", "TTHK" };
        public PickerPage()
        {
            picker = new Picker
            {
                Title = "Veebilehed"
            };
            foreach (string leht in nimetused)
            {
                picker.Items.Add(leht);
            }

            webView = new WebView
            {
                Source = new UrlWebViewSource{ Url= lehed[0] },
                HeightRequest = 600,
                WidthRequest=100
            };
            picker.SelectedIndex= 0;
            SwipeGestureRecognizer swipe_R = new SwipeGestureRecognizer
            {
                Direction=SwipeDirection.Right
            };
            swipe_R.Swiped += Swipe_Swiped;
            SwipeGestureRecognizer swipe_L = new SwipeGestureRecognizer { Direction=SwipeDirection.Left };
            swipe_L.Swiped += Swipe_L_Swiped;

            picker.SelectedIndexChanged += Valime_leht_avamiseks;
            st = new StackLayout
            {
                Children = { picker, webView}
            };
            st.GestureRecognizers.Add(swipe_R);
            st.GestureRecognizers.Add(swipe_L);
            Content = st;

        }

        private void Swipe_L_Swiped(object sender, SwipedEventArgs e)
        {
            if (picker.SelectedIndex < lehed.Length && picker.SelectedIndex >= 0)
            {
                picker.SelectedIndex += 1;
                
            }
            else if (picker.SelectedIndex == lehed.Length)
            {
                picker.SelectedIndex = 0;
            }
            var url = lehed[picker.SelectedIndex];
            webView.Source = new UrlWebViewSource { Url = url };
        }

        private void Swipe_Swiped(object sender, SwipedEventArgs e)
        {
            if (picker.SelectedIndex > 0 && picker.SelectedIndex <= lehed.Length)
            {
                picker.SelectedIndex -= 1;
            }
            else if(picker.SelectedIndex == 0)
            {
                picker.SelectedIndex = lehed.Length;
            }
            var url = lehed[picker.SelectedIndex];
            webView.Source = new UrlWebViewSource { Url = url };
        }

        private void Valime_leht_avamiseks(object sender, EventArgs e)
        {
            webView.Source = new UrlWebViewSource { Url = lehed[picker.SelectedIndex] };
        }
    }
}