using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App2
{
    public partial class MainPage : ContentPage
    {
        private bool Fin = false;
        private int Joueur1 = 1;
        private int Joueur2 = 10;
        private int JoueurCourant;
        private int TauxReussite;
        private int[,] jeu = new int[3, 3];
        private BoutonMorp[,] Boutons = new BoutonMorp[3, 3];
        private App2ViewModel ViewModel;

        public MainPage()
        {
            
            InitializeComponent();
            Initialisation();
            ViewModel = App2ViewModel.Instance;
            this.BindingContext = ViewModel; //App2ViewModel l'objet
            ViewModel.Compteur = 0;
        }


        private void Initialisation()
        {
            B_Rejouer.Clicked += B_Rejouer_Clicked;
            JoueurCourant = Joueur1;
            TauxReussite = (int)slider.Value;
            CreateButtons();

        }

        private void CreateButtons()
        {
            BoutonMorp b;
            viderTableuJeu();

            for (int ligne = 3; ligne < 6; ligne++)
            {
                for (int col = 0; col < 3; col++)
                {
                    b = new BoutonMorp();
                    //b.BorderColor = Color.Transparent;
                    //b.BackgroundColor = Color.Transparent;
                    b.Rows = ligne - 3;
                    b.Cols = col;
                    //b.Text = "";
                    //b.Image = ImageSource.FromFile("morpion2.png");
                    b.Source = null;
                    b.Aspect = Aspect.Fill;
                    
                    b.GestureRecognizers.Add(new TapGestureRecognizer(OnTap));

                    //b.Clicked += B_Clicked;
                    Boutons[b.Rows, b.Cols] = b;
                    Grille.Children.Add(b, col, ligne);
                }
            }
        }


        private void viderTableuJeu()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    jeu[i, j] = 0;
                }
            }
        }

        private void PlacerPion(int row, int col)
        {
            Boutons[row, col].Source = "Rond.png";
            jeu[row, col] = JoueurCourant;
            JoueurCourant = Joueur1;
        }

        private bool Gagne(int somme)
        {
            if (somme == 3 || somme == 30)
            {
                Fin = true;
                if(somme == 3)
                {
                    lJoueur.Text = "Vous avez Gagné!";
                }
                else
                {
                    lJoueur.Text = "Vous avez perdu...";
                }

                return true;
            }

            return false;
        }

        private void verifRegles()
        {
            int somme = 0;
            // Test lignes
            for(int i = 0; i<3; i++)
            {
                somme = 0;
                for (int j = 0; j < 3; j++)
                {
                    somme += jeu[i,j];

                }
                if (Gagne(somme))
                {
                    return;
                }
            }

            // Test colones
            for (int i = 0; i < 3; i++)
            {
                somme = 0;
                for (int j = 0; j < 3; j++)
                {
                    somme += jeu[j, i];
                }
                if (Gagne(somme))
                {
                    return;
                }
            }

            //Test premiere diagonale
            somme = 0;
            for (int j = 0; j < 3; j++)
            {
                somme += jeu[j, j];
            }
            if (Gagne(somme))
            {
                return;
            }

            //Test deuxieme diagonale
            somme = jeu[2, 0] + jeu[1, 1] + jeu[0, 2];
            if (Gagne(somme))
            {
                return;
            }

            //Test si Match null
            bool complet = true;

            for (int ligne = 0; ligne < 3; ligne++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (jeu[ligne, col] == 0)
                    {
                        complet = false;
                    }
                }
            }

            if (complet)
            {
                lJoueur.Text = "Match Null";
                Fin = true;
            }
        }

        private void jouerOrdi()
        {
            if(Fin)
            {
                return;
            }

            Random randErreur = new Random();
            int taux = randErreur.Next(100);


            if (taux < TauxReussite)
            {
                //
                //Gagne s'il peut
                //
                int somme = 0;
                // Test lignes 
                for (int i = 0; i < 3; i++)
                {
                    somme = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        somme += jeu[i, j];

                    }

                    if (somme == 20)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (jeu[i, j] == 0)
                            {
                                PlacerPion(i, j);
                                return;
                            }
                        }
                    }
                }

                somme = 0;
                // Test Colonnes
                for (int i = 0; i < 3; i++)
                {
                    somme = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        somme += jeu[j, i];
                    }

                    if (somme == 20)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (jeu[j, i] == 0)
                            {
                                PlacerPion(j, i);
                                return;
                            }
                        }
                    }
                }

                //Test premiere diagonale
                somme = 0;
                for (int j = 0; j < 3; j++)
                {
                    somme += jeu[j, j];
                }
                if (somme == 20)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (jeu[j, j] == 0)
                        {
                            PlacerPion(j, j);
                            return;
                        }
                    }
                }

                //Test deuxieme diagonale
                somme = jeu[2, 0] + jeu[1, 1] + jeu[0, 2];
                if (somme == 20)
                {
                    if (jeu[2, 0] == 0)
                    {
                        PlacerPion(2, 0);
                        return;
                    }

                    if (jeu[1, 1] == 0)
                    {
                        PlacerPion(1, 1);
                        return;
                    }

                    if (jeu[0, 2] == 0)
                    {
                        PlacerPion(2, 0);
                        return;
                    }
                }



                //
                //Bloque jouer s'il peut gagner
                //
                somme = 0;
                // Test lignes 
                for (int i = 0; i < 3; i++)
                {
                    somme = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        somme += jeu[i, j];

                    }

                    if (somme == 2)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (jeu[i, j] == 0)
                            {
                                PlacerPion(i, j);
                                return;
                            }
                        }
                    }
                }

                somme = 0;
                // Test Colonnes
                for (int i = 0; i < 3; i++)
                {
                    somme = 0;
                    for (int j = 0; j < 3; j++)
                    {
                        somme += jeu[j, i];

                    }

                    if (somme == 2)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (jeu[j, i] == 0)
                            {
                                PlacerPion(j, i);
                                return;
                            }
                        }
                    }
                }
                //Test premiere diagonale
                somme = 0;
                for (int j = 0; j < 3; j++)
                {
                    somme += jeu[j, j];
                }
                if (somme == 2)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (jeu[j, j] == 0)
                        {
                            PlacerPion(j, j);
                            return;
                        }
                    }
                }

                //Test deuxieme diagonale
                somme = jeu[2, 0] + jeu[1, 1] + jeu[0, 2];
                if (somme == 2)
                {
                    if (jeu[2, 0] == 0)
                    {
                        PlacerPion(0, 0);
                        return;
                    }
                    else if (jeu[1, 1] == 0)
                    {
                        PlacerPion(1, 1);
                        return;
                    }
                    else
                    {
                        PlacerPion(0, 2);
                        return;
                    }
                }
            }

            // l'Ordi Joue au hazard si aucun cas precedent
            bool pass = false;
            while (!pass)
            {
                Random rnd = new Random();
                int row = rnd.Next(3);
                int col = rnd.Next(3);
                if (jeu[row, col] == 0)
                {
                    PlacerPion(row, col);
                    return;
                }
            }

        }

        private void OnTap(View arg1, object sender)
        {
            var bouton = arg1 as BoutonMorp;
            
            if (Fin == false)
            {
                if (jeu[bouton.Rows, bouton.Cols] == 0)
                {
                    if (JoueurCourant == Joueur1)
                    {
                        bouton.Source = "Croix.png";
                        jeu[bouton.Rows, bouton.Cols] = JoueurCourant;
                        JoueurCourant = Joueur2;
                        verifRegles();
                        jouerOrdi();
                        verifRegles();
                    }
                }
            }
        }

        //private void B_Clicked(object sender, EventArgs e)
        //{
        //    var bouton = sender as BoutonMorp;
        //    if (Fin == false)
        //    {
        //        if (bouton.Text == "")
        //        {
        //            int row =  bouton.Rows;
        //            int column = bouton.Cols;

        //            if (JoueurCourant == Joueur1)
        //            {
        //                bouton.Text = "X";
        //                jeu[row, column] = JoueurCourant;
        //                JoueurCourant = Joueur2;
        //                verifRegles();
        //                jouerOrdi();
        //                verifRegles();
        //            }
        //        }
        //    }
        //}


        private void B_Rejouer_Clicked(object sender, EventArgs e)
        {
            Fin = false;
            lJoueur.Text = ". . . . .";
            JoueurCourant = Joueur1;
            viderTableuJeu();
            TauxReussite = (int)slider.Value;

            ViewModel.Compteur++;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {

                    Boutons[i, j].Source = null;
                }
            }
        }

        void SliderEvent(object sender, ValueChangedEventArgs args)
        {
            int value = (int)args.NewValue;
            TauxReussite = value;
            Dificulte.Text = "Dificulté : " + value.ToString();
        }

        async private void PageSuivante_Clicked(object sender, EventArgs e)
        {
            var Page = new Page1();
            // On la pose sur la pille
            await Navigation.PushAsync(Page);
        }
    }
}
