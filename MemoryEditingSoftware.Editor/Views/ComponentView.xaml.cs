using MemoryEditingSoftware.Core.Entities;
using MemoryEditingSoftware.Run.Views;
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

        private UserControl contentView;

        public UserControl ContentView
        {
            get { return contentView; }
            set { contentView = value; }
        }

        #endregion

        #region Commands 

        #endregion

        #region Constructor 

        public ComponentView(EditItem editItem)
        {
            InitializeComponent();

            ContentView = new WriteItemControl(new SimpleWriter(true, true, editItem));

            ComponentContentControl.Content = ContentView;
        }

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        #endregion

        private void top_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.ScrollNS;
        }

        private void left_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.ScrollWE;
        }

        private void right_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.ScrollWE;
        }

        private void bottom_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.ScrollNS;
        }

        private void cursor_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = null;
        }

        // Handle the start of the drag operation
        private void Canvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
    }
}
