using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Chapter09_Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly Brush _drawBrush = Brushes.AliceBlue;
    private readonly Pen _drawPen = new(Brushes.Black, 4);
    private readonly Size _drawSize = new(30, 30);
    private readonly Brush _selectBrush = Brushes.Red;
    private readonly Brush _selectionSquareBrush = Brushes.Transparent;
    private readonly Pen _selectionSquarePen = new(Brushes.Black, 1);

    private bool _isDragging;

    /// <summary>
    ///  点击和左上角偏移
    /// </summary>
    private Vector _clickOffset;

    /// <summary>
    /// 选中的元素
    /// </summary>
    private DrawingVisual? _selectedVisual;

    /// <summary>
    /// 多选模式
    /// </summary>
    private bool _isMultiSelect;

    /// <summary>
    /// 绘制的多选覆盖长方体
    /// </summary>
    private DrawingVisual? _selectSquare;

    private Point _selectSquareTopLeft;

    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 处理 DrawCanvas 控件的 MouseLeftButtonDown 事件。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void DrawCanvas_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (CmdAdd.IsChecked == true) // 添加方格
        {
            var visual = new DrawingVisual();
            var mousePoint = e.GetPosition(DrawCanvas); // 获取鼠标位置
            DrawSquare(visual, mousePoint, false);
            DrawCanvas.AddVisual(visual);
        }
        else if (CmdDelete.IsChecked == true) // 1. 寻找方格  2. 删除方格
        {
            var mousePoint = e.GetPosition(DrawCanvas);
            var visual = DrawCanvas.GetDrawingVisual(mousePoint);
            if (visual != null)
            {
                DrawCanvas.RemoveVisual(visual);
            }
        }
        else if (CmdSelectOne.IsChecked == true) // 1. 获取方格  2. 标记方格  3. 设置移动  4. 计算偏移  5. 改变样式
        {
            var mousePoint = e.GetPosition(DrawCanvas);
            var visual = DrawCanvas.GetDrawingVisual(mousePoint);
            if (visual != null)
            {
                _isDragging = true;
                var topLeftCorner = new Point(
                    visual.ContentBounds.TopLeft.X + _drawPen.Thickness / 2,
                    visual.ContentBounds.TopLeft.Y + _drawPen.Thickness / 2
                ); // 边界 + 笔触
                _clickOffset = topLeftCorner - mousePoint;
                if (_selectedVisual != null && _selectedVisual != visual)
                {
                    ClearSelectSquare();
                }
                _selectedVisual = visual;
            }
        }
        else if (CmdSelectMore.IsChecked == true) // 1. 定义矩形  2. 寻找方格  3. 绘制方格  4. 改变样式
        {
            _isMultiSelect = true;
            _selectSquare = new DrawingVisual();
            DrawCanvas.AddVisual(_selectSquare);
            _selectSquareTopLeft = e.GetPosition(DrawCanvas);
            Mouse.Capture(DrawCanvas); // 捕获鼠标
        }
    }

    /// <summary>
    /// 处理 DrawCanvas 控件的 MouseMove 事件。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void DrawCanvas_OnMouseMove(object sender, MouseEventArgs e)
    {
        if (_isDragging && _selectedVisual != null)
        {
            var topLeftPoint = e.GetPosition(DrawCanvas) + _clickOffset;
            DrawSquare(_selectedVisual, topLeftPoint, true);
        }
        else if (_isMultiSelect && _selectSquare != null)
        {
            var pointDragging = e.GetPosition(DrawCanvas);
            DrawSelectionSquare(_selectSquareTopLeft, pointDragging);
        }
    }

    /// <summary>
    /// 处理 DrawCanvas 控件的 MouseLeftButtonUp 事件。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void DrawCanvas_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (_isDragging)
        {
            _isDragging = false;
            ClearSelectSquare();
        }
        if (_isMultiSelect)
        {
            _isMultiSelect = false;
            var rectGeometry = new RectangleGeometry(
                new Rect(_selectSquareTopLeft, e.GetPosition(DrawCanvas))
            );
            var visuals = DrawCanvas.GetVisualsByHit(rectGeometry);

            MessageBox.Show($"选择了{visuals.Length} 个元素");

            if (_selectSquare != null)
            {
                DrawCanvas.RemoveVisual(_selectSquare);
            }

            Mouse.Capture(null);
        }
    }

    /// <summary>
    /// 绘制方格
    /// </summary>
    /// <param name="visual">源元素</param>
    /// <param name="point">起始点</param>
    /// <param name="isSelected">是否标记</param>
    private void DrawSquare(DrawingVisual visual, Point point, bool isSelected)
    {
        using var drawContext = visual.RenderOpen();

        if (point.X < 0)
            point.X = 0;
        else if (point.X + _drawSize.Width > DrawCanvas.ActualWidth)
            point.X = DrawCanvas.ActualWidth - _drawSize.Width;

        if (point.Y < 0)
            point.Y = 0;
        else if (point.Y + _drawSize.Height > DrawCanvas.ActualHeight)
            point.Y = DrawCanvas.ActualHeight - _drawSize.Height;

        TestText.Text = point.ToString();

        drawContext.DrawRectangle(
            isSelected ? _selectBrush : _drawBrush,
            _drawPen,
            new Rect(point, _drawSize)
        );
    }

    /// <summary>
    /// 绘制多选方格
    /// </summary>
    /// <param name="point1">左上起始点</param>
    /// <param name="point2">目标点</param>
    private void DrawSelectionSquare(Point point1, Point point2)
    {
        if (_selectSquare == null)
            return;

        _selectionSquarePen.DashStyle = DashStyles.Dash;
        using var drawContext = _selectSquare.RenderOpen();
        drawContext.DrawRectangle(
            _selectionSquareBrush,
            _selectionSquarePen,
            new Rect(point1, point2)
        );
    }

    /// <summary>
    /// 清空选中的方格
    /// </summary>
    private void ClearSelectSquare()
    {
        if (_selectedVisual == null)
            return;

        var topLeftCorner = new Point(
            _selectedVisual.ContentBounds.TopLeft.X + _drawPen.Thickness / 2,
            _selectedVisual.ContentBounds.TopLeft.Y + _drawPen.Thickness / 2
        ); // 边界 + 笔触}

        DrawSquare(_selectedVisual, topLeftCorner, false);

        _selectedVisual = null;
    }
}
