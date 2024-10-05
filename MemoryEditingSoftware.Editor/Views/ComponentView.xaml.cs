using MemoryEditingSoftware.Core.Entities;
using Prism.Commands;
using System.Windows.Controls;
using System.Windows.Input;

namespace MemoryEditingSoftware.Editor.Views
{
    /// <summary>
    /// Interaction logic for EditorItemView
    /// </summary>
    public partial class ComponentView : UserControl
    {
        #region Commands 

        public DelegateCommand<GridPosition> UpdateGridPositionCommand { get; set; }

        #endregion

        #region Constructor 

        public ComponentView(EditItem editItem, DelegateCommand<GridPosition> updateGridPositionCommand)
        {
            InitializeComponent();

            ComponentContentControl.Content = new SimpleReadView(editItem);
            UpdateGridPositionCommand = updateGridPositionCommand;
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
                GridPosition gridPosition = new GridPosition();
                switch (button.Name)
                {
                    case "top":
                        gridPosition.Direction = GridPositionDirection.Top;
                        break;
                    case "bottom":
                        gridPosition.Direction = GridPositionDirection.Bottom;
                        break;
                    case "left":
                        gridPosition.Direction = GridPositionDirection.Left;
                        break;
                    case "right":
                        gridPosition.Direction = GridPositionDirection.Right;
                        break;
                }
                gridPosition.ComponentView = this;

                UpdateGridPositionCommand.Execute(gridPosition);
            }
        }

        private void Button_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Button button)
            {
                GridPosition gridPosition = new GridPosition();
                gridPosition.ComponentView = this;
                switch (button.Name)
                {
                    case "top":
                        gridPosition.Direction = GridPositionDirection.ReverseTop;
                        break;
                    case "bottom":
                        gridPosition.Direction = GridPositionDirection.ReverseBottom;
                        break;
                    case "left":
                        gridPosition.Direction = GridPositionDirection.ReverseLeft;
                        break;
                    case "right":
                        gridPosition.Direction = GridPositionDirection.ReverseRight;
                        break;
                }
                UpdateGridPositionCommand.Execute(gridPosition);
            }
        }
    }
}
