using System.Collections.Generic;

public static class LevelCosts
{
    private static readonly ushort[] Costs = { 1, 2, 3, 5, 8, 13, 21, 34, 50, 100 };
    
    // TODO: make this somewhat optimal by saving the results to a list each time it is accessed, so it doesn't need to be calculated again.
    // LONG TODO: Save those precalcs into file and load them too? idk probably not would be a longer initial load, but optimize the run-time.
    // These optimizations come at the cost of memory space, instead of cpu time.

    private static ulong TierCost(ushort rank) 
    {
        // This has not been tested against the really high values that would exceed an int.
        // It might not work high enough to be worth ulong yet.
        return (ulong)(Costs[(rank-1)%10] * 10^(rank) * 100^((rank-1)/10));
    }

    public static ulong CalculateCost(ushort level, ushort maxLevel, ushort rank, byte baseMulti = 1, byte steps = 1) 
    {
        // Check if the Level is in the final step
        if (level >= maxLevel - steps + 1)
        {
            return TierCost(rank) * baseMulti * Costs[9];
        }
        else if (level != 0)
        {
            return TierCost((ushort)(rank - 1)) * baseMulti * 100 + TierCost(rank) * baseMulti * Costs[level/ steps];
        }
        else 
        {
            return TierCost((ushort)(rank - 1)) * baseMulti * 100 + TierCost(rank) * baseMulti * Costs[0];
        }
    }
}
