using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class lumememm : ContentPage
    {
        StackLayout st;
        Button btnColor, btnDezentegreerima;
        Label lbl, lblEpilepsia;
        Switch sw, swEpilepsia;
        BoxView box, box2, box3_vedro;
        AbsoluteLayout abs;
        Random rnd = new Random();
        Image img;
        public lumememm()
        {
            InitializeComponent();
            BackgroundColor = Color.FromHex("#6c0000");
            box = new BoxView
            {
                BackgroundColor = Color.White,
                CornerRadius = 300,
                HeightRequest= 300,
                WidthRequest= 300
            };

            box2 = new BoxView
            {
                BackgroundColor = Color.White,
                CornerRadius = 300,
                HeightRequest = 200,
                WidthRequest = 200
            };

            box3_vedro = new BoxView
            {
                BackgroundColor = Color.Gray,
                CornerRadius = 10,
                HeightRequest = 130,
                WidthRequest = 100
            };
            //
            lbl = new Label
            {
                Text = "Näita/Peida"
            };

            sw = new Switch
            {
                BackgroundColor = Color.FromHex("#6c0000"),
                ThumbColor = Color.FromHex("#d00000"),
                IsToggled = false
            };
            sw.Toggled += Sw_Toggled;
            //

            btnColor = new Button
            {
                Text= "Random värv",
                BackgroundColor = Color.FromHex("#d00000"),
                TextColor = Color.FromHex("#6c0000")
            };
            btnColor.Clicked += BtnColor_Clicked;
            btnColor.Pressed += BtnColor_Pressed;

            btnDezentegreerima = new Button
            {
                Text = "Dezentegreerima",
                BackgroundColor = Color.FromHex("#d00000"),
                TextColor = Color.FromHex("#6c0000")
            };
            btnDezentegreerima.Clicked += BtnDezentegreerima_Clicked;
            //
            lblEpilepsia = new Label
            {
                Text = "Mode: Epilepsia",
                BackgroundColor = Color.FromHex("#d00000"),
                TextColor = Color.FromHex("#6c0000")
            };

            swEpilepsia = new Switch
            {
                BackgroundColor = Color.FromHex("#6c0000"),
                ThumbColor = Color.FromHex("#d00000"),
                IsToggled = false
            };
            swEpilepsia.Toggled += SwEpilepsia_Toggled;
            //
            img = new Image { Source = "bloodface.png", IsVisible=false };

            st = new StackLayout
            {
                Children = { lbl, sw, btnColor, btnDezentegreerima, lblEpilepsia, swEpilepsia  }
            };
            //
            abs = new AbsoluteLayout
            {
                Children = { box, box2, img, box3_vedro, st }
            };
            AbsoluteLayout.SetLayoutBounds(box3_vedro, new Rectangle(0.5, 0.01, 100, 60));
            AbsoluteLayout.SetLayoutFlags(box3_vedro, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(img, new Rectangle(0.5, 0.1, 300, 300));
            AbsoluteLayout.SetLayoutFlags(img, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(box2, new Rectangle(0.5, 0.1, 200, 200));
            AbsoluteLayout.SetLayoutFlags(box2, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(box, new Rectangle(0.5, 0.37, 300, 300));
            AbsoluteLayout.SetLayoutFlags(box, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(st, new Rectangle(0.5, 1.0, 300, 280));
            AbsoluteLayout.SetLayoutFlags(st, AbsoluteLayoutFlags.PositionProportional);

            Content = abs;
        }

        private async void SwEpilepsia_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                while (swEpilepsia.IsToggled) 
                {
                    BackgroundColor= Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                    box.BackgroundColor = Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                    box2.BackgroundColor = Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                    box3_vedro.BackgroundColor = Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                    await Task.Delay(30);
                }
            }
            else
            {
                BackgroundColor = Color.FromHex("#6c0000");
                box.BackgroundColor = Color.White;
                box2.BackgroundColor = Color.White;
                box3_vedro.BackgroundColor = Color.Gray;
            }
        }

        private async void BtnDezentegreerima_Clicked(object sender, EventArgs e)
        {
            for(int i = 0; i < 5; i++)
            {
                box.Opacity -=  0.2;
                box2.Opacity -= 0.2;
                box3_vedro.Opacity -= 0.2;
                await Task.Run(() => Thread.Sleep(1000));
            }
            sw.IsToggled = true;
        }

        private async void BtnColor_Pressed(object sender, EventArgs e)
        {
            while (btnColor.IsPressed)
            {
                img.IsVisible = true;
                await Task.Delay(10);
                img.IsVisible = false;
                await Task.Delay(10);
            }
        }

        private void BtnColor_Clicked(object sender, EventArgs e)
        {
            box.BackgroundColor = Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            box2.BackgroundColor = Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            box3_vedro.BackgroundColor = Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
        }

        private void Sw_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                box.Opacity= 0;
                box2.Opacity= 0;
                box3_vedro.Opacity= 0;
            }
            else
            {
                box.Opacity= 1;
                box2.Opacity= 1;
                box3_vedro.Opacity= 1;
            }
        }
    }
}