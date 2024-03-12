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
        ImageButton imgBtn;
        Button btn_Home, btn_history, btn_back, btn_favorite;
        List<string> lehed = new List<string> { "https://google.com" ,"https://github.com/KiLcRaFt", "https://moodle.edu.ee/course/view.php?id=37973", "https://www.tthk.ee/" };
        List<string> nimetused = new List<string> { "Google", "Github", "Moodle", "TTHK" };
        List<string> history = new List<string> { "https://google.com" };
        List<string> favorite = new List<string> { "https://twitch.tv" };
        public PickerPage()
        {
            picker = new Picker
            {
                Title = "Veebilehed",
                HeightRequest = 50
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
                Source = new UrlWebViewSource { Url = lehed[0] },
                HeightRequest = 550,
                WidthRequest = width
            };
            picker.SelectedIndex = 0;
            SwipeGestureRecognizer swipe_R = new SwipeGestureRecognizer
            {
                Direction = SwipeDirection.Right
            };
            swipe_R.Swiped += Swipe_Swiped;
            SwipeGestureRecognizer swipe_L = new SwipeGestureRecognizer { Direction = SwipeDirection.Left };
            swipe_L.Swiped += Swipe_L_Swiped;

            //Entry ------------------------------------------------------------

            entry = new Entry
            {
                HeightRequest = 50
            };
            entry.Completed += Entry_Completed;

            //Buttons -------------------------------------------------------------

            btn_Home = new Button
            {
                Text = "Home",
                BackgroundColor = Color.Gray,
                HeightRequest = 25,
                WidthRequest = 50,
                CornerRadius = 20,
                FontSize= 11
            };

            btn_Home.Clicked += Btn_Home_Clicked;

            btn_history = new Button
            {
                Text = "History",
                BackgroundColor = Color.Gray,
                HeightRequest = 25,
                WidthRequest = 50,
                CornerRadius = 20,
                FontSize = 11
            };

            btn_history.Clicked += Btn_history_Clicked;


            btn_back = new Button
            {
                Text = "<-",
                BackgroundColor = Color.Gray,
                HeightRequest = 25,
                WidthRequest = 50,
                CornerRadius = 20,
                FontSize = 11
            };

            btn_back.Clicked += Btn_back_Clicked;

            btn_favorite = new Button
            {
                Text = "Lemmikud",
                BackgroundColor = Color.Gray,
                HeightRequest = 25,
                WidthRequest = 50,
                CornerRadius = 20,
                FontSize = 11
            };
            btn_favorite.Clicked += Btn_favorite_Clicked;

            //---------------------------------------------------

            imgBtn = new ImageButton
            {
                Source = "~/Nommiste_Mobile/Mobile.Android/Resources/drawable/favorite.png",
                HeightRequest = 25,
                WidthRequest = 50
            };

            imgBtn.Clicked += ImgBtn_Clicked;

            picker.SelectedIndexChanged += Valime_leht_avamiseks;


            Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                
                Children = { picker, entry, webView }
            };
            for (int i = 0; i < 3; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40, GridUnitType.Absolute) });
            }

            grid.Children.Add(picker, 0, 0);
            Grid.SetColumnSpan(picker, 8);

            grid.Children.Add(entry, 0, 1);
            Grid.SetColumnSpan(entry, 8);

            grid.Children.Add(btn_Home, 0, 2);
            Grid.SetColumnSpan(btn_Home, 2);
            grid.Children.Add(btn_history, 2, 2);
            Grid.SetColumnSpan(btn_history, 2);
            grid.Children.Add(btn_back, 4, 2);
            Grid.SetColumnSpan(btn_back, 2);
            grid.Children.Add(btn_favorite, 6, 2);
            Grid.SetColumnSpan(btn_favorite, 2);

            grid.Children.Add(imgBtn, 8, 2);
            Grid.SetColumnSpan(imgBtn, 2);


            st = new StackLayout
            {
                Children = { grid, webView }
            };

            st.GestureRecognizers.Add(swipe_R);
            st.GestureRecognizers.Add(swipe_L);

            Content = st;

        }

        private void ImgBtn_Clicked(object sender, EventArgs e)
        {
            var Url = (webView.Source as UrlWebViewSource)?.Url;
            favorite.Add(Url);
        }

        private async void Btn_favorite_Clicked(object sender, EventArgs e)
        {
            string[] favoritee = favorite.ToArray();
            var urll = await DisplayActionSheet("Lemmik", "Lobbu", null, favoritee);
            webView.Source = urll;
            if (lehed.Contains(urll))
            {
                picker.SelectedIndex = lehed.IndexOf(urll);
            }
            else
            {
                lehed.Add(urll);
                picker.SelectedIndex = lehed.IndexOf(urll);
            }
        }

        private void Btn_back_Clicked(object sender, EventArgs e)
        {
            if (history.Any())
            {
                webView.GoBack();
                //string url = webView.Source.ToString();
                //picker.SelectedIndex = lehed.IndexOf(url);
                //int ind = lehed.IndexOf(history[history.Count() -1]);
                //picker.SelectedIndex = ind;
                //webView.Source = new UrlWebViewSource { Url = lehed[ind] };


            }
        }

        private void Btn_Home_Clicked(object sender, EventArgs e)
        {
            webView.Source = lehed[0];
            picker.SelectedIndex = 0;

        }

        private async void Btn_history_Clicked(object sender, EventArgs e)
        {
            string[] historyy = history.ToArray();
            var url = await DisplayActionSheet("Ajalugu", "Lobbu", null, historyy);
            webView.Source = url;
            picker.SelectedIndex = lehed.IndexOf(url);
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