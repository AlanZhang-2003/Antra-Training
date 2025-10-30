using System.Runtime.InteropServices;

namespace ConsoleApp1;

public class ConsoleApp2
{

    public static int[] GenerateNumbers()
    {
        Console.Write("Size of array:");
        int size = int.Parse(Console.ReadLine());
        int[] array = new int[size];

        for (int i = 0; i < size; i++)
        {
            array[i] = i + 1;
        }
        
        return array;
    }
    
    public static void Reverse(int[] numbers)
    {
        for (int i = 0; i < numbers.Length / 2; i++)
        {
            int temp =  numbers[i];
            numbers[i] = numbers[numbers.Length - 1 - i];
            numbers[numbers.Length - 1 - i] = temp;
        }  
    }

    public static void PrintNumbers(int[] numbers)
    {
        foreach(int number in numbers)
        {
            Console.Write($"{number} ");
        }
    }

    public static int Fibonacci(int step)
    {
        if (step <= 1)
        {
            return step;
        }
        return Fibonacci(step -1 ) + Fibonacci(step - 2);
    }

    /*
    static void Main(string[] args)
    {
        int[] numbers = GenerateNumbers();
        Reverse(numbers);
        PrintNumbers(numbers);
        
        Console.WriteLine(Fibonacci(4));
    }*/
    
    
    
}

