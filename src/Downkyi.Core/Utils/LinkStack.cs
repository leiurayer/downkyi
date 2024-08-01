namespace Downkyi.Core.Utils;

// 链表实现栈
public class LinkStack<T>
{
    //栈顶指示器
    public Node<T>? Top { get; set; }

    //栈中结点的个数
    public int NCount { get; set; }

    //初始化
    public LinkStack()
    {
        Top = null;
        NCount = 0;
    }

    //获取栈的长度
    public int GetLength()
    {
        return NCount;
    }

    //判断栈是否为空
    public bool IsEmpty()
    {
        if ((Top == null) && (0 == NCount))
        {
            return true;
        }
        return false;
    }

    //入栈
    public void Push(T item)
    {
        Node<T> p = new(item);
        if (Top == null)
        {
            Top = p;
        }
        else
        {
            p.Next = Top;
            Top = p;
        }
        NCount++;
    }

    //出栈
    public T? Pop()
    {
        if (IsEmpty())
        {
            return default;
        }
        Node<T>? p = Top;
        Top = Top!.Next;
        --NCount;
        return p!.Data;
    }

    //
    public T? Peek()
    {
        if (IsEmpty())
        {
            return default;
        }

        return Top!.Data;
    }
}

//结点定义
public class Node<T>
{
    public T? Data;

    public Node<T>? Next;

    public Node(T item)
    {
        Data = item;
    }
}