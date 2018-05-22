using DLab.Widgets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public AlphabetGridAdapter(List<string> lstAlphabets) 
        {
            this.lstAlphabets = lstAlphabets;
        }

        public override int GetCount()
        {
            return lstAlphabets.Count;
        }

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

        public void AddNewList(List<string> lstImages)
        {
            this.lstAlphabets = lstImages;
            NotifyDataSetChanged();
        }

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

        public void RemoveByIndex(int index)
        {
            if(index >= 0 && index < GetCount())
            {
                lstAlphabets.RemoveAt(index);
                NotifyDataSetChanged(index);
                Debug.WriteLine("Index :- " + index);
            }
        }

        public void AddData(string data)
        {
            lstAlphabets.Add(data);
            int index = lstAlphabets.IndexOf(data);
            NotifyDataSetChanged(index);
            Debug.WriteLine("Index :- " + index);
        }

        public void SetSelectedItem(int itemPosition)
        {
            mSelectedPosition = itemPosition;
            NotifyDataSetChanged();
        }
    }
}
