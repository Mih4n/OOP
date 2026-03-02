using System.Collections;

namespace Second.ClassLib.First;

public class MyLinkedList<T> : ICollection<T>, IEnumerable<T>
{
    private int count;
    private Node<T>? head;
    private Node<T>? tail;

    public int Count => count;

    public bool IsReadOnly => false;

    public void Clear()
    {
        head = tail = null;
        count = 0;
    }

    public bool Contains(T item)
    {
        foreach (var x in this)
            if (EqualityComparer<T>.Default.Equals(x, item))
                return true;

        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        foreach (var item in this)
            array[arrayIndex++] = item;
    }

    public void Add(T item)
    {
        var node = new Node<T>(item);

        if (head == null)
            head = tail = node;
        else
        {
            tail?.Next = node;
            tail = node;
        }

        count++;
    }

    public bool Remove(T item)
    {
        Node<T>? current = head;
        Node<T>? previous = null;

        var comparer = EqualityComparer<T>.Default;

        while (current != null)
        {
            if (comparer.Equals(current.Value, item))
            {
                if (previous == null)
                    head = current.Next;
                else
                    previous.Next = current.Next;

                if (current == tail)
                    tail = previous;

                count--;
                return true;
            }

            previous = current;
            current = current.Next;
        }

        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node<T>? current = head;

        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

internal class Node<T>
{
    public T Value;
    public Node<T>? Next;

    public Node(T value)
    {
        Value = value;
    }
}
