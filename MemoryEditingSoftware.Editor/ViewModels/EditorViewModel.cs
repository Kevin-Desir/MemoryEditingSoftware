using MemoryEditingSoftware.Core;
using MemoryEditingSoftware.Core.Attributes;
using MemoryEditingSoftware.Core.Dialogs;
using MemoryEditingSoftware.Core.Entities;
using MemoryEditingSoftware.Editor.Controls;
using MemoryEditingSoftware.Editor.Views;
using MemoryEditingSoftware.Run.Controls;
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
using System.Windows.Input;
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
        /// Content control that displays the list of components available that the user can drop on the grid to add a new component.
        /// </summary>
        private ContentControl componentsAvailableContentControl;
        public ContentControl ComponentsAvailableContentControl
        {
            get { return componentsAvailableContentControl; }
            set { SetProperty(ref componentsAvailableContentControl, value); }
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

            grid.Background = Brushes.DarkSlateGray;

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

            ComponentsAvailableContentControl = new ContentControl();
            StackPanel availableComponentsStackPannel = new StackPanel();
            ComponentsAvailableContentControl.Content = availableComponentsStackPannel;
            IEnumerable<Type> droppableViews = Assembly.GetAssembly(typeof(WriteItemControl))
            .GetTypes()
            .Where(type => type.GetCustomAttributes(typeof(DroppableView), false).Any());

            foreach (var type in droppableViews)
            {
                var attribute = (DroppableView)type.GetCustomAttribute(typeof(DroppableView));
                //object subObject = Activator.CreateInstance(attribute.ObjectType);
                //object instance = Activator.CreateInstance(type, subObject);
                Console.WriteLine($"Class: {type.Name}, ObjectType: {attribute.ObjectType}");
                string displayName = attribute?.Name ?? type.Name;
                DroppableButton button = new DroppableButton(displayName, type, attribute.ObjectType);
                button.PreviewMouseLeftButtonDown += Button_PreviewMouseLeftButtonDown;

                availableComponentsStackPannel.Children.Add(button);
            }
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
                propertyGrid.Children.Clear();
                propertyGrid.RowDefinitions.Clear();
                return; // clicked on an unavailable case
            }
            if ((e.Source as ComponentView).ContentView as WriteItemControl == null)
            {
                propertyGrid.Children.Clear();
                propertyGrid.RowDefinitions.Clear();
                return; // clicked on an unavailable case
            }
        }

        // Handle the drop event
        private void mainGrid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(Canvas)) != null)
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
                            if (targetRow <= currentRow + currentRowSpan - 1)
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
                            if (targetColumn <= currentColumn + currentColumnSpan - 1)
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
            else if (e.Data.GetData(typeof(ContentControl)) != null)
            {
                ContentControl draggedElement = e.Data.GetData(typeof(ContentControl)) as ContentControl;

                if (draggedElement == null)
                    return;

                ComponentView componentView = ((draggedElement.Parent as Grid).Parent as Border).Parent as ComponentView;

                if (componentView == e.Source as ComponentView)
                {
                    // show the properties (when the user clicks)
                    if (e.Source as ComponentView == null)
                    {
                        propertyGrid.Children.Clear();
                        propertyGrid.RowDefinitions.Clear();
                        return; // clicked on an unavailable case
                    }
                    if ((e.Source as ComponentView).ContentView as IComponentControl == null)
                    {
                        propertyGrid.Children.Clear();
                        propertyGrid.RowDefinitions.Clear();
                        return; // clicked on an unavailable case
                    }

                    object ed = ((e.Source as ComponentView).ContentView as IComponentControl).MainObject;
                    Type mainObjectType = ed.GetType();

                    var flaggedProperties = mainObjectType
                        .GetProperties()
                        .Where(prop => Attribute.IsDefined(prop, typeof(EditableProperty)));

                    propertyGrid.Children.Clear();
                    propertyGrid.RowDefinitions.Clear();

                    propertyGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    Label propertyLabel = new Label() { Content = "Properties" };
                    Grid.SetColumn(propertyLabel, 0);
                    Grid.SetRow(propertyLabel, 0);
                    propertyGrid.Children.Add(propertyLabel);

                    int i = 1;
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

                    propertyGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    Button validatePropertiesButton = new Button()
                    {
                        Content = "Validate",
                        Margin = new Thickness(5),
                        Padding = new Thickness(5),
                        Background = new SolidColorBrush(Colors.Green),
                        Foreground = new SolidColorBrush(Colors.White),
                        IsEnabled = false
                    };
                    Grid.SetColumn(validatePropertiesButton, 1);
                    Grid.SetRow(validatePropertiesButton, i);
                    propertyGrid.Children.Add(validatePropertiesButton);
                }
                else if (e.Source as ComponentView == null)
                {
                    // drop the element (when the user do a drag and drop)
                    // Get the mouse position and calculate the target grid cell
                    var position = e.GetPosition(grid);

                    int targetRow = GetRowFromPosition(position.Y);
                    int targetColumn = GetColumnFromPosition(position.X);

                    componentView.SetValue(Grid.ColumnProperty, targetColumn);
                    componentView.SetValue(Grid.RowProperty, targetRow);
                    componentView.SetValue(Grid.ColumnSpanProperty, 1);
                    componentView.SetValue(Grid.RowSpanProperty, 1);

                    draggedElement = null; // Clear the reference
                }
                // otherwise, we are trying to drop on another cell that is already occupied
            }
            else if (e.Data.GetData(typeof(DroppableButton)) != null)
            {
                DroppableButton draggedElement = e.Data.GetData(typeof(DroppableButton)) as DroppableButton;
                object subObject = Activator.CreateInstance(draggedElement.ObjectType);
                object instance = Activator.CreateInstance(draggedElement.DroppableViewType, subObject);

                // Get the mouse position and calculate the target grid cell
                var position = e.GetPosition(grid);

                int targetRow = GetRowFromPosition(position.Y);
                int targetColumn = GetColumnFromPosition(position.X);

                ComponentView componentView = new ComponentView(instance as UserControl);
                Grid.SetRow(componentView, targetRow);
                Grid.SetRowSpan(componentView, 1);

                Grid.SetColumn(componentView, targetColumn);
                Grid.SetColumnSpan(componentView, 1);

                grid.Children.Add(componentView);
            }
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

        // Handle the start of the drag operation
        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Canvas element)
            {
                DragDrop.DoDragDrop(element, element, DragDropEffects.Move);
            }
            else if (sender is ContentControl contentControl)
            {
                DragDrop.DoDragDrop(contentControl, contentControl, DragDropEffects.Move);
            }
        }

        #endregion

        #region Public Methods

        #endregion

    }
}

