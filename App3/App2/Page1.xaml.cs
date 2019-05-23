using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page1 : ContentPage
	{
        private App2ViewModel ViewModel;

        public Page1 ()
		{
			InitializeComponent ();
            Init();
        }

        private void Init()
        {
            ViewModel = App2ViewModel.Instance;
            this.BindingContext = ViewModel; //App2ViewModel l'objet     
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var Serveur = new ServicesWeb();
            Debug.WriteLine("Go!");
            Serveur.GetInfosAsync(1);
        }
    }
}