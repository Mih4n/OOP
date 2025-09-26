using System.Collections;

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
    public ArrayOne(int[] array) => this.array = array;

    /// <summary>
    /// multiplication
    /// </summary>
    /// <param name="number"></param>
    /// <param name="array"></param>
    /// <returns></returns>
    public static ArrayOne operator *(int number, ArrayOne array)
    {
        for (var i = 0; i < array.Length; i++)
            array[i] = array[i] * number;

        return array;
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
        for (var i = 0; i < array.Length; i++)
            array[i] = -array[i];

        return array;
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
