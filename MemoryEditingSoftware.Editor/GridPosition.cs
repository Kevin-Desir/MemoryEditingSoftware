using MemoryEditingSoftware.Editor.Views;

namespace MemoryEditingSoftware.Editor
{
    public class GridPosition
    {
        public ComponentView ComponentView;
        public GridPositionDirection Direction;
    }

    public enum GridPositionDirection 
    { 
        Left, Right, Top, Bottom, 
        ReverseLeft, ReverseRight, ReverseTop, ReverseBottom
    }
}
