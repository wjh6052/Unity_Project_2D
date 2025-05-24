using UnityEngine;




public class Data_Mgr
{
    // 최대 HP
    public static float AddMaxHP = 0;
    // 최대 스테미너
    public static float AddMaxStamina = 0;


    // 공격력
    public static float AddAttackPower = 0;
    // 크리티컬 확률
    public static float AddCriticalRate = 0;
    // 크리티컬 데미지
    public static float AddCriticalDamage = 0;


    // 골드
    public static int PlayerGold = 0;
    public static int Score = 0;
    //public static int BastScore = 0;




    // 데이터 저장
    public static void SaveGameData()
    {
        PlayerPrefs.SetFloat("AddMaxHP", AddMaxHP);
        PlayerPrefs.SetFloat("AddMaxStamina", AddMaxStamina);
                    
        PlayerPrefs.SetFloat("AddAttackPower", AddAttackPower);
        PlayerPrefs.SetFloat("AddCriticalRate", AddCriticalRate);
        PlayerPrefs.SetFloat("AddCriticalDamage", AddCriticalDamage);
                    
                    
        PlayerPrefs.SetInt("PlayerGold", PlayerGold);
    }


    // 데이터 저장
    public static void LoadGameData()
    {
        AddMaxHP = PlayerPrefs.GetFloat("AddMaxHP", 0);
        AddMaxStamina = PlayerPrefs.GetFloat("AddMaxStamina", 0);

        AddAttackPower = PlayerPrefs.GetFloat("AddAttackPower", 0);
        AddCriticalRate = PlayerPrefs.GetFloat("AddCriticalRate", 0);
        AddCriticalDamage = PlayerPrefs.GetFloat("AddCriticalDamage", 0);


        PlayerGold = PlayerPrefs.GetInt("PlayerGold", 0);
    }
}