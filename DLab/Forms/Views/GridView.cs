using System;
using System.Collections.Generic;
using Xamarin.Forms;
using DLab.Widgets;
using DLab.Forms.Events;

/*
 * Developed By : Kishan Donga, (Software Engineer - Mobility)
 * */

namespace DLab.Views
{
    public class GridView : ScrollView
    {
        // simple grid of the xamarin forms
        private static Grid mGrid = null;
        // custom abstract class for the data transaction 
        private GridAdapter adapter = null;
        // return the no of the Row
        private int rowCount = 0;
        // return the total item
        private int itemCount = 0;
        // return the position of the item
        private int position = 0;
        // store the old view of the particular position 
        private Dictionary<int, View> viewDict = new Dictionary<int, View>();
        // too get the Item Tap events
        public EventHandler<GridEventArgs> ItemTappedEvent = null; 

        public GridView()
        {
            mGrid = new Grid();
            Content = mGrid;
            // if new item arives then handle automatic scrolling of the View
            mGrid.SizeChanged += Handle_SizeChanged;
        }

        // For, the Column
        public static readonly BindableProperty NumColumnsProperty = BindableProperty.Create(
        "NumColumns",
        typeof(int),
        typeof(GridView),
        3,
        propertyChanged: (bindable, oldvalue, newvalue) =>
        {
            ((GridView)bindable).InvalidateLayout();
        });

        public int NumColumns
        {
            set { SetValue(NumColumnsProperty, value); }
            get { return (int)GetValue(NumColumnsProperty); }
        }

        // if you want to allow ScrollToEnd then true else false
        // if true - then it will be automatically scroll view when height of the view increases
        // if false then not scrolling even any change occurs in height
        public static readonly BindableProperty ScrollToEndProperty = BindableProperty.Create(
        "ScrollToEnd",
        typeof(bool),
        typeof(GridView),
        false,
        propertyChanged: (bindable, oldvalue, newvalue) =>
        {
            ((GridView)bindable).InvalidateLayout();
        });

        public bool ScrollToEnd
        {
            set { SetValue(ScrollToEndProperty, value); }
            get { return (bool)GetValue(ScrollToEndProperty); }
        }

        // Specify Space between two column, default is 6
        public static readonly BindableProperty ColumnSpacingProperty = BindableProperty.Create(
        "ColumnSpacing",
        typeof(double),
        typeof(GridView),
        6.0,
        propertyChanged: (bindable, oldvalue, newvalue) =>
        {
            mGrid.ColumnSpacing = (double)newvalue;
            ((GridView)bindable).InvalidateLayout();
        });

        public double ColumnSpacing
        {
            set { SetValue(ColumnSpacingProperty, value); }
            get { return (double)GetValue(ColumnSpacingProperty); }
        }

        // Specify Space between two row, default is 6
        public static readonly BindableProperty RowSpacingProperty = BindableProperty.Create(
        "RowSpacing",
        typeof(double),
        typeof(GridView),
        6.0,
        propertyChanged: (bindable, oldvalue, newvalue) =>
        {
            mGrid.RowSpacing = (double)newvalue;
            ((GridView)bindable).InvalidateLayout();
        });

        public double RowSpacing
        {
            set { SetValue(RowSpacingProperty, value); }
            get { return (double)GetValue(RowSpacingProperty); }
        }

        // to get the row count
        public int NumRow
        {
            get { return rowCount + 1; }
        }

        // to get the item count
        public int ItemCount
        {
            get { return (itemCount + 1); }
        }

        // to get and set ItemSource 
        // Instance of the your custom adapter
        public GridAdapter ItemSource
        {
            set { setAdapter(value); }
            get { return adapter; }
        }

        // to get and set Item Tap evant
        public EventHandler<GridEventArgs> ItemTapped
        {
            set { ItemTappedEvent = value; }
            get { return ItemTappedEvent; }
        }

        // private method used by ItemSource
        private void setAdapter(GridAdapter adapter)
        {
            if (NumColumns <= 0)
            {
                throw new ArgumentException("NumColumns must be geter then zero!");
            }

            this.adapter = adapter ?? throw new ArgumentNullException("Adapter can not be null!");
            // Attach NotifyDataSetChange event
            adapter.AttachToEvent(Handle_notifyDataSetChange);
            // Attach NotifyDataSetChangeByIndex event
            adapter.AttachToEvent(Handle_notifyDataSetChangeByIndex);
            // set column and row definations 
            // also change in the definations
            setDefinations();
            // load view at initial state
            loadCells();
        }

        private void Handle_notifyDataSetChange(object o, EventArgs arg)
        {
            if(mGrid != null)
            {
                // clear whole grid
                ClearGrid();
                // set column and row definations 
                // also change in the definations
                setDefinations();
                for (position = 0; position <= itemCount; position++)
                {
                    // get old view from the Dictionary 
                    View oldView = viewDict[position];
                    // pass into the getview method for changes 
                    View v = adapter.GetView(position, oldView, mGrid);
                    viewDict.Remove(position);
                    // re-write into the grid
                    AddViewInGrid(v, position);
                }
            }
        }

        private void Handle_notifyDataSetChangeByIndex(object o, int index)
        {
            if (mGrid != null)
            {
                // set column and row definations 
                // also change in the definations
                setDefinations();
                View v;

                // to check already exist or not
                // if exist then this view and all the below view 
                // refreshed 
                // Note : Above view or cell can't affected, when you use this method for the 
                // data change with particular index
                if (viewDict.ContainsKey(index))
                {
                    // remove view or cell form the Grid
                    for(position = GetChildCount(); position >= index; position--)
                    {
                        RemoveViewInGrid(viewDict[position]);
                    }

                    // re-write the grid
                    for (position = index; position <= itemCount; position++)
                    {
                        View oldView = viewDict[position];
                        v = adapter.GetView(position, oldView, mGrid);
                        viewDict.Remove(position);
                        AddViewInGrid(v, position);
                    }
                }
                else
                {
                    // cell consider as new one and will be added at the 
                    // end of the grid 
                    position = index;
                    v = adapter.GetView(position, null, mGrid);
                    AddViewInGrid(v, position);
                } 
            }
        }

        // Automatically handle the scrolling
        private async void Handle_SizeChanged(object o, EventArgs arg)
        {
            if (ScrollToEnd)
            {
                double width = mGrid.Width;
                double height = mGrid.Height;
                await ScrollToAsync(width, height, false);
            }
        }

        // to define row and no of column
        private void setDefinations()
        {
            itemCount = adapter.GetCount() - 1;
            rowCount = GetRowCount(itemCount);
            defineRowDefination();
            defineColumnDefination();
        }

        // add all the view 
        private void loadCells()
        {
            for(position = 0; position <= itemCount; position++)
            {
                View v = adapter.GetView(position, null, mGrid);
                AddViewInGrid(v, position);
            }
        }

        // calculate how many rows required
        // if required then added new row else remove unwanted 
        // rows from the grid
        private void defineRowDefination()
        {
            int definitionsCount = mGrid.RowDefinitions.Count;
            if (definitionsCount != rowCount)
            {
                if(definitionsCount > rowCount)
                {
                    int diff = definitionsCount - rowCount;
                    // here, count take 3 because when you remove row from the grid 
                    // below create some space, otherwise it will be always resting to the bottom
                    if (diff >= 3)
                    {
                        for (int i = (definitionsCount - 1); i > rowCount; i--)
                        {
                             mGrid.RowDefinitions.RemoveAt(i);
                        }
                    }
                }
                else
                {
                    // to remove row definations
                    for (int i = definitionsCount; i < rowCount; i++)
                    {
                        mGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    }
                }  
            }
        }

        // same as like row; add new column or remove unwanted column
        // but most of case not change the count of the column at run time 
        // so, it will be give same answers everytime 
        private void defineColumnDefination()
        {
            int definitionsCount = mGrid.ColumnDefinitions.Count;
            if (definitionsCount != NumColumns)
            {
                if (definitionsCount > NumColumns)
                {
                    for (int i = (definitionsCount - 1); i > NumColumns; i--)
                    {
                        mGrid.ColumnDefinitions.RemoveAt(i);
                    }
                }
                else
                {
                    for (int i = definitionsCount; i < NumColumns; i++)
                    {
                        mGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    }
                }
            }
        }

        // to calculate how many row required
        private int GetRowCount(int itemCount)
        {
            int nRow = 0;
            float value = (float)itemCount / NumColumns;
            nRow = (int)value;
            if (IsFloat(value))
            {
                nRow += 1;
            }
            return nRow;
        }

        // to check no is float or not
        private bool IsFloat(float f)
        {
            return ((f - (int)f) != 0);
        }

        // add view in grid with gesture recognizers
        private async void AddViewInGrid(View view, int position)
        {
            viewDict.Add(position, view);
            await adapter.InAnimation(view);
            mGrid.Children.Add(view, GetColumnIndex(position), GetRowIndex(position));
            await adapter.OutAnimation(view);
            SetGestureRecognizers(view, position);
        }

        // apply gesture to the view
        private void SetGestureRecognizers(View view, int position)
        {
            view.GestureRecognizers.Clear();
            CustomGestureRecognizers cgr = new CustomGestureRecognizers();
            cgr.grid = this;
            cgr.postion = position;
            cgr.OnTapped = ItemTappedEvent;
            view.GestureRecognizers.Add(cgr.getTapGestureRecognizer());
        }

        // to store the details of the view like 
        // position and view holder 
        private class CustomGestureRecognizers
        {
            public GridView grid { get; set; }
            public EventHandler<GridEventArgs> OnTapped { get; set; }
            public int postion { get; set; }
            public TapGestureRecognizer getTapGestureRecognizer()
            {
                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.NumberOfTapsRequired = 1;
                tapGestureRecognizer.Tapped += (s, e) =>
                {
                    GridEventArgs args = new GridEventArgs();
                    args.position = postion;
                    args.view = (View)s;
                    OnTapped?.Invoke(grid, args);
                };

                return tapGestureRecognizer;
            }
        }

        // remove view from the grid
        private void RemoveViewInGrid(View view)
        {
            mGrid.Children.Remove(view);
        }

        //clear whole grid
        private void ClearGrid()
        {
            mGrid.Children.Clear();
        }

        // get child count
        private int GetChildCount()
        {
            return mGrid.Children.Count - 1;
        }

        // to get the row index 
        // mathematically calculate the row index
        // logic : row    = (int)(index / width)
        private int GetRowIndex(int position)
        {
            return position / NumColumns;
        }

        // to get the column index 
        // mathematically calculate the column index
        // logic : column = index % width
        private int GetColumnIndex(int position)
        {
            return position % NumColumns;
        }
    }
}
