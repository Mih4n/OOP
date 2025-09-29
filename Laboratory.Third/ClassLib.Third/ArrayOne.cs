using System.Collections;
using System.Text;

namespace ClassLib.Third;

public class ArrayOne : IEnumerable<int>
{
    /// <summary>
    /// inner array
    /// </summary>
    private int[] array;

    /// <summary>
    /// array length
    /// </summary>
    public int Length => array.Length;

    /// <summary>
    /// array indexer
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int this[int index] 
    {
        get => array[index];
        set => array[index] = value;
    }

    /// <summary>
    /// initialize by size without any elements
    /// </summary>
    /// <param name="size"></param>
    public ArrayOne(int size) => array = new int[size];

    /// <summary>
    /// initialize from standard array
    /// </summary>
    /// <param name="array"></param>
    public ArrayOne(int[] array) => this.array = (int[])array.Clone();

    /// <summary>
    /// multiplication
    /// </summary>
    /// <param name="number"></param>
    /// <param name="array"></param>
    /// <returns></returns>
    public static ArrayOne operator *(int number, ArrayOne array)
    {
        var result = new int[array.Length];
        for (var i = 0; i < array.Length; i++)
            result[i] = array[i] * number;

        return new ArrayOne(result);
    }

    /// <summary>
    /// multiplication
    /// </summary>
    /// <param name="array"></param>
    /// <param name="number"></param>
    /// <returns></returns>
    public static ArrayOne operator *(ArrayOne array, int number)
        => number * array;

    /// <summary>
    /// operator
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    public static ArrayOne operator -(ArrayOne array) 
    {
        var result = new int[array.Length];
        for (var i = 0; i < array.Length; i++)
            result[i] = -array[i];
        return new ArrayOne(result);
    }

    /// <summary>
    /// input array
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static ArrayOne Input(string name)
    {
        Console.WriteLine($"write down an array: {name}");
        var input = Console.ReadLine();
        var arr = input?.Split(" ").Select(int.Parse).ToArray() ?? [];
        return new ArrayOne(arr);
    }

    /// <summary>
    /// print array
    /// </summary>
    /// <param name="name"></param>
    public void Print(string name)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append("[ ");
        foreach (var element in array)
            stringBuilder.AppendFormat("{0}, ", element);
        stringBuilder.Append("]");
        Console.WriteLine($"{name} = {stringBuilder}");
    }

    /// <summary>
    /// static method: sum of negative elements in multiple arrays
    /// </summary>
    /// <param name="arrays"></param>
    /// <returns></returns>
    public static int SumOnlyNegative(params ArrayOne[] arrays)
    {
        return arrays
            .Sum(a => a.Where(e => e < 0).Sum());
    }

    /// <summary>
    /// sum of negative elements in one array
    /// </summary>
    /// <returns></returns>
    public int SumNegatives() => array.Where(e => e < 0).Sum();

    /// <summary>
    /// replace repeated negative elements with given sum
    /// </summary>
    /// <param name="sum"></param>
    public void ReplaceNegativesWithSum(int sum)
    {
        var seen = new HashSet<int>();
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] < 0)
            {
                if (seen.Contains(array[i]))
                    array[i] = sum;
                else
                    seen.Add(array[i]);
            }
        }
    }

    /// <inheritdoc/>
    public IEnumerator<int> GetEnumerator()
    {
        foreach (var element in array)
            yield return element;
    } 

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
