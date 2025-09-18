using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Chapter09_Wpf.CustCanvas;

/// <summary>
/// 自定义控件
/// </summary>
public class DrawCanvas : Canvas
{
    /// <summary>
    /// 子元素
    /// </summary>
    private readonly List<Visual> _children = [];

    private List<DrawingVisual> _hitChildren = [];

    /// <summary>
    /// 子元素个数
    /// </summary>
    protected override int VisualChildrenCount => _children.Count;

    /// <summary>
    /// 获取子元素索引器
    /// </summary>
    protected override Visual GetVisualChild(int index)
    {
        return _children[index];
    }

    /// <summary>
    /// 添加子元素
    /// </summary>
    /// <param name="visual">可视化元素</param>
    /// <returns></returns>
    public void AddVisual(Visual visual)
    {
        _children.Add(visual);
        AddVisualChild(visual); // 添加到视觉树
        AddLogicalChild(visual); // 添加到逻辑树
    }

    /// <summary>
    /// 移除子元素
    /// </summary>
    /// <param name="visual">移除的可视化元素</param>
    public void RemoveVisual(Visual visual)
    {
        _children.Remove(visual);
        RemoveVisualChild(visual);
        RemoveLogicalChild(visual);
    }

    /// <summary>
    /// 获取子元素
    /// </summary>
    /// <param name="point">子元素覆盖的点</param>
    /// <returns>寻找的子元素, 没有返回Null</returns>
    public DrawingVisual? GetDrawingVisual(Point point)
    {
        var visual = VisualTreeHelper.HitTest(this, point);
        return visual?.VisualHit as DrawingVisual;
    }

    /// <summary>
    /// 获取子元素
    /// </summary>
    /// <param name="region">框选区域</param>
    /// <returns>框选命中的子元素</returns>
    public DrawingVisual[] GetVisualsByHit(Geometry region)
    {
        _hitChildren.Clear();
        var callBack = new HitTestResultCallback(HitTestCallBack);
        var parameters = new GeometryHitTestParameters(region);
        VisualTreeHelper.HitTest(this, null, callBack, parameters);
        return _hitChildren.ToArray();
    }

    /// <summary>
    /// 命中结果处理
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    private HitTestResultBehavior HitTestCallBack(HitTestResult result)
    {
        if (
            result is not GeometryHitTestResult geometryHitTestResult
            || result.VisualHit is not DrawingVisual visual
        )
            return HitTestResultBehavior.Continue;

        if (geometryHitTestResult.IntersectionDetail == IntersectionDetail.FullyInside)
        {
            _hitChildren.Add(visual);
        }
        return HitTestResultBehavior.Continue;
    }
}
