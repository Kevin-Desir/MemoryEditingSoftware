using MemoryEditingSoftware.Core.Entities;
using System.Windows.Controls;

namespace MemoryEditingSoftware.Run.Views
{
    /// <summary>
    /// Interaction logic for WriteItem.xaml
    /// </summary>
    public partial class WriteItem : UserControl
    {
        public WriteItem(EditItem editItem)
        {
            InitializeComponent();

            Name.Text = editItem.Name;
            if (editItem.IsLoop)
                ActivateButton.Content = "Enable";
            else
                ActivateButton.Content = "Activate";
            Val.Text = editItem.Value;
        }
    }
}
