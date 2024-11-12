using MemoryEditingSoftware.Core.Entities;
using Prism.Commands;
using System.Collections.Generic;
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

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                switch (button.Name)
                {
                    case "top":
                        this.SetValue(Grid.RowProperty, (int) this.GetValue(Grid.RowProperty) - 1);
                        this.SetValue(Grid.RowSpanProperty, (int)this.GetValue(Grid.RowSpanProperty) + 1);
                        break;
                    case "bottom":
                        this.SetValue(Grid.RowSpanProperty, (int) this.GetValue(Grid.RowSpanProperty) + 1);
                        break;
                    case "left":
                        this.SetValue(Grid.ColumnProperty, (int) this.GetValue(Grid.ColumnProperty) - 1);
                        this.SetValue(Grid.ColumnSpanProperty, (int) this.GetValue(Grid.ColumnSpanProperty) + 1);
                        break;
                    case "right":
                        this.SetValue(Grid.ColumnSpanProperty, (int) this.GetValue(Grid.ColumnSpanProperty) + 1);
                        break;
                }
            }
        }

        private void Button_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Button button)
            {
                switch (button.Name)
                {
                    case "top":
                        this.SetValue(Grid.RowProperty, (int)this.GetValue(Grid.RowProperty) + 1);
                        this.SetValue(Grid.RowSpanProperty, (int)this.GetValue(Grid.RowSpanProperty) - 1);
                        break;
                    case "bottom":
                        this.SetValue(Grid.RowSpanProperty, (int)this.GetValue(Grid.RowSpanProperty) - 1);
                        break;
                    case "left":
                        this.SetValue(Grid.ColumnProperty, (int)this.GetValue(Grid.ColumnProperty) + 1);
                        this.SetValue(Grid.ColumnSpanProperty, (int)this.GetValue(Grid.ColumnSpanProperty) - 1);
                        break;
                    case "right":
                        this.SetValue(Grid.ColumnSpanProperty, (int)this.GetValue(Grid.ColumnSpanProperty) - 1);
                        break;
                }
            }
        }
    }
}
