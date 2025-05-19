using UnityEngine;

public class GlobalValue
{
    public static CharacterStatsInfo[] CharacterStatsArr = null;



    public static void InitData()
    {
        CharacterStatsArr = null;
        CharacterStatsArr = Resources.LoadAll<CharacterStatsInfo>("StatsInfo");

        
    }

}
