// See https://aka.ms/new-console-template for more information

using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Schema;
//Alan Zhang
//Testing:

//CopyArray();
//GroceryList();
//int[] primes = FindPrimesInRange(2, 30); for (int i = 0; i < primes.Length; i++) Console.WriteLine(primes[i]);
//int[] input = new int[4] { 3, 2, 4, -1 }; int[] sum = RotateSum(input, 2); for(int i = 0; i < sum.Length; i++) Console.WriteLine(sum[i]);
//int[] input = new int[9] { 0, 1, 1, 5, 2, 2, 6, 3, 3};  int[] ans = longest(input); for (int i = 0; i < ans.Length; i++) Console.WriteLine(ans[i]);
//mostFrequent([7,7,7,0,2,2,2,0,10,10,10]);
//reverse();
//ReverseSentence("C# is not C++, and PHP is not Delphi!");
//palindrome("Hi,exe? ABBA! Hog fully a string: ExE. Bob");
//extract("ftp://www.example.com/employee");

void CopyArray()
{
    int[] array = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    int[] array2 = new int[10];

    for (int i = 0; i < array.Length; i++)
    {
        array2[i] = array[i];
    }

    for (int i = 0; i < array.Length; i++)
    {
        Console.WriteLine($"array 1: {array[i]}");
        Console.WriteLine($"array 2: {array2[i]}");
    }
}

void GroceryList()
{
    string[] items = new string[0];
    string input;
    string start;
    while(true)
    {
        for (int i = 0; i < items.Length; i++)
        {
            Console.WriteLine(items[i]);
        }
        
        Console.WriteLine("Enter command (+ item, - item, or -- to clear)):");
        Console.Write("> ");
        input = Console.ReadLine();
        
        if (input == "_")
            break;

        if (string.IsNullOrEmpty(input))
            continue;
        
        start = input.Substring(0,2);
        string name = input.Length > 2 ? input.Substring(2) : "";

        if (start == "+ " && !string.IsNullOrEmpty(name))
        {
            Array.Resize(ref items, items.Length + 1);
            items[items.Length - 1] = name;
        }
        else if (start == "- " && !string.IsNullOrEmpty(name))
        {
            int reSizeLoc = 0;
            bool found = false;
            string[] copy = new string[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == name && !found)
                {
                    found = true;
                    continue;
                }
                copy[i] = items[i];
                reSizeLoc++;
            }

            if (found)
            {
                Array.Resize(ref copy, reSizeLoc);
                items = copy;
            }
        }
        else if (input == "--")
            items = new string[0];
        

    }
}

static int[] FindPrimesInRange(int startNum, int endNum)
{
    int[] primes = new int[(endNum - startNum + 1)];
    int reSizeLoc = 0;
    for(int i = startNum; i <= endNum; i++)
    {
        bool isPrime = true;
        for (int j = 2; j*j <= i; j++)
        {
            if (i % j == 0)
            {
                isPrime = false;
                break;
            }
        }

        if (isPrime)
        {
            primes[reSizeLoc] = i;
            reSizeLoc++;
        }
    }
    Array.Resize(ref primes, reSizeLoc);
    return primes;
}

static int[] RotateSum(int[] nums, int rotate)
{
    int[] sum = new int[nums.Length];

    for (int n = 1; n <= rotate; n++)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            int rotatedIndex = (i + n) % nums.Length;
            sum[rotatedIndex] += nums[i];
        }
    }
    return sum;
}

static int[] longest(int[] nums)
{
    int j = 0;
    int longestLength = 1;
    int longestNum = nums[0];
    for (int i = 1; i < nums.Length; i++)
    {
        if (nums[i] != nums[i - 1])
        {
            if (i - j > longestLength)
            {
                longestLength = i - j;
                longestNum = nums[j];
            }
            j = i;
        }
    }
    

    int[] ans = new int[longestLength];
    for (int i = 0; i < longestLength; i++)
    {
        ans[i] = longestNum;
    }

    return ans;
}

static void mostFrequent(int[] nums)
{
    int maxCount = 0;
    int leftMost = nums[0];
    for (int i = 0; i < nums.Length; i++)
    {
        int count = 0;
        for (int j = 0; j < nums.Length; j++)
        {
            if (nums[j] == nums[i])
                count++;

            if (count > maxCount)
            {
                maxCount = count;
                leftMost = nums[i];
            }
        }
    }
    
    Console.WriteLine($"The number {leftMost} is the most frequent (occurs {maxCount} times)");
}

static void reverse()
{
    string word = "sample";
    char[] wordChar =  word.ToCharArray();
    Array.Reverse(wordChar);
    Console.WriteLine(wordChar);

    string word2 = "24tvcoi92";
    for (int i = word2.Length-1; i >= 0; i--)
    {
        Console.Write(word2[i]);
    }
}

static void ReverseSentence(string sentence)
{
    char[] seperators = { '.', ',', ':', ';', '=', '(', ')', '&', '[', ']', '"', '\'', '\\', '/', '!', '?', ' ' };
    int wordCount = 0;
    int i = 0;
    while (i < sentence.Length)
    {
        if (Array.Exists(seperators, s => s == sentence[i]))
        {
            while (i < sentence.Length && Array.Exists(seperators, s => s == sentence[i]))
                i++;
            wordCount++;

        }
        else
        {
            while (i < sentence.Length && !Array.Exists(seperators, s => s == sentence[i]))
                i++;
            wordCount++;

        }

    }

    i = 0;
    int index = 0;
    string[] reverse = new string[wordCount];
    while (i < sentence.Length)
    {
        if (Array.Exists(seperators, s => s == sentence[i]))
        {
            int start = i;
            while (i < sentence.Length && Array.Exists(seperators, s => s == sentence[i]))
                i++;
            reverse[index] = sentence.Substring(start, i - start);
            index++;
            
        }
        else
        {
            int start = i;
            while (i < sentence.Length && !Array.Exists(seperators, s => s == sentence[i]))
                i++;
            reverse[index] = sentence.Substring(start, i - start);
            index++;

        }

    }

    int l = 0;
    int r = wordCount - 1;
    while (l < r)
    {
        while (l < r && Array.Exists(seperators, s => s == reverse[l][0]))
            l++;
        while (l < r && Array.Exists(seperators, s => s == reverse[r][0]))
            r--;
        if (l < r)
        {
            string temp = reverse[l];
            reverse[l] = reverse[r];
            reverse[r] = temp;
            l++;
            r--;
        }
    }

    string ans = string.Join("", reverse);
    Console.WriteLine(ans);
}

static void palindrome(string sentence)
{
    int j = 0;
    int length = 0;
    char[] seperators = { '.', ',', ':', ';', '=', '(', ')', '&', '[', ']', '"', '\'', '\\', '/', '!', '?', ' ' };

    string[] result = new string[sentence.Length];
    int index = 0;
    
    for (int i = 0; i < sentence.Length; i++)
    {
        length++;
        if (Array.Exists(seperators, s => s == sentence[i]))
        {
            string word = sentence.Substring(j, length - 1);
            j = i + 1;
            length = 0;

            bool isPalidrome = true;
            for (int x = 0; x < word.Length / 2; x++)
            {
                if (word[x] != word[word.Length - x - 1])
                {
                    isPalidrome = false;
                    break;
                }
            }

            if (isPalidrome)
                result[index++] = word;
        }
    }
    
    Array.Sort(result, 0, index, StringComparer.OrdinalIgnoreCase);

    for (int i = 0; i < index ; i++)
    {
        if(result[i] == "")
            continue;
        
        Console.Write(result[i]);
        
        if(i < index - 1)
            Console.Write(", ");
    }

}

static void extract(string url)
{
    string protocol = "";
    string server = "";
    string resource = "";

    int protocolEnd = url.IndexOf("://");

    if (protocolEnd != 1)
    {
        protocol = url.Substring(0, protocolEnd);
        int serverStart = protocolEnd + 3;
        int resourceStart = url.IndexOf('/', serverStart);

        if (resourceStart != -1)
        {
            server = url.Substring(serverStart, resourceStart - serverStart);
            resource = url.Substring(resourceStart + 1);
        }
        else
        {
            server = url;
        }
    }
    else
    {
        int resourceStart = url.IndexOf("/");
        if (resourceStart != -1)
        {
            server = url.Substring(0, resourceStart);
            resource = url.Substring(resourceStart + 1);
        }
        else
        {
            server = url;
        }
    }
    
    Console.WriteLine($"Protocol: {protocol}");
    Console.WriteLine($"Server: {server}");
    Console.WriteLine($"Resource: {resource}");
}

