using DLab.Forms.Events;
using DLab.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamGridView.Adapter.DataGrid;
using XamGridView.Helper;

namespace XamGridView.Pages
{
    public static class DataGridResource
    {
        public static int HEADER_WIDTH = 150;
        public static int HEADER_HEIGHT = 35;
        public static Color HEADER_BACKGROUND_COLOR = Color.FromHex("#A9A9A9");
        public static Color HEADER_BOARDER_COLOR = Color.FromHex("#000000");
        public static int ROW_CELL_WIDTH = 150;
        public static int ROW_CELL_HEIGHT = 50;
        public static Color ROW_CELL_ACTIVE_BACKGROUND_COLOR = Color.FromHex("#E7E7E7");
        public static Color ROW_CELL_DEFAULT_BACKGROUND_COLOR = Color.FromHex("#FFFFFF");
        public static Color ROW_CELL_BOARDER_COLOR = Color.FromHex("#000000");
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataGridPage : ContentPage
    {
        private DataGridAdapter adapter;
        private DataGridViewModel viewModel;

        public DataGridPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
            viewModel = new DataGridViewModel();
            BindingContext = viewModel;

            var list = DataSource.GetOrders();
            var headerList = GetHeader(list);

            GridView grid = new GridView() {
                NumColumns = 1,
                ScrollToEnd = true,
                ColumnSpacing = 0,
                RowSpacing = 1
            };

            slView.Children.Add(new DataGridHeaderView(headerList).getRootView());
            slView.Children.Add(grid);

            adapter = new DataGridAdapter(list);
            grid.ItemTapped += Handle_TapEvent;
            grid.ItemSource = adapter;

            //viewModel.IsVisible = true;
        }

        public static List<string> GetHeader(List<Dictionary<string, object>> dList)
        {
            List<string> list = new List<string>();
            Dictionary<string, object> model = dList[0];
            for (int i = 0; i < model.Count; i++)
            {
                list.Add(model.Keys.ElementAt(i));
            }

            return list;
        }

        private void Handle_TapEvent(object s, GridEventArgs arg)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    if (!pbIndicator.IsVisible)
                    {
                        // get the selected view holder from the Event arguments
                        DataGridViewHolder holder = (DataGridViewHolder)arg.view.BindingContext;
                        Dictionary<string, object> model = holder.data;
                        adapter.SetSelectedItem(arg.position);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            });
        }
    }

    public class DataGridViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isVisible = false;
         
        public bool IsVisible
        {
            get
            {
                return isVisible;
            }
            set
            {
                isVisible = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}