using MemoryEditingSoftware.Editor.Views;

namespace MemoryEditingSoftware.Editor
{
    /// <summary>
    /// Position of the graphic component on the editor grid.
    /// </summary>
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
