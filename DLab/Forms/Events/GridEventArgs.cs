using System;
using Xamarin.Forms;

/*
 * Developed By : Kishan Donga, (Software Engineer Mobility)
 * Email Id : kishandonga.92@gmail.com
 * Contact : (+91)9712598499
 * Country : India
 * */

namespace DLab.Forms.Events
{
    public class GridEventArgs : EventArgs
    {
        public int position { get; set; }
        public View view { get; set; }
    }
}
