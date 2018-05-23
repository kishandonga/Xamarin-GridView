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

            // Note : here order not mandatory because your list empty
            adapter = new AlphabetGridAdapter(new List<string>());
            gridLayout.ItemSource = adapter;
            gridLayout.ItemTapped += Handle_TapEvent;

            // clear list
            btnClear.Clicked += delegate
            {
                adapter.AddNewList(new List<string>());
                letter = 'A';
            };

            // remove random item
            btnRemove.Clicked += delegate
            {
                adapter.RemoveByRandom();
            };

            // to add the new item
            btnAdd.Clicked += delegate
            {
                if (letter < 91)
                {
                    adapter.AddData(letter.ToString());
                    letter++;
                }
            };
        }

        // handle tap event 
        private void Handle_TapEvent(object s, GridEventArgs arg)
        {
            Device.BeginInvokeOnMainThread(() => {
                AlphabetViewHolder holder = (AlphabetViewHolder)arg.view.BindingContext;
                adapter.SetSelectedItem(arg.position);
            });
        }
    }
}