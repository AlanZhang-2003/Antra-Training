// See https://aka.ms/new-console-template for more information

public class MyStack<T>
{
    private List<T> stack = new List<T>();
    public int Count()
    {
        return stack.Count();
    }
    
    public T Pop()
    {
        
        T temp = stack[stack.Count - 1];
        stack.RemoveAt(stack.Count -1);
        return temp;
    }

    public void Push(T item)
    {
        stack.Add(item);
    }
}

public class MyList<T>
{
    private List<T> myList = new List<T>();

    public void Add(T element)
    {
        myList.Add(element);
    }
    
    public void Remove(T element)
    {
        myList.Remove(element);
    }

    public bool Contains(T element)
    {
        foreach (T value in myList)
        {
            if (value.Equals(element))
                return true;
        }

        return false;
    }

    public void Clear()
    {
        myList.Clear();
    }

    public void InsertAt(T element, int index)
    {
        myList.Insert(index, element);
    }

    public void DeleteAt(int index)
    {
        myList.RemoveAt(index);
    }

    public T Find(int index)
    {
        return myList[index];
    }
}

public class Entity
{
    public int Id { get; set; }
}
public interface IRepository<T> where T : Entity
{
    void Add(T item);
    void Remove(T item);
    void Save();
    IEnumerable<T> GetAll();
    T GetById(int id);
}

public class GenericRepository<T>: IRepository<T> where T : Entity
{
    private List<T> _items = new List<T>();
    public void Add(T item)
    {
        _items.Add(item);
    }

    public void Remove(T item)
    {
        _items.Remove(item);
    }

    public void Save()
    {
        Console.WriteLine("Saved");
    }

    public IEnumerable<T> GetAll()
    {
        return _items;
    }

    public T GetById(int id)
    {
        foreach (T item in _items)
        {
            if (item.Id == id)
                return item;
        }

        return null;
    }
}