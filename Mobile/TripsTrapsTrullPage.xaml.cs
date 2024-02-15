using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
//using Xamarin.Forms.PlatformConfiguration.TizenSpecific;
using Xamarin.Forms.Xaml;

namespace Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TripsTrapsTrullPage : ContentPage
    {
        Grid grid;
        Frame fr;
        Image krest, noll;
        int taps = 1;
        public TripsTrapsTrullPage()
        {
            
            grid = new Grid
            {
                BackgroundColor = Color.Black,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += Tap_Tapped;
            tap.NumberOfTapsRequired = 1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    grid.Children.Add
                        (
                            fr = new Frame
                            {
                                BackgroundColor = Color.WhiteSmoke,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.FillAndExpand
                            },
                            j,
                            i
                        );
                    fr.GestureRecognizers.Add(tap);
                }
            }
            AbsoluteLayout.SetLayoutBounds(grid, new Rectangle(0.5, 0.01, 300, 300));
            AbsoluteLayout.SetLayoutFlags(grid, AbsoluteLayoutFlags.PositionProportional);
            Content = grid;
            
        }
        

    private void Tap_Tapped(object sender, EventArgs e)
        {
            taps++;
            Frame fr = (Frame)sender;
            if (fr.Content == null)
            {
                if (taps % 2 == 0)
                {
                    krest = new Image { Source = "krest.png" };
                    fr.Content = krest;
                }
                else
                {
                    noll = new Image { Source = "noll.png" };
                    fr.Content = noll;
                }
            }
            
                       
        }
    }
}