using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Stopify.Presentation.Utilities.Controls;

public class StretchyWrapPanel : Panel
{
    public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register(nameof(ItemWidth),
            typeof(double), typeof(StretchyWrapPanel), new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsMeasure));

    [TypeConverter(typeof(LengthConverter))]
    public double ItemWidth
    {
        get { return (double)GetValue(ItemWidthProperty); }
        set { SetValue(ItemWidthProperty, value); }
    }

    public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register(nameof(ItemHeight),
            typeof(double), typeof(StretchyWrapPanel), new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsMeasure));

    [TypeConverter(typeof(LengthConverter))]
    public double ItemHeight
    {
        get { return (double)GetValue(ItemHeightProperty); }
        set { SetValue(ItemHeightProperty, value); }
    }

    public static readonly DependencyProperty OrientationProperty = StackPanel.OrientationProperty.AddOwner(typeof(StretchyWrapPanel),
            new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsMeasure, OnOrientationChanged));

    public Orientation Orientation
    {
        get { return _orientation; }
        set { SetValue(OrientationProperty, value); }
    }

    private static void OnOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((StretchyWrapPanel)d)._orientation = (Orientation)e.NewValue;
    }

    private Orientation _orientation = Orientation.Horizontal;

    public static readonly DependencyProperty StretchProportionallyProperty = DependencyProperty.Register(nameof(StretchProportionally), typeof(bool),
            typeof(StretchyWrapPanel), new PropertyMetadata(true, OnStretchProportionallyChanged));

    public bool StretchProportionally
    {
        get { return _stretchProportionally; }
        set { SetValue(StretchProportionallyProperty, value); }
    }

    private static void OnStretchProportionallyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
        ((StretchyWrapPanel)o)._stretchProportionally = (bool)e.NewValue;
    }

    private bool _stretchProportionally = true;

    private struct UVSize
    {
        internal UVSize(Orientation orientation, double width, double height)
        {
            U = V = 0d;
            _orientation = orientation;
            Width = width;
            Height = height;
        }

        internal UVSize(Orientation orientation)
        {
            U = V = 0d;
            _orientation = orientation;
        }

        internal double U;
        internal double V;
        private readonly Orientation _orientation;

        internal double Width
        {
            get { return _orientation == Orientation.Horizontal ? U : V; }
            set
            {
                if (_orientation == Orientation.Horizontal)
                {
                    U = value;
                }
                else
                {
                    V = value;
                }
            }
        }

        internal double Height
        {
            get { return _orientation == Orientation.Horizontal ? V : U; }
            set
            {
                if (_orientation == Orientation.Horizontal)
                {
                    V = value;
                }
                else
                {
                    U = value;
                }
            }
        }
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        var curLineSize = new UVSize(Orientation);
        var panelSize = new UVSize(Orientation);
        var itemWidth = ItemWidth;
        var itemHeight = ItemHeight;
        var itemWidthSet = !double.IsNaN(itemWidth);
        var itemHeightSet = !double.IsNaN(itemHeight);
        var uvAvailableSize = new UVSize(Orientation, availableSize.Width, availableSize.Height);
        var useItemU = Orientation == Orientation.Horizontal ? itemWidthSet : itemHeightSet;

        var children = InternalChildren;
        int itemsInFirstRow = 0;

        // Measure how many items fit into the first row
        for (int i = 0, count = children.Count; i < count; i++)
        {
            var child = children[i];
            if (child == null) continue;

            child.Measure(new Size(itemWidthSet ? itemWidth : availableSize.Width, itemHeightSet ? itemHeight : availableSize.Height));

            var sz = new UVSize(Orientation, itemWidthSet ? itemWidth : child.DesiredSize.Width, itemHeightSet ? itemHeight : child.DesiredSize.Height);

            if (curLineSize.U + sz.U > uvAvailableSize.U)
            {
                break; // First line is full
            }
            else
            {
                curLineSize.U += sz.U;
                curLineSize.V = Math.Max(sz.V, curLineSize.V);
                itemsInFirstRow++;
            }
        }

        // Set the panel size to the size of the first row only
        if (itemsInFirstRow > 0)
        {
            panelSize.U = curLineSize.U;
            panelSize.V = curLineSize.V;
        }

        return new Size(panelSize.Width, panelSize.Height);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        var firstInLine = 0;
        var itemWidth = ItemWidth;
        var itemHeight = ItemHeight;
        double accumulatedV = 0;
        var itemU = Orientation == Orientation.Horizontal ? itemWidth : itemHeight;
        var curLineSize = new UVSize(Orientation);
        var uvFinalSize = new UVSize(Orientation, finalSize.Width, finalSize.Height);
        var itemWidthSet = !double.IsNaN(itemWidth);
        var itemHeightSet = !double.IsNaN(itemHeight);
        var useItemU = Orientation == Orientation.Horizontal ? itemWidthSet : itemHeightSet;

        var children = InternalChildren;
        int itemsInFirstRow = 0;

        // Measure the first row and ensure we don't overflow the available size
        for (int i = 0, count = children.Count; i < count; i++)
        {
            var child = children[i];
            if (child == null) continue;

            var sz = new UVSize(Orientation, itemWidthSet ? itemWidth : child.DesiredSize.Width, itemHeightSet ? itemHeight : child.DesiredSize.Height);

            if (curLineSize.U + sz.U > uvFinalSize.U)
            {
                break; // Stop after the first row
            }
            else
            {
                curLineSize.U += sz.U;
                curLineSize.V = Math.Max(sz.V, curLineSize.V);
                itemsInFirstRow++;
            }
        }

        // If there are items in the first row, calculate the space for each
        if (itemsInFirstRow > 0)
        {
            // Calculate how much space each item should take
            double spacePerItem = uvFinalSize.U / itemsInFirstRow;

            // Arrange the first row normally, adjusting the size to fit within available space
            for (int i = 0; i < itemsInFirstRow; i++)
            {
                var child = children[i];
                if (child != null)
                {
                    // Stretch each item to fill the available width in the first row
                    child.Arrange(new Rect(i * spacePerItem, accumulatedV, spacePerItem, curLineSize.V));
                }
            }
        }

        // Set width and height to 0 for all items not in the first row (hidden)
        for (int i = itemsInFirstRow, count = children.Count; i < count; i++)
        {
            var child = children[i];
            if (child != null)
            {
                // Collapse them completely
                child.Arrange(new Rect(0, 0, 0, 0));
            }
        }

        return new Size(uvFinalSize.U, curLineSize.V); // Return the final size based on the first row
    }
}
