using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class Riik
    {
        public string nimi { get; set; }
        public string pealinn { get; set; }
        public string rahvaarv { get; set; }
        public string lipp { get; set; }
        public int continent { get; set; }
    }
    public partial class Riigid : ContentPage
    {
        public ObservableCollection<Riik> EuroopaRiigid { get; set; }
        public ObservableCollection<Riik> AmeerikaRiigid { get; set; }
        public ObservableCollection<Riik> riigid { get; set; }
        Label lbl_list, lbl_euroopa, lbl_amerika;
        ListView list, list2;
        Button kustuta_btn, lisa_btn;
        ListView euroopaListView, ameerikaListView;
        public Riigid()
        {
            EuroopaRiigid = new ObservableCollection<Riik>();
            AmeerikaRiigid = new ObservableCollection<Riik>();
            riigid = new ObservableCollection<Riik>
            {
            new Riik {nimi="Estonia", pealinn="Tallinn", rahvaarv="345987", lipp="eesti.jpg", continent = 1},
            new Riik {nimi="Canada", pealinn="Ottawa", rahvaarv="678098", lipp="canada.png", continent = 0},
            };

            lbl_list = new Label
            {
                Text = "Riigid",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };

            kustuta_btn = new Button
            {
                Text = "Kustuta",
            };
            kustuta_btn.Clicked += Kustuta_btn_Clicked;

            lisa_btn = new Button
            {
                Text = "Lisa"
            };
            lisa_btn.Clicked += Lisa_btn_Clicked;
            //--------------------------------------
            foreach (Riik item in riigid)
            {
                if (item.continent == 1)
                    EuroopaRiigid.Add(item);
                else if (item.continent == 0)
                    AmeerikaRiigid.Add(item);
            }

            euroopaListView = new ListView
            {
                BackgroundColor = Color.WhiteSmoke,
                SeparatorColor = Color.Blue,
                Header = "Euroopa riigid:",
                Footer = DateTime.Now.ToString("T"),
                HasUnevenRows = true,
                ItemsSource = EuroopaRiigid,

                ItemTemplate = new DataTemplate(() =>
                {
                    ImageCell imageCell = new ImageCell { TextColor = Color.Red, DetailColor = Color.Green };
                    imageCell.SetBinding(ImageCell.TextProperty, "nimi");
                    Binding companyBinding = new Binding { Path = "pealinn", StringFormat = "Selle riigi pealinn on {0}" };
                    imageCell.SetBinding(ImageCell.DetailProperty, companyBinding);
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "lipp");
                    return imageCell;
                })
            };

            ameerikaListView = new ListView
            {
                BackgroundColor = Color.WhiteSmoke,
                SeparatorColor = Color.Blue,
                Header = "Ameerika riigid:",
                Footer = DateTime.Now.ToString("T"),
                HasUnevenRows = true,
                ItemsSource = AmeerikaRiigid,

                ItemTemplate = new DataTemplate(() =>
                {
                    ImageCell imageCell = new ImageCell { TextColor = Color.Red, DetailColor = Color.Green };
                    imageCell.SetBinding(ImageCell.TextProperty, "nimi");
                    Binding companyBinding = new Binding { Path = "pealinn", StringFormat = "Selle riigi pealinn on {0}" };
                    imageCell.SetBinding(ImageCell.DetailProperty, companyBinding);
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "lipp");
                    return imageCell;
                })
            };

            StackLayout st = new StackLayout { Children = { lbl_list, euroopaListView, ameerikaListView, lisa_btn, kustuta_btn } };

            euroopaListView.ItemTapped += List_ItemTapped;
            ameerikaListView.ItemTapped += List2_ItemTapped;
            //list.ItemSelected += List_ItemSelected;
            this.Content = st;
        }

        private async void List2_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Riik selectedRiik = e.Item as Riik;
            if (selectedRiik != null)
            {
                await DisplayAlert("Riik", $"{selectedRiik.nimi} - {selectedRiik.pealinn} \n Rahvaarv on: {selectedRiik.rahvaarv}", "OK");
            }
        }

        private async void Lisa_btn_Clicked(object sender, EventArgs e)
        {
            string nimi = await DisplayPromptAsync("Sisesta nimi ", "Sisesta nimi ", keyboard: Keyboard.Default);
            Riik riik = riigid.FirstOrDefault(r => r.nimi == nimi);
            if(riik == null)
            {
                string pealinn = await DisplayPromptAsync("Siseta pealinn", "Siseta pealinn ", keyboard: Keyboard.Default);
                string rahvaarv = await DisplayPromptAsync("Sisesta rahvaarv", "Sisesta rahvaarv ", keyboard: Keyboard.Numeric);
                string kontinent = await DisplayPromptAsync("Vali kontinent", "Vali euroopa (1) või ameerika (0) ", keyboard: Keyboard.Numeric);

                var photo = await MediaPicker.PickPhotoAsync();
                var img = photo.FileName;

                riigid.Add(new Riik { nimi = nimi, pealinn = pealinn, rahvaarv = rahvaarv, lipp = img, continent = Convert.ToInt32(kontinent) });
                if (Convert.ToInt32(kontinent) == 1)
                    EuroopaRiigid.Add(riigid.Last());
                else if (Convert.ToInt32(kontinent) == 0)
                    AmeerikaRiigid.Add(riigid.Last());
            }
            else
            {
                await DisplayAlert("Viga", "Selle riik on juba olemas!", "OK");
            }

        }
        private void Kustuta_btn_Clicked(object sender, EventArgs e)
        {
            Riik riik = null;

            if (euroopaListView.SelectedItem != null)
            {
                riik = euroopaListView.SelectedItem as Riik;
                EuroopaRiigid.Remove(riik);
                euroopaListView.SelectedItem = null;
            }
            else if (ameerikaListView.SelectedItem != null)
            {
                riik = ameerikaListView.SelectedItem as Riik;
                AmeerikaRiigid.Remove(riik);
                ameerikaListView.SelectedItem = null;
            }
        }

        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Riik selectedRiik = e.Item as Riik;
            if (selectedRiik != null)
            {
                await DisplayAlert("Riik", $"{selectedRiik.nimi} - {selectedRiik.pealinn} \n Rahvaarv on: {selectedRiik.rahvaarv}", "OK");
            }
        }

        private void List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                lbl_list.Text = e.SelectedItem.ToString();
            }
        }

    }
    
}
