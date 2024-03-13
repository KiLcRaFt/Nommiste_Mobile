using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Messaging;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Web;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;

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
            btn_sms.Clicked += Btn_sms_Clicked;

            btn_email = new Button
            {
                Text = "Kirjuta",
                WidthRequest = 120
            };
            btn_email.Clicked += Btn_email_Clicked;



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
                    },

                    new TableSection("Tekst")
                    {   
                        new EntryCell
                        {
                            Placeholder = "Sisesta texti",
                            Keyboard=Keyboard.Text
                        }
                    }
                }
            };
            Content = tableview;
        }

        private async void Btn_email_Clicked(object sender, EventArgs e)
        {
            //var mail = CrossMessaging.Current.EmailMessenger;
            //if (mail.CanSendEmail)
            //{
            //    mail.SendEmail(((EntryCell)tableview.Root[1][0]).Text, "Tervitus!", ((EntryCell)tableview.Root[3][0]).Text);
            //}

            try
            {
                var email = ((EntryCell)tableview.Root[1][1]).Text;
                if (!string.IsNullOrWhiteSpace(email))
                {
                    var subject = ((EntryCell)tableview.Root[0][0]).Text;
                    var body = ((EntryCell)tableview.Root[3][0]).Text;
                    var uri = new Uri($"mailto:{email}?subject={subject}&body={body}");
                    await Launcher.OpenAsync(uri);

                }
                else
                {
                    await DisplayAlert("Viga", "palun sisesta email", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", $"Viga emeiliga: {ex.Message}", "Ok");
            }
        }

        private void Btn_sms_Clicked(object sender, EventArgs e)
        {
            var sms = CrossMessaging.Current.SmsMessenger;
            if (sms.CanSendSms)
            {
                sms.SendSms(((EntryCell)tableview.Root[1][0]).Text, ((EntryCell)tableview.Root[3][0]).Text);
            }
        }

        private async void Btn_hel_Clicked(object sender, EventArgs e)
        {
            try
            {
                var number = ((EntryCell)tableview.Root[1][0]).Text;
                if (!string.IsNullOrWhiteSpace(number))
                {
                    await Launcher.OpenAsync(new Uri("tel:" + number));
                }
                else
                {
                    await DisplayAlert("Viga", "Palun siseta tel. number", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", $"Tekkis tõrge helistamisel: {ex.Message}", "Ok");
            }
        }
    }
}