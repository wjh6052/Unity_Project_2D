using UnityEngine;


[System.Serializable]
public struct FCharacterStats
{
    [Header("ĳ���� ����")]
    public ECharacterType CharacterType;


    [Header("�ִ� ü��")]
    public float MaxHP;

    [Header("�ִ� ���׹̳�")]
    public float MaxStamina;


    [Header("���ݷ�")]
    public float AttackPower;
    [Header("ġ��Ÿ Ȯ�� %")]
    public float CriticalRate;
    [Header("ġ��Ÿ ������ %")]
    public float CriticalDamage;

    [Header("hit�� ���ߴ� �ð�")]
    public float HitStopDuration;


    [Header("�̵��ӵ�")]
    public float Speed;
    [Header("���� �Ŀ�")]
    public float JumpPower;


    [Header("���� �Ŀ�")]
    public float SlidingStaminaCost; // �����̵� �� �Ҹ�Ǵ� ���׹̳� ��
}

[CreateAssetMenu(fileName = "_StatsInfo", menuName = "Add CharacterInfo/CharacterStatsInfo")]
public class CharacterStatsInfo : ScriptableObject
{
    public FCharacterStats CharacterStats;

}
