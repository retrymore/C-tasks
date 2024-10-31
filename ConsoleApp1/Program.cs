/*//Создайте класс, который будет
//работать с коллекциями объектов.
//Используйте Generic для определения типа данных,
//которые будут храниться в коллекции.
//Реализуйте основные операции над коллекцией:
//добавление, удаление, поиск и т. д.
//TODO - delete commented code
public class CustomCollection<T>
{
    private List<T> _items;
    public CustomCollection()
    {
        _items = new List<T>();
    }
    public void Add(T item)
    {
        _items.Add(item);
    }
    public bool Remove(T item)
    {
        return _items.Remove(item);
    }
    public T Find(Predicate<T> match)
    {
        return _items.Find(match);
    }
    public List<T> GetAll()
    {
        return new List<T>(_items);
    }
    public int Count
    {
        get { return _items.Count; }
    }

}
public class Program
{
    static void Main()
    {
        CustomCollection<string> stringCollection = new CustomCollection<string>();
        stringCollection.Add("Hello");
        stringCollection.Add("World");
        Console.WriteLine($"Elements: {+ stringCollection.Count}");
        string foundItem = stringCollection.Find(item => item.StartsWith("H"));
        stringCollection.Remove(foundItem);
        Console.WriteLine($"Elements after delete: {+ stringCollection.Count}");
        List<string> allItem = stringCollection.GetAll();
    }
}*/

using System.Diagnostics;

//TODO add xml comments - description about the class goals
public class ProductPurchase //TODO according to task is it Abstract class? isn't it?
{
    // do you need the possibility to set thise propeties in external code base?
    public string ProductName { get; set; } 
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal priceWSale { set; get; }
    public bool isSale { get; set; } = false; //TODO it is unneccessary because the default value for boolt type is false

    //TODO add xml for all methods below
    public ProductPurchase()
    {
        ProductName = "Unnamed Products";
        Price = 0.0m;
        Quantity = 0;
    }

    public ProductPurchase(string productName, decimal price, int quantity, bool IsSale)
    {
        ProductName = productName; 
        Price = price; 
        Quantity = quantity;
    }

    public virtual decimal GetCost() => Price* Quantity;

    public override string ToString() => $"{ProductName}; {Price}; {Quantity};";

    //TODO c# comparing methodology based on hash codes and you need to add one mor emethod to make it work in right way
    public override bool Equals(object obj) // TODO the copmared object can be not a ProductPurchase instance?
    {
        if (obj is ProductPurchase other)
        {
            return this.ProductName == other.ProductName  //TODO is it neccessary to use this operator here?
                   && this.Price == other.Price;
        }

        return false;
    }
}

//TODO don't use russian language in .net code base
//Develop the first derived class for purchasing a product
//with a fixed sale on price
//and override the necessary methods (GetCost( ) and ToString( )).

//TODO add xml comments the same with previous one
public class FixedSales : ProductPurchase
{
    // do you need the possibility to set thise propeties in external code base?
    public bool IsSale { get; set; } //TODO correct name of property (maybe IsForSale or IsSold)

    public FixedSales(string productName, decimal price, int quantity, bool isSale, bool IsSale) 
        : base(productName, price, quantity, IsSale)
    {
        IsSale = isSale;
    }

    //TODO why virtual method?
    //TODO make all parameters name more understandableВ
    public virtual string FixedSaleCalculate(decimal fixSale, bool isSale, decimal priceWSale)
    {
        fixSale = 0.10m;
        if (isSale)
        {
            priceWSale = priceWSale * fixSale;
        }
        // Always one result? Is it matter to have only one result in the method?
        return $"No sales on: {ProductName}";
    }

    public override decimal GetCost()
    {
        //Suggestion use {} in complex if else constructions
        if (isSale) 
            return priceWSale * Quantity;
        else // TODO don't use unnecessary language operators 
            return Price * Quantity;
    }

    //TODO same logig in comparisson to base class, what is matter to duplicate it in overrided method?
    public override string ToString() 
    {
        return $"{ProductName}; {Price}; {Quantity}";
    }
}

//TODO add xml comments - description about the class goals
public class Sales : ProductPurchase
{
    // do you need the possibility to set these propeties in external code base? || UPD. Through trial and error I realized that it is much better to specify parameters in the parent class
    public bool CheckSale { get; set; }

    public Sales(string productName, decimal price, int quantity, bool checkSale, bool IsSale)
        : base(productName, price, quantity, IsSale)
    {
        CheckSale = checkSale;
        if (Quantity >= 10) //TODO magic number? What is it? || UPD. Discount logic, if there are > 10 items in the cart there will be a discount of a certain amount
        {
            checkSale = true;
        }
    }
}

public class Program
{
    static void Main()
    {
        var pushchase1 = new ProductPurchase("Apple", 40.0m, 4, true);
        var pushchase2 = new ProductPurchase("Apple", 40.0m, 5, false);
        Console.WriteLine($"Price: {pushchase1.GetCost()}, {pushchase1.Quantity}");
        Console.WriteLine(pushchase1.ToString());

        bool areEqual = pushchase1.Equals(pushchase2);
        Console.WriteLine(areEqual);
    }
}