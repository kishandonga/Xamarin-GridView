using DLab.Widgets;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamGridView.Pages;

namespace XamGridView.Adapter.DataGrid
{
    public class DataGridAdapter : GridAdapter
    {
        private List<Dictionary<string, object>> lstData;
        private int selectedIndex = -1;

        // get your list of the Data
        public DataGridAdapter(List<Dictionary<string, object>> data)
        {
            lstData = data;
        }

        public override int GetCount()
        {
            return lstData.Count;
        }

        public override object GetItem(int position)
        {
            return lstData[position];
        }

        public override View GetView(int position, View convertView, View parentView)
        {
            Dictionary<string, object> model = lstData[position];
            DataGridViewHolder holder;

            if (convertView == null)
            {
                holder = new DataGridViewHolder(model.Count);
                convertView = holder.getRootView();
                convertView.BindingContext = holder;
            }
            else
            {
                holder = (DataGridViewHolder)convertView.BindingContext;
            }

            holder.data = model;
            StackLayout stack = holder.container;

            for(int i = 0; i <model.Count; i++)
            {
                Label lbl = (Label)stack.Children[i];
                lbl.Text = model.Values.ElementAt(i).ToString();
            }

            if (position == selectedIndex)
            {
                holder.slLayoutMain.BackgroundColor = DataGridResource.ROW_CELL_ACTIVE_BACKGROUND_COLOR;
            }
            else
            {
                holder.slLayoutMain.BackgroundColor = DataGridResource.ROW_CELL_DEFAULT_BACKGROUND_COLOR;
            }

            return convertView;
        }

        public override Task<bool> InAnimation(View view)
        {
            return Task.FromResult(true);
        }

        public override Task<bool> OutAnimation(View view)
        {
            return Task.FromResult(true);
        }

        // select item and notify to the view
        public void SetSelectedItem(int itemPosition)
        {
            selectedIndex = itemPosition;
            NotifyDataSetChanged();
        }
    }
}
