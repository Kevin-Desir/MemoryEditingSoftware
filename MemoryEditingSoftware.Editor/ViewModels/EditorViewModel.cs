using MemoryEditingSoftware.Core;
using MemoryEditingSoftware.Core.Attributes;
using MemoryEditingSoftware.Core.Dialogs;
using MemoryEditingSoftware.Core.Entities;
using MemoryEditingSoftware.Editor.Views;
using MemoryEditingSoftware.Run.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MemoryEditingSoftware.Editor.ViewModels
{
    public class EditorViewModel : BindableBase
    {
        #region Properties

        /// <summary>
        /// Content control of the main grid (dynamically generated) in the editor where all the component items are showed and that the user interacts with.
        /// Where all the action takes place !
        /// </summary>
        private ContentControl editorGridContentControl;
        public ContentControl EditorGridContentControl
        {
            get { return editorGridContentControl; }
            set { SetProperty(ref editorGridContentControl, value); }
        }

        /// <summary>
        /// The content control that display the properties of item clicked by the user on the editor grid.
        /// </summary>
        private ContentControl propertiesContentControl;
        public ContentControl PropertiesContentControl
        {
            get { return propertiesContentControl; }
            set { SetProperty(ref propertiesContentControl, value); }
        }

        /// <summary>
        /// How many rows there are in the editor grid.
        /// </summary>
        private ushort rowCount;

        public ushort RowCount
        {
            get { return rowCount; }
            set
            {
                SetProperty(ref rowCount, value);
                UpdateRowCount(rowCount);
            }
        }

        /// <summary>
        /// How many columns there are in the editor grid.
        /// </summary>
        private ushort columnCount;

        public ushort ColumnCount
        {
            get { return columnCount; }
            set
            {
                SetProperty(ref columnCount, value);
                UpdateColumnCount(columnCount);
            }
        }

        /// <summary>
        /// Grid that display the properties of item clicked by the user on the editor grid.
        /// </summary>
        private Grid propertyGrid;
        private Grid grid;
        private ushort _oldRowCount;
        private ushort _oldColumnCount;

        #endregion

        #region Commands

        #endregion

        #region Constructor

        public EditorViewModel()
        {
            grid = new Grid();
            grid.ShowGridLines = true;

            RowCount = 5;
            _oldRowCount = RowCount;
            ColumnCount = 5;
            _oldColumnCount = ColumnCount;

            grid.Background = Brushes.Magenta;
            //for (int i = 0; i < RowCount; i++)
            //{
            //    grid.RowDefinitions.Add(new RowDefinition());
            //}
            //for (int i = 0; i < ColumnCount; i++)
            //{
            //    grid.ColumnDefinitions.Add(new ColumnDefinition());
            //}

            ComponentView componentView = new ComponentView(new EditItem("Address", "name", "value", true));
            Grid.SetRow(componentView, 1);
            Grid.SetRowSpan(componentView, 1);

            Grid.SetColumn(componentView, 1);
            Grid.SetColumnSpan(componentView, 1);

            grid.Children.Add(componentView);

            ComponentView componentView2 = new ComponentView(new EditItem("Address 5", "name 5", "value 5", true));
            Grid.SetRow(componentView2, 2);
            Grid.SetRowSpan(componentView2, 1);

            Grid.SetColumn(componentView2, 2);
            Grid.SetColumnSpan(componentView2, 1);

            grid.Children.Add(componentView2);

            EditorGridContentControl = new ContentControl();
            EditorGridContentControl.Content = grid;

            grid.AllowDrop = true;
            grid.Drop += mainGrid_Drop;
            grid.DragOver += mainGrid_DragOver;
            grid.MouseLeftButtonUp += mainGrid_MouseLeftButtonUp;

            PropertiesContentControl = new ContentControl();
            propertyGrid = new Grid();
            propertyGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            propertyGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            PropertiesContentControl.Content = propertyGrid;
        }

        #endregion

        #region Private Methods

        // Handle the DragOver event to allow dropping
        private void mainGrid_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
            e.Handled = true;
        }

        private void mainGrid_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.Source as ComponentView == null)
            {
                return; // clicked on an unavailable case
            }
            if ((e.Source as ComponentView).ContentView as WriteItemControl == null)
            {
                return; // clicked on an unavailable case
            }

            SimpleWriter ed = ((e.Source as ComponentView).ContentView as WriteItemControl).SimpleWriter;

            var flaggedProperties = typeof(SimpleWriter)
                .GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(EditableProperty)));

            propertyGrid.Children.Clear();
            propertyGrid.RowDefinitions.Clear();
            int i = 0;
            
            foreach (var property in flaggedProperties)
            {
                propertyGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                var attribute = (EditableProperty)Attribute.GetCustomAttribute(property, typeof(EditableProperty));
                string displayName = attribute?.Name ?? property.Name;

                Label label = new Label()
                {
                    Content = displayName,
                };
                Grid.SetColumn(label, 0);
                Grid.SetRow(label, i);
                propertyGrid.Children.Add(label);

                if (property.PropertyType == typeof(string))
                {
                    TextBox textBox = new TextBox()
                    {
                        Text = property.GetValue(ed).ToString(),
                        VerticalAlignment = VerticalAlignment.Center,
                    };
                    Grid.SetColumn(textBox, 1);
                    Grid.SetRow(textBox, i);
                    propertyGrid.Children.Add(textBox);
                }
                else if (property.PropertyType == typeof(bool))
                {
                    ComboBox comboBox = new ComboBox()
                    {
                        ItemsSource = new List<string>() { "No", "Yes" },
                        SelectedIndex = (bool)property.GetValue(ed) == true ? 1 : 0,
                    };
                    Grid.SetColumn(comboBox, 1);
                    Grid.SetRow(comboBox, i);
                    propertyGrid.Children.Add(comboBox);
                }
                else if (property.PropertyType == typeof(VariableTypes))
                {
                    ComboBox comboBox = new ComboBox()
                    {
                        ItemsSource = Enum.GetValues(typeof(VariableTypes))
                                      .Cast<VariableTypes>()
                                      .ToList(),
                        SelectedItem = (VariableTypes)property.GetValue(ed)
                    };
                    Grid.SetColumn(comboBox, 1);
                    Grid.SetRow(comboBox, i);
                    propertyGrid.Children.Add(comboBox);
                }
                i++;
            }
        }

        // Handle the drop event
        private void mainGrid_Drop(object sender, DragEventArgs e)
        {
            Canvas draggedElement = e.Data.GetData(typeof(Canvas)) as Canvas;

            if (draggedElement == null)
                return;

            ComponentView componentView = ((draggedElement.Parent as Grid).Parent as Border).Parent as ComponentView;


            // Get the mouse position and calculate the target grid cell
            var position = e.GetPosition(grid);

            int targetRow = GetRowFromPosition(position.Y);
            int targetColumn = GetColumnFromPosition(position.X);
            int currentRow = (int)componentView.GetValue(Grid.RowProperty);
            int currentRowSpan = (int)componentView.GetValue(Grid.RowSpanProperty);
            int currentColumn = (int)componentView.GetValue(Grid.ColumnProperty);
            int currentColumnSpan = (int)componentView.GetValue(Grid.ColumnSpanProperty);

            if (targetRow >= 0 && targetColumn >= 0)
            {
                switch (draggedElement.Name)
                {
                    case "top":
                        if (targetRow <= currentRowSpan)
                        {
                            componentView.SetValue(Grid.RowSpanProperty, currentRow + currentRowSpan - targetRow);
                            componentView.SetValue(Grid.RowProperty, targetRow);
                        }
                        break;
                    case "bottom":
                        if (targetRow >= currentRow)
                        {
                            componentView.SetValue(Grid.RowSpanProperty, targetRow - currentRow + 1);
                        }
                        break;
                    case "left":
                        if (targetColumn <= currentColumnSpan)
                        {
                            componentView.SetValue(Grid.ColumnSpanProperty, currentColumn + currentColumnSpan - targetColumn);
                            componentView.SetValue(Grid.ColumnProperty, targetColumn);
                        }
                        break;
                    case "right":
                        if (targetColumn >= currentColumn)
                        {
                            componentView.SetValue(Grid.ColumnSpanProperty, targetColumn - currentColumn + 1);
                        }
                        break;
                }
            }

            Console.WriteLine($"Row = {componentView.GetValue(Grid.RowProperty)}\tRowSpan = {componentView.GetValue(Grid.RowSpanProperty)}\n" +
                $"Column = {componentView.GetValue(Grid.ColumnProperty)} + ColumnSpan = {componentView.GetValue(Grid.ColumnSpanProperty)}");

            draggedElement = null; // Clear the reference
        }

        // Get the row index based on the Y-coordinate
        private int GetRowFromPosition(double y)
        {
            double cumulativeHeight = 0;
            for (int i = 0; i < grid.RowDefinitions.Count; i++)
            {
                cumulativeHeight += grid.RowDefinitions[i].ActualHeight;
                if (y < cumulativeHeight)
                    return i;
            }
            return -1; // Not found
        }

        // Get the column index based on the X-coordinate
        private int GetColumnFromPosition(double x)
        {
            double cumulativeWidth = 0;
            for (int i = 0; i < grid.ColumnDefinitions.Count; i++)
            {
                cumulativeWidth += grid.ColumnDefinitions[i].ActualWidth;
                if (x < cumulativeWidth)
                    return i;
            }
            return -1; // Not found
        }

        private void UpdateRowCount(ushort newRowCount)
        {
            if (newRowCount > _oldRowCount)
            {
                for (int i = _oldRowCount; i < newRowCount; i++)
                {
                    grid.RowDefinitions.Add(new RowDefinition());
                }
            }
            else if (newRowCount < _oldRowCount)
            {
                for (int i = _oldRowCount; i > newRowCount; i--)
                {
                    grid.RowDefinitions.RemoveAt(grid.RowDefinitions.Count - 1);
                }
            }
            _oldRowCount = newRowCount;
        }

        private void UpdateColumnCount(ushort newColumnCount)
        {
            if (newColumnCount > _oldColumnCount)
            {
                for (int i = _oldColumnCount; i < newColumnCount; i++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                }
            }
            else if (newColumnCount < _oldColumnCount)
            {
                for (int i = _oldColumnCount; i > newColumnCount; i--)
                {
                    grid.ColumnDefinitions.RemoveAt(grid.ColumnDefinitions.Count - 1);
                }
            }
            _oldColumnCount = newColumnCount;
        }

        #endregion

        #region Public Methods

        #endregion

    }
}

