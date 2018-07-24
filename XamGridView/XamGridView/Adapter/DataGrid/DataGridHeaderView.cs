using System.Collections.Generic;
using Xamarin.Forms;
using XamGridView.Pages;

namespace XamGridView.Adapter.DataGrid
{
    public class DataGridHeaderView
    {
        public StackLayout slLayoutMain;
        
        public DataGridHeaderView(List<string> list)
        {
            slLayoutMain = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 0
            };

            StackLayout container = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 0
            };

            for (int i = 0; i < list.Count; i++)
            {
                Label lbl = new Label();
                lbl.BackgroundColor = DataGridResource.HEADER_BACKGROUND_COLOR;
                lbl.WidthRequest = DataGridResource.HEADER_WIDTH;
                lbl.HeightRequest = DataGridResource.HEADER_HEIGHT;
                lbl.Text = list[i];
                lbl.HorizontalTextAlignment = TextAlignment.Center;
                lbl.VerticalTextAlignment = TextAlignment.Center;

                container.Children.Add(lbl);
            }

            StackLayout BottomBorder = new StackLayout()
            {
                BackgroundColor = DataGridResource.HEADER_BOARDER_COLOR,
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
