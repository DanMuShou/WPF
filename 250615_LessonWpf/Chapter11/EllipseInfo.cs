using System.Windows.Shapes;

namespace Chapter11;

public class EllipseInfo(Ellipse ellipse, double velocityY)
{
    public Ellipse Ellipse { get; set; } = ellipse;
    public double VelocityY { get; set; } = velocityY;
}
