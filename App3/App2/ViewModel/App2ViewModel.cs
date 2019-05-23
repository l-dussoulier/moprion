using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace App2
{
    class App2ViewModel : INotifyPropertyChanged
    {
        private int compteur;
        //public int Compteur { get => compteur; set => compteur = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        //Le Cadenas
        private static readonly object padlock = new object();

        //l'instance
        private static App2ViewModel instance = null;

        private App2ViewModel() // Constructeur privé
        {
        }

        public static App2ViewModel Instance
        {
            get
            {
                if(instance == null)
                {
                    lock (padlock)
                    {
                        if(instance == null)
                        {
                            instance = new App2ViewModel();
                        }
                    }
                }
                return instance;
            }
        }

        public int Compteur
        {
            get
            {
                return compteur;
            }
            set
            {
                if(compteur != value)
                {
                    compteur = value;

                    //if(PropertyChanged != null)
                    //{
                    //    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Compteur"));
                    //}
                    //
                    //version simplifié, le ? test si est null

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Compteur"));
                }
            }
        }
    }
}
