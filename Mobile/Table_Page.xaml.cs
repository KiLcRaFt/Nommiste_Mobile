using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Messaging;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Table_Page : ContentPage
    {
        TableView tableview;
        Button btn_hel, btn_sms, btn_email;
        public Table_Page()
        {
            btn_hel= new Button
            {
                Text = "Helista",
                WidthRequest = 120,
            };
            btn_hel.Clicked += Btn_hel_Clicked;

            btn_sms = new Button
            {
                Text = "Saada sms",
                WidthRequest = 120
            };

            btn_email = new Button
            {
                Text = "Kirjuta",
                WidthRequest = 120
            };

            tableview = new TableView
            {

                Intent = TableIntent.Form,
                Root = new TableRoot("Andmete sisestamine")
                {
                    new TableSection("Põhiandmed:")
                    {
                        new EntryCell
                        {
                            Label="Nimi:",
                            Placeholder="Sisesta oma sõbra nimi",
                            Keyboard=Keyboard.Default
                        }
                    },

                    new TableSection("Kontaktiandmed:")
                    {
                        new EntryCell
                        {
                            Label = "Telefon",
                            Placeholder = "Sisesta tel. number",
                            Keyboard=Keyboard.Telephone
                        },
                        new EntryCell
                        {
                            Label="Email",
                            Placeholder="Sisesta email",
                            Keyboard=Keyboard.Email
                        }
                    },
                    new TableSection("Valige")
                    {
                        new ViewCell
                        {
                            View = new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                HeightRequest = 200,
                                Children =
                                {
                                    btn_hel, btn_sms, btn_email
                                }
                            }
                        }
                    }
                }
            };
            Content = tableview;
        }

        private void Btn_hel_Clicked(object sender, EventArgs e)
        {
            var phoneNumberCell = tableview.Root
            .FirstOrDefault(section => section.Title == "Kontaktiandmed:")
            ?.FirstOrDefault(cell => (cell as EntryCell)?.Placeholder == "Sisesta tel. number") as EntryCell;
            if (phoneNumberCell != null)
            {
                var call = CrossMessaging.Current.PhoneDialer;
                if (call.CanMakePhoneCall)
                {
                    call.MakePhoneCall(phoneNumberCell.Text);
                };
            }
        }
    }
}