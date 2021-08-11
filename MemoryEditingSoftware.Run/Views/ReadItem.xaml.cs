using MemoryEditingSoftware.Core.Entities;
using System.Runtime.InteropServices;
using System.Windows.Controls;

namespace MemoryEditingSoftware.Run.Views
{
    /// <summary>
    /// Interaction logic for ReadItem.xaml
    /// </summary>
    public partial class ReadItem : UserControl
    {
        public ReadItem(EditItem editItem)
        {
            InitializeComponent();

            name.Text = editItem.Name;
            val.Text = editItem.Value;
        }
    }
}
