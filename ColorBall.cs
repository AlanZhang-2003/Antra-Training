namespace ConsoleApp1;

public class ColorBall
{
    public class Color
    {
        private int _red;
        private int _green;
        private int _blue;
        private int _alpha;
        
        public Color(int red, int green, int blue)
        {
            _red = red;
            _green = green;
            _blue = blue;
        }

        public Color(int red, int green, int blue, int alpha = 255)
        {
            _red = red;
            _green = green;
            _blue = blue;
            _alpha = alpha;
        }

        public int GetSetRed
        {
            get => _red;
            set => _red = value;
        }

        public int GetSetGreen
        {
            get => _green;
            set => _green = value;
        }

        public int GetSetBlue
        {
            get => _blue;
            set => _blue = value;
        }

        public int GetSetAlpha
        {
            get => _alpha;
            set => _alpha = value;
        }

        public int GrayScale()
        {
            return ((_red + _green + _blue) / 3);
        }
    }

    public class Ball
    {
        private Color _color;
        private int _size;
        public int ThrowCount;

        public Ball(int size, Color color, int throwCount = 0)
        {
            _size = size;
            _color = color;
            ThrowCount = throwCount;
        }

        public void Pop()
        {
            _size = 0;
        }
        
        public void ThrowBall()
        {
            if(_size != 0)
                ThrowCount++;
        }

        public int GetThrown()
        {
            return ThrowCount;
        }
    }
    
    static void Main(string[] args)
    {
        Color color1 = new Color(1, 1, 1, 1);
        Color color2 = new Color(50, 12, 5);
        
        Console.WriteLine($"Color1 Blue: {color1.GetSetBlue}");
        color1.GetSetBlue = 100;
        Console.WriteLine($"Color1 Blue: {color1.GetSetBlue}");
        
        Console.WriteLine($"Color2 Alpha: {color2.GetSetAlpha}");

        Ball ball1 = new Ball(100, color1, 50);
        Console.WriteLine($"Ball1 Before ThrowCount: {ball1.ThrowCount}");
        ball1.ThrowBall();
        Console.WriteLine($"Ball1 After ThrowCount: {ball1.ThrowCount}");
        ball1.Pop();
        ball1.ThrowBall();
        Console.WriteLine($"Ball1 ThrowCount After Pop: {ball1.ThrowCount}");
        
        Ball ball2 = new Ball(50, color2);
        Console.WriteLine($"Ball2 Default ThrowCount: {ball2.ThrowCount}");
        
    }
}