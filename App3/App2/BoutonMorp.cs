using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App2
{
    class BoutonMorp : Image
    {
        private int rows;
        private int cols;

        public int Cols { get => cols; set => cols = value; }
        public int Rows { get => rows; set => rows = value; }
    }
}
