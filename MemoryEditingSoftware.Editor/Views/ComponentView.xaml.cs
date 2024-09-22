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
            GridPosition gridPosition = new GridPosition();
            gridPosition.Direction = GridPositionDirection.Right;
            gridPosition.ComponentView = this;

            UpdateGridPositionCommand.Execute(gridPosition);
        }

        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GridPosition gridPosition = new GridPosition();
            gridPosition.Direction = GridPositionDirection.ReverseRight;
            gridPosition.ComponentView = this;

            UpdateGridPositionCommand.Execute(gridPosition);
        }
    }
}
