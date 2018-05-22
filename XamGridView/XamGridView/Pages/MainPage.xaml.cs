using Xamarin.Forms;
using XamGridView.Pages;

/*
 * Developed By : Kishan Donga, (Software Engineer Mobility)
 * Email Id : kishandonga.92@gmail.com
 * Contact : (+91)9712598499
 * Country : India
 * */

namespace XamGridView
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            btnImagesGrid.Clicked += delegate
            {
                Navigation.PushAsync(new GridImagesPage());
            };

            btnAlphabetGrid.Clicked += delegate
            {
                Navigation.PushAsync(new AlphabetPage());
            };
            
        }
    }
}
