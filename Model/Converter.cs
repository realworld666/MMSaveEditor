using System.Collections.Generic;

public abstract class Converter<T>  where T : Person
{
    public static List<Person> Convert(List<T> list)
    {
        List<Person> result = new List<Person>();
        foreach (T person in list)
            result.Add(person);
        
        return result;
    }
}
