using DLab.Widgets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

/*
 * Developed By : Kishan Donga, (Software Engineer Mobility)
 * Email Id : kishandonga.92@gmail.com
 * Contact : (+91)9712598499
 * Country : India
 * */

namespace XamGridView.Adapter.Alphabet
{
    // The custom Adapter
    public class AlphabetGridAdapter : GridAdapter
    {
        private List<string> lstAlphabets;
        private int mSelectedPosition = -1;

        // get your list of the Data
        public AlphabetGridAdapter(List<string> lstAlphabets) 
        {
            this.lstAlphabets = lstAlphabets;
        }

        // return the count
        public override int GetCount()
        {
            return lstAlphabets.Count;
        }

        // get the item at perticular position
        public override object GetItem(int position)
        {
            return lstAlphabets[position];
        }

        public override View GetView(int position, View convertView, View parentView)
        {
            AlphabetViewHolder holder;

            if (convertView == null)
            {
                holder = new AlphabetViewHolder();
                convertView = holder.getRootView();
                convertView.BindingContext = holder;
            }
            else
            {
                holder = (AlphabetViewHolder)convertView.BindingContext;
            }
          
            holder.lbl.Text = GetItem(position).ToString();

            if (position == mSelectedPosition)
            {
                holder.frame.BackgroundColor = Color.LightGray;
            }
            else
            {
                holder.frame.BackgroundColor = Color.White;
            }

            return convertView;
        }

        // change the list and notify to the view
        public void AddNewList(List<string> lstImages)
        {
            this.lstAlphabets = lstImages;
            NotifyDataSetChanged();
        }

        // remove item into the list and notify to the view by index
        public void RemoveByRandom()
        {
            int index = new Random().Next(0, GetCount());
            if (index >= 0 && index < GetCount())
            {
                lstAlphabets.RemoveAt(index);
                NotifyDataSetChanged(index);
                Debug.WriteLine("Index :- " + index);
            }
        }

        // remove item into the list and notify to the view by index
        public void RemoveByIndex(int index)
        {
            if(index >= 0 && index < GetCount())
            {
                lstAlphabets.RemoveAt(index);
                NotifyDataSetChanged(index);
                Debug.WriteLine("Index :- " + index);
            }
        }

        // add item into the list and notify to the view
        public void AddData(string data)
        {
            lstAlphabets.Add(data);
            int index = lstAlphabets.IndexOf(data);
            NotifyDataSetChanged(index);
            Debug.WriteLine("Index :- " + index);
        }

        // select item and notify to the view
        public void SetSelectedItem(int itemPosition)
        {
            mSelectedPosition = itemPosition;
            NotifyDataSetChanged();
        }

        public override Task<bool> OutAnimation(View view)
        {
            return view.FadeTo(1, 500);
        }

        public override Task<bool> InAnimation(View view)
        {
            view.Opacity = 0;
            return Task.FromResult(true);
        }
    }
}
