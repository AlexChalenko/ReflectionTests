using System.Text;

public class SimpleCSVSerializer
{
    public static string Serialize(object obj)
    {
        var type = obj.GetType();
        var sb = new StringBuilder();
        foreach (var field in type.GetFields())
        {
            sb.Append($"{field.GetValue(obj)},");
        }
        foreach (var prop in type.GetProperties())
        {
            sb.Append($"{prop.GetValue(obj, null)},");
        }
        // Удаляем последнюю запятую
        if (sb.Length > 0)
            sb.Remove(sb.Length - 1, 1);
        return sb.ToString();
    }

    public static T Deserialize<T>(string csv) where T : new()
    {
        T obj = new T();
        var type = typeof(T);
        string[] parts = csv.Split(',');
        int index = 0;

        foreach (var field in type.GetFields())
        {
            if (index < parts.Length)
            {
                field.SetValue(obj, Convert.ChangeType(parts[index], field.FieldType));
                index++;
            }
        }

        foreach (var prop in type.GetProperties())
        {
            if (index < parts.Length && prop.CanWrite)
            {
                prop.SetValue(obj, Convert.ChangeType(parts[index], prop.PropertyType), null);
                index++;
            }
        }

        return obj;
    }
}
