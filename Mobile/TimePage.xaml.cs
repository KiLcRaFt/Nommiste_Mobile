using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimePage : ContentPage
    {
        public TimePage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            lbl.TextColor= Color.FromRgb(Convert.ToInt32(Random.Equals(0, 999)), Convert.ToInt32(Random.Equals(0, 999)), Convert.ToInt32(Random.Equals(0, 999)));
        }

        bool flag= false;

        public async void NaitaAeg()
        { 
            while(flag) 
            {
                lbl.Text = DateTime.Now.ToString("M");
                Time_run_btn.Text =DateTime.Now.ToString("T");
                await Task.Delay(1000);
            }
        }

        private void Time_run_btn_Clicked(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
            }
            else
            {
                flag= true;
                NaitaAeg();
            }
        }

        private async void Close_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}