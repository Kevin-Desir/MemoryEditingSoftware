using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MemoryEditingSoftware.Editor.Controls
{
    public class DroppableButton : Button
    {
        public Type DroppableViewType { get; }
        public Type ObjectType { get; }

        public DroppableButton(string displayName, Type droppableViewType, Type objectType)
        {
            Content = displayName;
            DroppableViewType = droppableViewType;
            ObjectType = objectType;

            Margin = new System.Windows.Thickness(5);
            Padding = new System.Windows.Thickness(5);
        }
    }
}
