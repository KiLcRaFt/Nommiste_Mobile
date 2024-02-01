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
    public partial class EntryPage : ContentPage
    {
        Label lbl;
        Editor editor;
        public EntryPage()
        {
            Button Tagasi_btn = new Button
            {
                Text = "Tagasi to Start lehele",
                BackgroundColor = Color.FromHex("#6c0000"),
                TextColor = Color.FromHex("#d00000"),
            };

            lbl = new Label
            {
                Text = "Mingi tekst",
                BackgroundColor = Color.FromHex("#2c5c24"),
                TextColor = Color.FromHex("#1cd000"),
                VerticalTextAlignment= TextAlignment.Center
            };

            editor = new Editor
            {
                Placeholder = "Sisesta siia tekst ....",
                BackgroundColor = Color.FromHex("#014704"),
                TextColor = Color.FromHex("#eff500"),
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            StackLayout st = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.FromHex("#2e280b"),
                Children = {lbl, Tagasi_btn, editor},
                VerticalOptions= LayoutOptions.Fill,
            };

            Content = st;

            Tagasi_btn.Clicked += Entry_btn_Clicked;
            editor.TextChanged += Editor_TextChanged;
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl.Text = editor.Text;
        }

        private async void Entry_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
    
}