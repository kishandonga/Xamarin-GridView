using System;
using System.Threading.Tasks;
using Xamarin.Forms;

/*
 * Developed By : Kishan Donga, (Software Engineer Mobility)
 * */

namespace DLab.Widgets
{
    public abstract class GridAdapter
    {
        // NotifyDataSetChange event handler
        private event EventHandler notifyChanged;

        // NotifyDataSetChangeByIndex event handler
        private event EventHandler<int> notifyChangedByIndex;

        // AttachToEvent
        public void AttachToEvent(EventHandler attach)
        {
            notifyChanged += attach;
        }

        // AttachToEvent
        public void AttachToEvent(EventHandler<int> attach)
        {
            notifyChangedByIndex += attach;
        }

        // to pass count of the item or your data source to the GridView
        public abstract int GetCount();

        // to pass the item in your GetView method
        public abstract object GetItem(int position);

        // GetView passing data from the GridView to the Custom Adapter 
        // And Adapter to the GridView
        public abstract View GetView(int position, View convertView, View parentView);

        public abstract Task<bool> InAnimation(View view);

        public abstract Task<bool> OutAnimation(View view);

        // Notify to the GridView, When Item Source change
        public void NotifyDataSetChanged()
        {
            notifyChanged?.Invoke(this, EventArgs.Empty);
        }

        // Notify to the GridView, When Item Source change at particular index 
        public void NotifyDataSetChanged(int position)
        {
            notifyChangedByIndex?.Invoke(this, position);
        }
    }
}
