using System;

public static class LevelCosts
{
    private static readonly byte[] Costs = { 1, 2, 3, 5, 8, 13, 21, 34, 50, 100 };
    
    // TODO: make this somewhat optimal by saving the results to a list each time it is accessed, so it doesn't need to be calculated again.
    // LONG TODO: Save those precalcs into file and load them too? idk probably not would be a longer initial load, but optimize the run-time.
    // These optimizations come at the cost of memory space, instead of cpu time.

    /// <summary>
    /// Internal calculation of the Tier cost.
    /// This will return the multipler that goes against the grade cost for a provided Tier.
    /// This also calculates the flat additive that is applied to all
    /// </summary>
    /// <param name="rank"></param>
    /// <returns></returns>
    private static double TierCost(short rank) 
    {
        // This has not been tested against the really high values that would exceed an int.
        // It might not work high enough to be worth ulong yet.

        // Old method:
        // return (ulong)(Costs[(rank-1)%10] * 10^(rank) * 100^((rank-1)/10));

        // New Method:
        if (rank < 0) 
        {
            return 0;
        }
        else if (rank < 4)
        {
            return Math.Pow(9, rank) + Math.Pow(rank, rank + 9) - Math.Pow(rank, 2);
        }
        else 
        {
            return Math.Pow(rank, rank + 9);
        }
    }

    /// <summary>
    /// This function will calculate the level cost for a given Level, Rank, Multiplier and Steps.
    /// </summary>
    /// <param name="level"> The Skill's Level </param>
    /// <param name="maxLevel"> The Skill's Max Level per Rank. </param>
    /// <param name="rank"> The Skill's Rank </param>
    /// <param name="baseMulti"> The Cost multiplier.  Defaults to 1. </param>
    /// <param name="steps"> The Steps count. More steps requires more levels to go up in cost. </param>
    /// <returns></returns>
    public static double CalculateCost(byte level, byte rank, byte baseMulti = 1, byte steps = 1) 
    {
        // TODO: Test the case where Steps is anything other than 1.  We need to make sure that doesn't break anything...


        // Leveling Cost formula:
        // Bm(((9^x) + (x^(x+9) - x^2))(Gc) + ((9^(x-1)) + ((x-1)^((x-1)+9) - (x-1)^2)))
        // x = rank
        // Gc = Grade cost
        // Bm = Base Multiplier

        return baseMulti * (Costs[GetFinalLevel(level, steps)] * TierCost(rank) + (100*TierCost((short)(rank-1))));
        /*
        if (rank == 0)
        {
            return baseMulti * Costs[level / steps];
        }
        else if (level+steps > maxLevel )
        {
            return baseMulti * (Costs[level / steps] * TierCost(rank));
        }
        else 
        {
            return baseMulti * (Costs[level / steps] * TierCost(rank) + TierCost((byte)(rank - 1)));
        }
        */
    }

    private static short GetFinalLevel(byte level, byte steps) 
    {
        return (short)(level / steps);
    }
}
