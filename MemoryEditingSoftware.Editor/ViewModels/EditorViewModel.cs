using MemoryEditingSoftware.Core;
using MemoryEditingSoftware.Core.Attributes;
using MemoryEditingSoftware.Core.Dialogs;
using MemoryEditingSoftware.Core.Entities;
using MemoryEditingSoftware.Editor.Views;
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

        private ContentControl editorGridContentControl;
        public ContentControl EditorGridContentControl
        {
            get { return editorGridContentControl; }
            set { SetProperty(ref editorGridContentControl, value); }
        }

        private ContentControl propertiesContentControl;
        public ContentControl PropertiesContentControl
        {
            get { return propertiesContentControl; }
            set { SetProperty(ref propertiesContentControl, value); }
        }

        StackPanel propertiesStackPanel;

        private Grid grid;

        #endregion

        #region Commands

        #endregion

        #region Constructor

        public EditorViewModel()
        {
            grid = new Grid();
            grid.ShowGridLines = true;

            grid.Background = Brushes.Magenta;
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

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

            //Grid propGrid1 = new Grid();
            //propGrid1.ColumnDefinitions.Add(new ColumnDefinition());
            //propGrid1.ColumnDefinitions.Add(new ColumnDefinition());
            //Label nameLabel = new Label();
            //nameLabel.Content = "Name";
            //TextBox nameTextBlock = new TextBox();
            //propGrid1.Children.Add(nameLabel);
            //propGrid1.Children.Add(nameTextBlock);
            //Grid.SetColumn(nameTextBlock,1);
            propertiesStackPanel = new StackPanel();
            //propertiesStackPanel.Children.Add(propGrid1);
            PropertiesContentControl = new ContentControl();
            PropertiesContentControl.Content = propertiesStackPanel;           
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
            SimpleReader ed = ((e.Source as ComponentView).ContentView as SimpleReadView).SimpleReader;

            var flaggedProperties = typeof(SimpleReader)
                .GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(EditableProperty)));

            propertiesStackPanel.Children.Clear();

            foreach (var property in flaggedProperties)
            {
                Console.WriteLine($"Property: {property.Name}, Value: {property.GetValue(ed)}");
                Label label = new Label()
                {
                    Content = $"{property.Name} -> {property.GetValue(ed)}"
                };
                propertiesStackPanel.Children.Add(label);
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

            if (targetRow >= 0 && targetColumn >= 0)
            {
                switch (draggedElement.Name)
                {
                    case "top":
                        componentView.SetValue(Grid.RowSpanProperty, (int)componentView.GetValue(Grid.RowProperty) + (int)componentView.GetValue(Grid.RowSpanProperty) - targetRow);
                        componentView.SetValue(Grid.RowProperty, targetRow);
                        break;
                    case "bottom":
                        componentView.SetValue(Grid.RowSpanProperty, targetRow - (int)componentView.GetValue(Grid.RowProperty) + 1);
                        break;
                    case "left":
                        componentView.SetValue(Grid.ColumnSpanProperty, (int)componentView.GetValue(Grid.ColumnProperty) + (int)componentView.GetValue(Grid.ColumnSpanProperty) - targetColumn);
                        componentView.SetValue(Grid.ColumnProperty, targetColumn);
                        break;
                    case "right":
                        componentView.SetValue(Grid.ColumnSpanProperty, targetColumn - (int)componentView.GetValue(Grid.ColumnProperty) + 1);
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
    }

    #endregion

    #region Public Methods

    #endregion

}

