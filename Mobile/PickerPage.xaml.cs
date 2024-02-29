using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
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
        AbsoluteLayout abs;
        Entry entry;
        Button btn_Home, btn_history, btn_back;
        List<string> lehed = new List<string> { "https://github.com/KiLcRaFt", "https://moodle.edu.ee/course/view.php?id=37973", "https://www.tthk.ee/" };
        List<string> nimetused = new List<string> { "Github", "Moodle", "TTHK" };
        List<string> history = new List<string> { };
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

            var width = DeviceDisplay.MainDisplayInfo.Width;
            var height = DeviceDisplay.MainDisplayInfo.Height;

            //WebView ---------------------------------------------------------

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

            //Entry ------------------------------------------------------------

            entry = new Entry
            {

            };
            entry.Completed += Entry_Completed;

            //Buttons -------------------------------------------------------------

            btn_Home = new Button
            {
                Text="Home",
                BackgroundColor= Color.WhiteSmoke,
                HeightRequest = 25,
                WidthRequest = 50,
                CornerRadius = 20
            };
            AbsoluteLayout.SetLayoutBounds(btn_Home, new Rectangle(0.01, 0.05, 50, 25));
            AbsoluteLayout.SetLayoutFlags(btn_Home, AbsoluteLayoutFlags.PositionProportional);

            btn_history = new Button
            {
                Text= "History",
                BackgroundColor = Color.WhiteSmoke,
                HeightRequest = 25,
                WidthRequest = 50,
                CornerRadius = 20
            };

            AbsoluteLayout.SetLayoutBounds(btn_history, new Rectangle(0.06, 0.05, 50, 25));
            AbsoluteLayout.SetLayoutFlags(btn_history, AbsoluteLayoutFlags.PositionProportional);

            btn_back = new Button
            {
                Text = "<-",
                BackgroundColor = Color.WhiteSmoke,
                HeightRequest = 25,
                WidthRequest = 50,
                CornerRadius = 20
            };
            AbsoluteLayout.SetLayoutBounds(btn_back, new Rectangle(0.11, 0.5, 50, 25));
            AbsoluteLayout.SetLayoutFlags(btn_back, AbsoluteLayoutFlags.PositionProportional);



            picker.SelectedIndexChanged += Valime_leht_avamiseks;
            st = new StackLayout
            {
                Children = { picker, entry, webView}
            };
            st.GestureRecognizers.Add(swipe_R);
            st.GestureRecognizers.Add(swipe_L);

            AbsoluteLayout.SetLayoutBounds(st, new Rectangle(0, 0.05, 400, 800));
            AbsoluteLayout.SetLayoutFlags(st, AbsoluteLayoutFlags.PositionProportional);

            abs = new AbsoluteLayout
            {
                Children = { btn_Home, btn_history, btn_back, st }
            };
            Content = abs;

        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            string url = entry.Text;
            lehed.Add("https://" + url);
            nimetused.Add(url);
            int lastIndex = nimetused.Count- 1;

            if (lastIndex >= 0)
            {
                picker.Items.Add(nimetused[lastIndex]);
                picker.SelectedIndex = lastIndex;
                var urll = lehed[picker.SelectedIndex];
                DisplayAlert("Navigation", $"Opening {lehed[lastIndex]}", "OK");
                webView.Source = new UrlWebViewSource { Url = lehed[lastIndex] };
                history.Add(lehed[lastIndex]);
            }
        }

        private void Swipe_L_Swiped(object sender, SwipedEventArgs e)
        {
            if (picker.SelectedIndex < lehed.Count && picker.SelectedIndex >= 0)
            {
                picker.SelectedIndex += 1;
                
            }
            else if (picker.SelectedIndex == lehed.Count)
            {
                picker.SelectedIndex = 0;
            }
            var url = lehed[picker.SelectedIndex];
            webView.Source = new UrlWebViewSource { Url = url };
        }

        private void Swipe_Swiped(object sender, SwipedEventArgs e)
        {
            if (picker.SelectedIndex > 0 && picker.SelectedIndex <= lehed.Count)
            {
                picker.SelectedIndex -= 1;
            }
            else if(picker.SelectedIndex == 0)
            {
                picker.SelectedIndex = lehed.Count;
            }
            var url = lehed[picker.SelectedIndex];
            webView.Source = new UrlWebViewSource { Url = url };
        }

        private void Valime_leht_avamiseks(object sender, EventArgs e)
        {
            webView.Source = new UrlWebViewSource { Url = lehed[picker.SelectedIndex] };
            history.Add(lehed[picker.SelectedIndex]);
        }
    }
}