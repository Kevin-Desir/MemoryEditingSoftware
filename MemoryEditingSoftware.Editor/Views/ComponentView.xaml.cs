﻿using MemoryEditingSoftware.Core.Entities;
using Prism.Commands;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MemoryEditingSoftware.Editor.Views
{
    /// <summary>
    /// Interaction logic for EditorItemView
    /// </summary>
    public partial class ComponentView : UserControl
    {
        #region Properties

        private UIElement _draggedElement; // Store the element being dragged

        #endregion

        #region Commands 

        #endregion

        #region Constructor 

        public ComponentView(EditItem editItem)
        {
            InitializeComponent();

            ComponentContentControl.Content = new SimpleReadView(editItem);
        }

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        #endregion

        //private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    if (sender is Button button)
        //    {
        //        switch (button.Name)
        //        {
        //            case "top":
        //                this.SetValue(Grid.RowProperty, (int) this.GetValue(Grid.RowProperty) - 1);
        //                this.SetValue(Grid.RowSpanProperty, (int)this.GetValue(Grid.RowSpanProperty) + 1);
        //                break;
        //            case "bottom":
        //                this.SetValue(Grid.RowSpanProperty, (int) this.GetValue(Grid.RowSpanProperty) + 1);
        //                break;
        //            case "left":
        //                this.SetValue(Grid.ColumnProperty, (int) this.GetValue(Grid.ColumnProperty) - 1);
        //                this.SetValue(Grid.ColumnSpanProperty, (int) this.GetValue(Grid.ColumnSpanProperty) + 1);
        //                break;
        //            case "right":
        //                this.SetValue(Grid.ColumnSpanProperty, (int) this.GetValue(Grid.ColumnSpanProperty) + 1);
        //                break;
        //        }
        //    }
        //}

        //private void Button_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (sender is Button button)
        //    {
        //        switch (button.Name)
        //        {
        //            case "top":
        //                this.SetValue(Grid.RowProperty, (int)this.GetValue(Grid.RowProperty) + 1);
        //                this.SetValue(Grid.RowSpanProperty, (int)this.GetValue(Grid.RowSpanProperty) - 1);
        //                break;
        //            case "bottom":
        //                this.SetValue(Grid.RowSpanProperty, (int)this.GetValue(Grid.RowSpanProperty) - 1);
        //                break;
        //            case "left":
        //                this.SetValue(Grid.ColumnProperty, (int)this.GetValue(Grid.ColumnProperty) + 1);
        //                this.SetValue(Grid.ColumnSpanProperty, (int)this.GetValue(Grid.ColumnSpanProperty) - 1);
        //                break;
        //            case "right":
        //                this.SetValue(Grid.ColumnSpanProperty, (int)this.GetValue(Grid.ColumnSpanProperty) - 1);
        //                break;
        //        }
        //    }
        //}

        //private void top_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    this.Cursor = Cursors.ScrollN;
        //}

        //private void top_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    this.Cursor = null;
        //}

        // Handle the start of the drag operation
        private void Canvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Canvas element)
            {
                _draggedElement = element; // Keep a reference to the dragged element
                DragDrop.DoDragDrop(element, element, DragDropEffects.Move);
            }
        }
    }
}
