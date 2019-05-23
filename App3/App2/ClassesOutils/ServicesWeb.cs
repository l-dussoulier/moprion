using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace App2
{
    class ServicesWeb
    {
        private HttpClient Client;

        public ServicesWeb()
        {
            Client = new HttpClient();
            Client.MaxResponseContentBufferSize = 20000;
        }

        // async on lance un nouveau thread 
        public async void GetInfosAsync(int rang)
        {
            Uri uri = new Uri("http://www.kroko.ovh/~jerome/API_S4/fonc1.php?rang="+rang.ToString());

            var ReponseServeur = await Client.GetAsync(uri);

            if (ReponseServeur.IsSuccessStatusCode)
            {
                var Donnes = await ReponseServeur.Content.ReadAsStringAsync();
                Debug.WriteLine(Donnes);
            }
            else
            {
                Debug.WriteLine("ça marche app");
            }
        }

    }
}