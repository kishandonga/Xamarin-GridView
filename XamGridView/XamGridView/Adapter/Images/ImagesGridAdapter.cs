using DLab.Widgets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

/*
 * Developed By : Kishan Donga, (Software Engineer Mobility)
 * Email Id : kishandonga.92@gmail.com
 * Contact : (+91)9712598499
 * Country : India
 * */

namespace XamGridView.Adapter.Images
{
    public class ImagesGridAdapter : GridAdapter
    {
        private List<string> lstImages;

        // get your list of the Data
        public ImagesGridAdapter(List<string> lstImages)
        {
            this.lstImages = lstImages;
        }

        // return the count
        public override int GetCount()
        {
            return lstImages.Count;
        }

        // get the item at perticular position
        public override object GetItem(int position)
        {
            return lstImages[position];
        }

        // initialize your view in the convertview
        public override View GetView(int position, View convertView, View parentView)
        {
            ImagesGridViewHolder holder;

            if (convertView == null)
            {
                holder = new ImagesGridViewHolder();
                convertView = holder.getRootView();
                convertView.BindingContext = holder;
            }
            else
            {
                holder = (ImagesGridViewHolder)convertView.BindingContext;
            }

            holder.img.Source = new UriImageSource
            {
                Uri = new Uri(GetItem(position).ToString()),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(1, 0, 0, 0)
            };

            return convertView;
        }

        public override Task<bool> OutAnimation(View view)
        {
            return Task.FromResult(true);
        }

        public override Task<bool> InAnimation(View view)
        {
            return Task.FromResult(true);
        }
    }
}
