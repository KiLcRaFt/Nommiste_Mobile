using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoxPage : ContentPage
    {
        BoxView box;
        Label lbl;
        Button Close_btn;
        int mon_height = (int)DeviceDisplay.MainDisplayInfo.Height;
        int mon_width = (int)DeviceDisplay.MainDisplayInfo.Width;
        public BoxPage()
        {

            lbl = new Label
            {
                Text = "0",
                TextColor = Color.FromHex("#ffdfe5"),
                FontSize= 48,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment= TextAlignment.Center
            };

            int r=0, g=0, b=0;
            box = new BoxView
            {
                Color= Color.FromRgb(r,g,b),
                CornerRadius = 10,
                WidthRequest= 200,HeightRequest=200,
                HorizontalOptions= LayoutOptions.Center,
                VerticalOptions= LayoutOptions.CenterAndExpand
            };

            Close_btn = new Button
            {
                Text = "Tagasi to Start lehele",
                BackgroundColor = Color.FromHex("#6c0000"),
                TextColor = Color.FromHex("#d00000"),
            };

            Close_btn.Clicked += Close_btn_Clicked;

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += Tap_Tapped;
            box.GestureRecognizers.Add(tap);

            StackLayout st = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.FromHex("#2e280b"),
                Children = { box, Close_btn, lbl }
            };
            Content = st;


        }

        private async void Close_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        Random rnd;
        private void Tap_Tapped(object sender, EventArgs e)
        {
            rnd = new Random();
            box.Color= Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            int num = Convert.ToInt32(lbl.Text);
            lbl.Text = Convert.ToString(num+1);

            if (box.Height < 350)
            {
                int height = Convert.ToInt32(box.HeightRequest);
                box.HeightRequest = height + 5;
            }
            else
            {
                box.HeightRequest = 200;
            };

            if (box.Width < 350)
            {
                int width = Convert.ToInt32(box.WidthRequest);
                box.WidthRequest = width + 1;
            }
            else
            {
                box.WidthRequest = 200;
            }
        }
    }
}