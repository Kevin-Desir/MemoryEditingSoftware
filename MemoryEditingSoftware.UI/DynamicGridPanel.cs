//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;

//namespace MemoryEditingSoftware.UI
//{
//    public class DynamicGridPanel : Panel
//    {
//        private Dictionary<(int row, int column), UIElement> _occupiedCells = new Dictionary<(int row, int column), UIElement>();

//        Override MeasureOverride to measure children size
//        protected override Size MeasureOverride(Size availableSize)
//        {
//            foreach (UIElement child in Children)
//            {
//                child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
//            }
//            return availableSize; // Return size required by the panel
//        }

//        Override ArrangeOverride to arrange children in the grid
//        protected override Size ArrangeOverride(Size finalSize)
//        {
//            foreach (UIElement child in Children)
//            {
//                var row = GetRow(child);
//                var column = GetColumn(child);
//                var rowSpan = GetRowSpan(child);
//                var columnSpan = GetColumnSpan(child);

//                Rect rect = CalculateCellBounds(row, column, rowSpan, columnSpan, finalSize);
//                child.Arrange(rect);
//            }
//            return finalSize;
//        }

//        Method to calculate the actual cell position and size based on grid layout logic
//        private Rect CalculateCellBounds(int row, int column, int rowSpan, int columnSpan, Size finalSize)
//        {
//            Implement your custom logic for calculating each cell's size and position
//            return new Rect();
//        }

//        Check if the cell is occupied
//        public bool IsCellOccupied(int row, int column)
//        {
//            return _occupiedCells.ContainsKey((row, column));
//        }

//        Add elements to the grid
//        public void AddElement(UIElement element, int row, int column, int rowSpan = 1, int columnSpan = 1)
//        {
//            if (!IsCellWithinBounds(row, column) || IsCellOccupied(row, column))
//                throw new InvalidOperationException("Cannot place element at the specified position.");

//            _occupiedCells[(row, column)] = element;
//            Children.Add(element);
//            SetRow(element, row);
//            SetColumn(element, column);
//            SetRowSpan(element, rowSpan);
//            SetColumnSpan(element, columnSpan);
//        }

//        Remove elements from the grid
//        public void RemoveElement(UIElement element)
//        {
//            var row = GetRow(element);
//            var column = GetColumn(element);

//            _occupiedCells.Remove((row, column));
//            Children.Remove(element);
//        }

//        private bool IsCellWithinBounds(int row, int column)
//        {
//            Implement boundary check logic based on grid size
//            return true;
//        }

//        // Custom attached properties for Row/Column/Span (for binding purposes if needed)
//        public static int GetRow(UIElement element) => (int)element.GetValue(RowProperty);
//        public static void SetRow(UIElement element, int value) => element.SetValue(RowProperty, value);

//        public static int GetColumn(UIElement element) => (int)element.GetValue(ColumnProperty);
//        public static void SetColumn(UIElement element, int value) => element.SetValue(ColumnProperty, value);

//        public static int GetRowSpan(UIElement element) => (int)element.GetValue(RowSpanProperty);
//        public static void SetRowSpan(UIElement element, int value) => element.SetValue(RowSpanProperty, value);

//        public static int GetColumnSpan(UIElement element) => (int)element.GetValue(ColumnSpanProperty);
//        public static void SetColumnSpan(UIElement element, int value) => element.SetValue(ColumnSpanProperty, value);
//    }

//}
