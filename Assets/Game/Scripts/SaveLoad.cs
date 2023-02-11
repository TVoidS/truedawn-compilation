using System.Text.Json;

public static class SaveLoad
{
    public static string Save(object data) 
    {
        return JsonSerializer.Serialize(data);
    }
}
