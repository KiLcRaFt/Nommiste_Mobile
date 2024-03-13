using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class Telefon
    {
        public string Nimetus { get; set; }
        public string Tootaja { get; set; }
        public int Hind { get; set; }
        public string Pilt { get; set; }
    }
    public partial class List_page : ContentPage
    {
        public ObservableCollection<Telefon> telefons { get; set; }
        Label lbl_list;
        ListView list;
        Button kustuta_btn, lisa_btn;
        public List_page()
        {
            telefons = new ObservableCollection<Telefon>
            {
            new Telefon {Nimetus= "Iphone 13", Tootaja = "Apple", Hind = 1299, Pilt="iphone.jpg"},
            new Telefon {Nimetus= "Samsung galaxy S9", Tootaja = "Samsung", Hind = 899, Pilt = "samsung.jpg"},
            new Telefon {Nimetus= "Huawei P10", Tootaja = "Huawei", Hind = 680, Pilt = "huawei.jpg"},
            new Telefon {Nimetus= "LG G6", Tootaja = "LG", Hind = 760, Pilt = "lg.jpg"}
            };

            lbl_list = new Label
            {
                Text = "Telefonide loetelu",
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

            list = new ListView
            {
                SeparatorColor = Color.Blue,
                Header = "Minu oma kolektion:",
                Footer = DateTime.Now.ToString("T"),

                HasUnevenRows = true,
                ItemsSource = telefons,
                    
                ItemTemplate = new DataTemplate(() =>
                {
                    //    Label nimetus = new Label { FontSize = 20 };
                    //    nimetus.SetBinding(Label.TextProperty, "Nimetus");

                    //    Label tootaja = new Label();
                    //    tootaja.SetBinding(Label.TextProperty, "Tootaja");

                    //    Label hind = new Label();
                    //    hind.SetBinding(Label.TextProperty, "Hind");

                    //    return new ViewCell
                    //    {
                    //        View = new StackLayout
                    //        {
                    //            Padding = new Thickness(0, 5),
                    //            Orientation = StackOrientation.Horizontal,
                    //            Children = { nimetus, tootaja, hind }

                    //        }
                    //    };
                    ImageCell imageCell = new ImageCell { TextColor = Color.Red, DetailColor = Color.Green };
                    imageCell.SetBinding(ImageCell.TextProperty, "Nimetus");
                    Binding companyBinding = new Binding { Path = "Tootaja", StringFormat = "Tore telefon firmalt {0}"};
                    imageCell.SetBinding(ImageCell.DetailProperty, companyBinding);
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "Pilt");
                    return imageCell;
                })
                        

            };
            list.ItemTapped += List_ItemTapped;
            //list.ItemSelected += List_ItemSelected;
            this.Content = new StackLayout { Children = { lbl_list, list, lisa_btn, kustuta_btn } };
        }

        private void Lisa_btn_Clicked(object sender, EventArgs e)
        {
            telefons.Add(new Telefon { Nimetus = "Telefon", Tootaja = "Tootaja", Hind = 1 });
        }

        private void Kustuta_btn_Clicked(object sender, EventArgs e)
        {
            Telefon phone = list.SelectedItem as Telefon;
            if(phone != null)
            {
                telefons.Remove(phone);
                list.SelectedItem= null;
            }
        }

        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Telefon selectedPhone = e.Item as Telefon;
            if (selectedPhone != null)
            {
                await DisplayAlert("Выбранная модель", $"{selectedPhone.Tootaja} - {selectedPhone.Nimetus}", "OK");
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