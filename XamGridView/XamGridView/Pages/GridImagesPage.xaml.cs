using DLab.Forms.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamGridView.Adapter.Images;

/*
 * Developed By : Kishan Donga, (Software Engineer Mobility)
 * Email Id : kishandonga.92@gmail.com
 * Contact : (+91)9712598499
 * Country : India
 * */

namespace XamGridView.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GridImagesPage : ContentPage
    {
        public GridImagesPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);

            List<string> lstImages = new List<string>();
            for(int i = 20; i < 50; i++)
            {
                lstImages.Add("https://unsplash.it/300/300?image=" + i);
            }

            // passing the list of the images 
            ImagesGridAdapter adapter = new ImagesGridAdapter(lstImages);
            // Note : Order is mandatory 
            // first need to initialize the Tap event then after give the item source 
            // because if you first give item source then that time your event will ne consider as null
            // so it will be not detected gesture event 
            gridLayout.ItemTapped += Handle_TapEvent;
            gridLayout.ItemSource = adapter;
            
        }

        private void Handle_TapEvent(object s, GridEventArgs arg)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    // get the selected view holder from the Event arguments
                    // arg.view.BindingContext => to get the ViewHolder
                    // arg.position
                    ImagesGridViewHolder holder = (ImagesGridViewHolder)arg.view.BindingContext;
                    Image img = holder.img;
                    await img.ScaleTo(0.8, 50);
                    img.Scale = 1;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            });
        }
    }
}