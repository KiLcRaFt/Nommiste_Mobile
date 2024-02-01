using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            //InitializeComponent();
            Button Entry_btn = new Button
            {
                Text = "Entry leht",
                BackgroundColor = Color.FromHex("#2c5c24"),
                TextColor = Color.FromHex("#1cd000"),
                WidthRequest= 150, HeightRequest = 75
            };

            Button Time_btn = new Button
            {
                Text = "Time leht",
                BackgroundColor = Color.FromHex("#2c5c24"),
                TextColor = Color.FromHex("#eff500"),
                WidthRequest = 150,
                HeightRequest = 75
            };

            Button BoxView_btn = new Button
            {
                Text = "BoxView leht",
                BackgroundColor = Color.FromHex("#013869"),
                TextColor = Color.FromHex("#00ffef"),
                WidthRequest = 150,
                HeightRequest = 75
            };

            StackLayout st = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.FromHex("#2e280b"),
                Children = { Entry_btn, Time_btn, BoxView_btn },
            };

            Content = st;

            Entry_btn.Clicked += Entry_btn_Clicked;
            Time_btn.Clicked += Time_btn_Clicked;
            BoxView_btn.Clicked += BoxView_btn_Clicked;
        }

        private async void BoxView_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BoxPage());
        }

        private async void Time_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TimePage());
        }

        private async void Entry_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EntryPage());
        }
    }
}