using System; 
using static System.Console; 


internal class Program
{
    private static void Main(string[] args)
    {
        WriteBinary(234); 
    }

    static int[] ConvertToBinary(int number)
    {
        if (number > 255 || number < 0)
        {
            return []; 
        }

        int[] bits = new int[8]; 

        for (int i=bits.Length - 1; i > -1; i--)
        {
            if (number % 2 == 0)
            {
                bits[i] = 0; 
                number /= 2; 
            }

            else 
            {
                bits[i] = 1;
                number = (number - 1) / 2;  
            }                                        
        }
        return bits; 
    }

    static void WriteBinary(int number)
    {
        int[] binary = ConvertToBinary(number); 
        Write($"{number} = "); 
        foreach (int bit in binary)
        {
            Write(bit); 
        }
        WriteLine(); 
    }
}
