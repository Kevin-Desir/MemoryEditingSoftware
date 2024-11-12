using MemoryEditingSoftware.Core;
using MemoryEditingSoftware.Core.Entities;
using MemoryEditingSoftware.Editor.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
            get {  return editorGridContentControl; } 
            set { SetProperty(ref editorGridContentControl, value); } 
        }

        #endregion

        #region Commands

        #endregion

        #region Constructor

        public EditorViewModel()
        {
            Grid grid = new Grid();
            grid.Background = Brushes.Magenta;
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
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
        }

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        #endregion

    }
}
