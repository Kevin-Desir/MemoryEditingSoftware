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

        public DelegateCommand<GridPosition> UpdateGridPositionCommand { get; set; }

        #endregion

        #region Commands

        #endregion

        #region Constructor

        public EditorViewModel()
        {
            UpdateGridPositionCommand = new DelegateCommand<GridPosition>(UpdateGridPosition);

            Grid grid = new Grid();
            grid.Background = Brushes.Magenta;
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            ComponentView componentView = new ComponentView(new EditItem("Address", "name", "value", true), UpdateGridPositionCommand);
            Grid.SetRow(componentView, 1);
            Grid.SetRowSpan(componentView, 1);

            Grid.SetColumn(componentView, 1);
            Grid.SetColumnSpan(componentView, 1);

            grid.Children.Add(componentView);

            EditorGridContentControl = new ContentControl();
            EditorGridContentControl.Content = grid;
        }

        private void UpdateGridPosition(GridPosition position)
        {
            Grid grid = (EditorGridContentControl.Content as Grid);
            int index = grid.Children.IndexOf(position.ComponentView);
            int currentColumnSpan = Grid.GetColumnSpan(grid.Children[index]);

            switch (position.Direction)
            {
                case GridPositionDirection.Right:
                    Grid.SetColumnSpan(grid.Children[index], currentColumnSpan + 1);
                    break;
                case GridPositionDirection.ReverseRight:
                    Grid.SetColumnSpan(grid.Children[index], currentColumnSpan - 1);
                    break;
            }
        }

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        #endregion

    }
}
