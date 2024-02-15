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
    public partial class FramePage : ContentPage
    {
        Grid grid;
        Random rnd = new Random();
        Frame fr;
        Label lbl;
        Image img;
        Switch sw;

        public FramePage()
        {
            grid = new Grid
            {
                BackgroundColor= Color.FromHex("#6c0000"),
                HorizontalOptions= LayoutOptions.FillAndExpand,
                VerticalOptions= LayoutOptions.FillAndExpand
            }; 

            TapGestureRecognizer tap= new TapGestureRecognizer();
            tap.Tapped += Tap_Tapped;
            tap.NumberOfTapsRequired = 1;

            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    grid.Children.Add
                        (
                            fr = new Frame 
                            { 
                                BackgroundColor= Color.FromRgb(rnd.Next(0,255), rnd.Next(0, 255), rnd.Next(0, 255)),
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.FillAndExpand
                            }, 
                            j, 
                            i
                        );
                    fr.GestureRecognizers.Add(tap);
                }
            }
            lbl = new Label { Text = "Tekst" , FontSize=Device.GetNamedSize(NamedSize.Title, typeof(Label))};
            grid.Children.Add( lbl, 0, 6 );
            Grid.SetColumnSpan(lbl, 6);

            img = new Image { Source = "smile.jpg" };
            sw = new Switch { IsToggled = false };
            sw.Toggled += Image_On_Off;
            grid.Children.Add( sw , 0, 7);
            grid.Children.Add( img, 1, 7 );

            Content= grid;
        }

        private void Image_On_Off(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                img.IsVisible= true;
            }
            else
            {
                img.IsVisible= false;
            }
        }

        private void Tap_Tapped(object sender, EventArgs e)
        {
            Frame fr = (Frame)sender;
            var r = Grid.GetRow(fr)+1;
            var c = Grid.GetColumn(fr)+1;
            lbl.Text = "Rida: "+r.ToString()+" Veerg: "+c.ToString();
        }
    }
}