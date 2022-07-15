using System.Reflection;

namespace Reflection;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Display Public Properties:");
        DisplayPublicProperties();

        Console.WriteLine("\nCreate Instance:");
        CreateInstance();
    }

    static void DisplayPublicProperties()
    {
        var type = GetTypeOf<Student>();

        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var property in properties)
        {
            Console.WriteLine("{0}", property.Name);
        }
    }

    static void CreateInstance()
    {
        var type = GetTypeOf<Student>();

        var instance = (Student)Activator.CreateInstance(type);

        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var property in properties)
        {
            property.SetValue(instance, GetValueOfType(property.PropertyType));
        }

        var methodInfo = type.GetMethod("DisplayInfo");
        methodInfo!.Invoke(instance, null);
    }

    private static object? GetValueOfType(Type type)
    {
        if (type.Equals(typeof(string)))
        {
            return "Valor";
        }
        else if (type.Equals(typeof(int)))
        {
            return 10;
        }

        return null;
    }

    static Type? GetTypeOf<T>()
    {
        var types = Assembly.GetExecutingAssembly().GetTypes();

        foreach (var type in types)
        {
            if (type.Equals(typeof(T)))
            {
                return type;
            }
        }

        return null;
    }
}