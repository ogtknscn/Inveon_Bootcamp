namespace Week_1_Homework
{
    public class AreaCalculator
    {
        public static double CalculateArea(object shape)
        {
            if (shape is Circle circle)
            {
                return Math.PI * circle.Radius * circle.Radius;
            }
            else if (shape is Rectangle rectangle)
            {
                return rectangle.Width * rectangle.Height;
            }
            else
            {
                throw new ArgumentException("Unsupported shape type");
            }
        }
    }

    // Refactored code adhering to OCP
    // Abstract base class for shapes
    public abstract class Shape
    {
        public abstract double CalculateArea();
    }

    // Circle implementation
    public class Circle : Shape
    {
        public double Radius { get; set; }

        public override double CalculateArea()
        {
            return Math.PI * Radius * Radius;
        }
    }

    // Rectangle implementation
    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public override double CalculateArea()
        {
            return Width * Height;
        }
    }

    // Area calculator using OCP
    public class AreaCalculatorRefactored
    {
        public static double CalculateArea(Shape shape)
        {
            return shape.CalculateArea();
        }
    }
}