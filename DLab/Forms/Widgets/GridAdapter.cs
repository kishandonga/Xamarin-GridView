using System;
using Xamarin.Forms;

/*
 * Developed By : Kishan Donga, (Software Engineer Mobility)
 * Email Id : kishandonga.92@gmail.com
 * Contact : (+91)9712598499
 * Country : India
 * */

namespace DLab.Widgets
{
    public abstract class GridAdapter
    {
        private event EventHandler notifyChanged;

        private event EventHandler<int> notifyChangedByIndex;

        public void AttachToEvent(EventHandler attach)
        {
            notifyChanged += attach;
        }

        public void AttachToEvent(EventHandler<int> attach)
        {
            notifyChangedByIndex += attach;
        }

        public abstract int GetCount();

        public abstract object GetItem(int position);

        public abstract View GetView(int position, View convertView, View parentView);

        public void NotifyDataSetChanged()
        {
            notifyChanged?.Invoke(this, EventArgs.Empty);
        }

        public void NotifyDataSetChanged(int position)
        {
            notifyChangedByIndex?.Invoke(this, position);
        }
    }
}
