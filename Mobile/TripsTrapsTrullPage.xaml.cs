using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        Button btn, btn2;
        Label player;
        int taps = 1;
        List<Frame> tapped = new List<Frame>();
        List<Frame> untapped = new List<Frame>();

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
                Text = "Nollid käivad"
            };

            btn = new Button
            {
                Text = "Uus mäng"
            };
            btn.Clicked += Btn_Clicked; 
            btn2 = new Button
            {
                Text = "Kes on esimene?"
            };
            btn2.Clicked += Btn2_Clicked;

            st = new StackLayout
            {
                Children = { player, btn, btn2 }
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

        private void Btn2_Clicked(object sender, EventArgs e)
        {
            if(player.Text == "Nollid käivad")
            {
                taps = 2;
                player.Text = "Ristikud käivad";
            }
            else if(player.Text == "Ristikud käivad")
            {
                taps = 1;
                player.Text = "Nollid käivad";
            }
        }

        private void Btn_Clicked(object sender, EventArgs e)
        {
            Newgame();
        }

        public void Newgame()
        {
            foreach (var child in grid.Children)
            {
                if (child is Frame frame)
                {
                    frame.Content = null;
                }
            }
            taps = 1;
            player.Text = "Nollid käivad";
            tapped.Clear();
            untapped.Clear();
        }
        private void Tap_Tapped(object sender, EventArgs e)
        {

            Frame fr = (Frame)sender;
            if (fr.Content == null)
            {


                if (taps % 2 == 0)
                {
                    krest = new Image { Source = "krest.png" };
                    fr.Content = krest;
                    player.Text = "Nollid käivad";
                    tapped.Add(fr);
                    untapped.Remove(fr);
                }
                else
                {
                    noll = new Image { Source = "noll.png" };
                    fr.Content = noll;
                    player.Text = "Ristikud käivad";
                    tapped.Add(fr);
                    untapped.Remove(fr);
                }
                taps++;
            }
            WOL();
        }
        private async void WOL()
        {
            foreach (var winCondition in Winconditions())
            {
                bool xWins = true;
                bool oWins = true;
                foreach (var image in winCondition)
                {
                    if (image == null)
                    {
                        oWins = false;
                        xWins = false;
                    }
                    else
                    {
                        var imageSource = image.Source as FileImageSource;
                        if (imageSource != null)
                        {
                            if (imageSource.File == "noll.png")
                            {
                                oWins = false;
                            }
                            else if (imageSource.File == "krest.png")
                            {
                                xWins = false;
                            }
                        }
                    }
                }

                if (xWins)
                {
                    await DisplayAlert("O Võidab", "Nollid Võidabad", "OK");
                    bool answer = await DisplayAlert("Mida me teeme?", "Kas te tahate mängu jätkama", "Jah", "Ei");
                    if (answer) 
                    {
                        winCondition.Clear();
                        Newgame();   
                    }
                    break;
                }
                if (oWins)
                {
                    await DisplayAlert("X Võidab", "Ristikud Võidavad", "OK");
                    bool answer = await DisplayAlert("Mida me teeme?", "Kas te tahate mängu jätkama", "Jah", "Ei");
                    if (answer)
                    {

                        winCondition.Clear();
                        Newgame();
                    }
                    break;
                }
            }
            if (grid.Children.All(child => (child as Image)?.Source != null))
            {
                await DisplayAlert("Viik", "Unlack", "OK");
                bool answer = await DisplayAlert("Mida me teeme?", "Kas te tahate mängu jätkama", "Jah", "Ei");
                if (answer)
                {
                    Newgame();
                }
                
            }
        }
        private List<List<Image>> Winconditions()
        {
            List<List<Image>> winConditions = new List<List<Image>>();

            winConditions.Add(new List<Image> { GetImage(0, 0), GetImage(1, 0), GetImage(2, 0) });
            winConditions.Add(new List<Image> { GetImage(0, 1), GetImage(1, 1), GetImage(2, 1) });
            winConditions.Add(new List<Image> { GetImage(0, 2), GetImage(1, 2), GetImage(2, 2) });

            winConditions.Add(new List<Image> { GetImage(0, 0), GetImage(0, 1), GetImage(0, 2) });
            winConditions.Add(new List<Image> { GetImage(1, 0), GetImage(1, 1), GetImage(1, 2) });
            winConditions.Add(new List<Image> { GetImage(2, 0), GetImage(2, 1), GetImage(2, 2) });

            winConditions.Add(new List<Image> { GetImage(0, 0), GetImage(1, 1), GetImage(2, 2) });
            winConditions.Add(new List<Image> { GetImage(0, 2), GetImage(1, 1), GetImage(2, 0) });

            return winConditions;
        }

        private Image GetImage(int row, int column)
        {
            foreach (var child in grid.Children)
            {
                if (child is Frame && Grid.GetRow(child) == row && Grid.GetColumn(child) == column)
                {
                    var frame = (Frame)child;
                    if (frame.Content is Image)
                    {
                        return (Image)frame.Content;
                    }
                }
            }
            return null;
        }
    }
}