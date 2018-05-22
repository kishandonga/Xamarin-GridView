using DLab.Forms.Events;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamGridView.Adapter.Alphabet;

namespace XamGridView.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlphabetPage : ContentPage
    {
        private char letter = 'A';
        AlphabetGridAdapter adapter;
        public AlphabetPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);

            adapter = new AlphabetGridAdapter(new List<string>());
            gridLayout.ItemSource = adapter;
            gridLayout.ItemTapped += Handle_TapEvent;

            btnClear.Clicked += delegate
            {
                adapter.AddNewList(new List<string>());
                letter = 'A';
            };

            btnRemove.Clicked += delegate
            {
                adapter.RemoveByRandom();
            };

            btnAdd.Clicked += delegate
            {
                if (letter < 91)
                {
                    adapter.AddData(letter.ToString());
                    letter++;
                }
            };
        }

        private void Handle_TapEvent(object s, GridEventArgs arg)
        {
            Device.BeginInvokeOnMainThread(() => {
                AlphabetViewHolder holder = (AlphabetViewHolder)arg.view.BindingContext;
                adapter.SetSelectedItem(arg.position);
            });
        }
    }
}