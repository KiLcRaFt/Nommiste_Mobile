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
    public partial class Startpage2_0 : ContentPage
    {
            List<ContentPage> pages = new List<ContentPage>()
            {
                new EntryPage(), new TimePage(), new BoxPage(), new lumememm(),  new FramePage(), new TripsTrapsTrullPage(), new PickerPage(), new Table_Page(), new List_page()
            };
            List<string> texts = new List<string>()
            {
                "Ava entry","Ava timer leht", "Ava Box leht", "Ava lumemmem leht", "Ava Frame leht", "Ava Trips Traps Trull leht", "Ava Picker leht", "TableView leht", "List leht"
            };
            StackLayout st;
            public Startpage2_0()
            {
                st = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    BackgroundColor = Color.FromHex("#2e280b")
                };
                for (int i = 0; i < pages.Count; i++)
                {
                    Button button = new Button
                    {
                        Text = texts[i],
                        TextColor = Color.FromHex("#d00000"),
                        BackgroundColor = Color.FromHex("#ffdfe5"),
                        TabIndex = i
                    };
                    st.Children.Add(button);
                    button.Clicked += Button_Clicked;
                }
                ScrollView sv = new ScrollView { Content = st };
                Content = sv;
            }

            private async void Button_Clicked(object sender, EventArgs e)
            {
                Button btn = (Button)sender;
                await Navigation.PushAsync(pages[btn.TabIndex]);
            }
        
    }
}