using UnityEngine;

public class GlobalValue
{
    public static CharacterStatsInfo[] CharacterStatsArr = null;



    public static void InitData()
    {
        CharacterStatsArr = null;

        CharacterStatsInfo[] temp = Resources.LoadAll<CharacterStatsInfo>("StatsInfo");
        if (temp[0])
            CharacterStatsArr = temp;
    }

}
