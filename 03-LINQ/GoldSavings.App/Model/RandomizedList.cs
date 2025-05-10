namespace GoldSavings.App.Model;

public class RandomizedList<T>
{
    private List<T> _list = new List<T>();
    private Random _random = new Random();
    
    public void Add(T element)
    {
        if (_random.Next(0, 2) == 0)
        {
            _list.Insert(0, element);
        }
        else
        {
            _list.Add(element);
        }
    }
    
    public T Get(int index)
    {
        if (_list.Count == 0)
        {
            throw new Exception("List is empty.");
        }
        
        return _list[_random.Next(0, index)];
    }
    
    public bool IsEmpty()
    {
        return _list.Count == 0;
    }
}