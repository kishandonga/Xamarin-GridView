using System;
using System.Collections.Generic;
using Xamarin.Forms;
using DLab.Widgets;
using DLab.Forms.Events;

/*
 * Developed By : Kishan Donga, (Software Engineer - Mobility)
 * Email Id : kishandonga.92@gmail.com
 * Contact : (+91)9712598499
 * Country : India
 * */

namespace DLab.Views
{
    public class GridView : ScrollView
    {
        private static Grid mGrid = null;
        private GridAdapter adapter = null;
        private int rowCount = 0;
        private int itemCount = 0;
        private int position = 0;
        private Dictionary<int, View> viewDict = new Dictionary<int, View>();
        public EventHandler<GridEventArgs> ItemTappedEvent = null; 

        public GridView()
        {
            mGrid = new Grid();
            Content = mGrid;
            mGrid.SizeChanged += Handle_SizeChanged;
        }

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

        public int NumRow
        {
            get { return rowCount; }
        }

        public int ItemCount
        {
            get { return (itemCount + 1); }
        }

        public GridAdapter ItemSource
        {
            set { setAdapter(value); }
            get { return adapter; }
        }

        public EventHandler<GridEventArgs> ItemTapped
        {
            set { ItemTappedEvent = value; }
            get { return ItemTappedEvent; }
        }

        private void setAdapter(GridAdapter adapter)
        {
            if (adapter == null)
            {
                throw new ArgumentNullException("Adapter can not be null!");
            }

            if(NumColumns <= 0)
            {
                throw new ArgumentException("NumColumns must be geter then zero!");
            }

            this.adapter = adapter;
            adapter.AttachToEvent(Handle_notifyDataSetChange);
            adapter.AttachToEvent(Handle_notifyDataSetChangeByIndex);
            setDefinations();
            loadCells();
        }

        private void Handle_notifyDataSetChange(object o, EventArgs arg)
        {
            if(mGrid != null)
            {
                ClearGrid();
                setDefinations();
                for (position = 0; position <= itemCount; position++)
                {
                    View oldView = viewDict[position];
                    View v = adapter.GetView(position, oldView, mGrid);
                    viewDict.Remove(position);
                    AddViewInGrid(v, position);
                }
            }
        }

        private void Handle_notifyDataSetChangeByIndex(object o, int index)
        {
            if (mGrid != null)
            {
                setDefinations();
                View v;

                if (viewDict.ContainsKey(index))
                {
                    for(position = GetChildCount(); position >= index; position--)
                    {
                        RemoveViewInGrid(viewDict[position]);
                    }

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
                    position = index;
                    v = adapter.GetView(position, null, mGrid);
                    AddViewInGrid(v, position);
                } 
            }
        }

        private async void Handle_SizeChanged(object o, EventArgs arg)
        {
            if (ScrollToEnd)
            {
                double width = mGrid.Width;
                double height = mGrid.Height;
                await ScrollToAsync(width, height, false);
            }
        }

        private void setDefinations()
        {
            itemCount = adapter.GetCount() - 1;
            rowCount = GetRowCount(itemCount);
            defineRowDefination();
            defineColumnDefination();
        }

        private void loadCells()
        {
            for(position = 0; position <= itemCount; position++)
            {
                View v = adapter.GetView(position, null, mGrid);
                AddViewInGrid(v, position);
            }
        }

        private void defineRowDefination()
        {
            int definitionsCount = mGrid.RowDefinitions.Count;
            if (definitionsCount != rowCount)
            {
                if(definitionsCount > rowCount)
                {
                    int diff = definitionsCount - rowCount;
                    if(diff >= 3)
                    {
                        for (int i = (definitionsCount - 1); i > rowCount; i--)
                        {
                             mGrid.RowDefinitions.RemoveAt(i);
                        }
                    }
                }
                else
                {
                    for (int i = definitionsCount; i < rowCount; i++)
                    {
                        mGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    }
                }  
            }
        }

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

        private bool IsFloat(float f)
        {
            return ((f - (int)f) != 0);
        }

        private void AddViewInGrid(View view, int position)
        {
            viewDict.Add(position, view);
            mGrid.Children.Add(view, GetColumnIndex(position), GetRowIndex(position));
            SetGestureRecognizers(view,position);
        }

        private void SetGestureRecognizers(View view, int position)
        {
            view.GestureRecognizers.Clear();
            CustomGestureRecognizers cgr = new CustomGestureRecognizers();
            cgr.grid = this;
            cgr.postion = position;
            cgr.OnTapped = ItemTappedEvent;
            view.GestureRecognizers.Add(cgr.getTapGestureRecognizer());
        }

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

        private void RemoveViewInGrid(View view)
        {
            mGrid.Children.Remove(view);
        }

        private void ClearGrid()
        {
            mGrid.Children.Clear();
        }

        private int GetChildCount()
        {
            return mGrid.Children.Count - 1;
        }

        private int GetRowIndex(int position)
        {
            return position / NumColumns;
        }

        private int GetColumnIndex(int position)
        {
            return position % NumColumns;
        }
    }
}
