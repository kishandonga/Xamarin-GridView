using System.Collections.Generic;
using Xamarin.Forms;
using XamGridView.Pages;

namespace XamGridView.Adapter.DataGrid
{
    public class DataGridViewHolder
    {
        public StackLayout slLayoutMain;
        public StackLayout container;
        public Dictionary<string, object> data;

        public DataGridViewHolder(int size)
        {
            slLayoutMain = new StackLayout() {
                Orientation = StackOrientation.Vertical,
                Spacing = 0
            };

            container = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 0
            };

            for (int i = 0; i < size; i++)
            {
                Label lbl = new Label();
                lbl.WidthRequest = DataGridResource.ROW_CELL_WIDTH;
                lbl.HeightRequest = DataGridResource.ROW_CELL_HEIGHT;
                lbl.Text = "N/A";
                lbl.HorizontalTextAlignment = TextAlignment.Center;
                lbl.VerticalTextAlignment = TextAlignment.Center;

                container.Children.Add(lbl);
            }

            StackLayout BottomBorder = new StackLayout()
            {
                BackgroundColor = DataGridResource.ROW_CELL_BOARDER_COLOR,
                HeightRequest = 0.5d,
                Spacing = 0
            };

            slLayoutMain.Children.Add(container);
            slLayoutMain.Children.Add(BottomBorder);
        }

        // to get the root view of the your cell
        public View getRootView()
        {
            return slLayoutMain;
        }
    }
}
