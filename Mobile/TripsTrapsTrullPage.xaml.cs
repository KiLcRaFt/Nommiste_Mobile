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
        StackLayout st;
        AbsoluteLayout abs;
        Frame fr;
        Image krest, noll;
        Button btn;
        Label player;
        int taps = 1;
        List<List<int>> kresti = new List<List<int>>();
        List<List<int>> nolli = new List<List<int>>();
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
            player = new Label
            {
                Text = "Nollid käivad",
            };

            st = new StackLayout
            {
                Children = { player }
            };

            abs = new AbsoluteLayout
            {
                Children = { grid, st }
            };

            AbsoluteLayout.SetLayoutBounds(grid, new Rectangle(0.5, 0.01, 400, 400));
            AbsoluteLayout.SetLayoutFlags(grid, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(st, new Rectangle(0.5, 1, 300, 280));
            AbsoluteLayout.SetLayoutFlags(st, AbsoluteLayoutFlags.PositionProportional);
            Content = abs;

        }


        private void Tap_Tapped(object sender, EventArgs e)
        {
            
            Frame fr = (Frame)sender;
            if (fr.Content == null)
            {
                var rida = Grid.GetRow(fr);
                var column = Grid.GetColumn(fr);

                if (taps % 2 == 0)
                {
                    krest = new Image { Source = "krest.png" };
                    fr.Content = krest;
                    player.Text = "Ristikud käivad";
                    kresti.Add(new List<int> { rida, column });
                }
                else
                {
                    noll = new Image { Source = "noll.png" };
                    fr.Content = noll;
                    player.Text = "Nollid käivad";
                    nolli.Add(new List<int> { rida, column });
                }
                taps++;
            }
            WOL();
        }
        private void WOL()
        {
            // Check for horizontal wins
            for (int i = 0; i < 3; i++)
            {
                if (kresti.Count(row => row[0] == i) == 3)
                {
                    // Horizontal win
                    // Handle the win condition here
                    DisplayAlert("Game Over", "Player with crosses wins!", "OK");
                    return;
                }
            }

            // Check for vertical wins
            for (int i = 0; i < 3; i++)
            {
                if (kresti.Count(column => column[1] == i) == 3)
                {
                    // Vertical win
                    // Handle the win condition here
                    DisplayAlert("Game Over", "Player with crosses wins!", "OK");
                    return;
                }
            }

            // Check for diagonal wins
            if (kresti.Any(pos => pos[0] == pos[1]) || kresti.Any(pos => pos[0] + pos[1] == 2))
            {
                // Diagonal win
                // Handle the win condition here
                DisplayAlert("Game Over", "Player with crosses wins!", "OK");
                return;
            }

            // No win yet
            // Continue the game or handle other conditions
            // 8 pobed == 3 vertikalno, 3 gorizontalno, 2 diagonal
        }
    }
}