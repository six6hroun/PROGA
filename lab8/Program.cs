using MyVector;
public class MyStack<T> : MyVector<T>
{
    public MyStack() : base()
    {
    }
    public void Push(T item) //№1 метод для помещения элменета на вершину стека
    {
        Add(item);
    }
    public T Pop() //№2 метод для извлечения верхнего элемента из стека
    {
        if (elementCount == 0) throw new ArgumentOutOfRangeException("Stack empty");
        return Remove(elementCount - 1);
    }
    public T Peek() //№3 метод для возвращения верхнего элменета стека без его извлечения
    {
        if (elementCount == 0) throw new ArgumentOutOfRangeException("Stack empty");
        return LastElement();
    }
    public bool Empty() //№4 метод для проверки, является ли стек пустым
    {
        return IsEmpty();
    }
    public int Search(T item) //№5 метод для поиска глубины объекта в стеке
    {
        if (IndexOF(item) == -1) return -1;
        return elementCount - IndexOF(item);
    }
}