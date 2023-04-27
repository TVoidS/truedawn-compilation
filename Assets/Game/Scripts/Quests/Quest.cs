using System.Collections.Generic;

public class Quest 
{
    private readonly Dictionary<string, uint> triggers = new();
    private readonly Dictionary<string, uint> requirements = new();



    public Quest() { }
}