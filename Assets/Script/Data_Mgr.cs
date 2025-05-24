using UnityEngine;




public class Data_Mgr
{
    // �ִ� HP
    public static float AddMaxHP = 0;
    // �ִ� ���׹̳�
    public static float AddMaxStamina = 0;


    // ���ݷ�
    public static float AddAttackPower = 0;
    // ũ��Ƽ�� Ȯ��
    public static float AddCriticalRate = 0;
    // ũ��Ƽ�� ������
    public static float AddCriticalDamage = 0;


    // ���
    public static int PlayerGold = 0;
    public static int Score = 0;
    //public static int BastScore = 0;




    // ������ ����
    public static void SaveGameData()
    {
        PlayerPrefs.SetFloat("AddMaxHP", AddMaxHP);
        PlayerPrefs.SetFloat("AddMaxStamina", AddMaxStamina);
                    
        PlayerPrefs.SetFloat("AddAttackPower", AddAttackPower);
        PlayerPrefs.SetFloat("AddCriticalRate", AddCriticalRate);
        PlayerPrefs.SetFloat("AddCriticalDamage", AddCriticalDamage);
                    
                    
        PlayerPrefs.SetInt("PlayerGold", PlayerGold);
    }


    // ������ ����
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