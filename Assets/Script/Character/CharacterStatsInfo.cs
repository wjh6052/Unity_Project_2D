using UnityEngine;


[System.Serializable]
public struct FCharacterStats
{
    [Header("캐릭터 종류")]
    public ECharacterType CharacterType;


    [Header("최대 체력")]
    public float MaxHP;

    [Header("최대 스테미나")]
    public float MaxStamina;


    [Header("공격력")]
    public float AttackPower;
    [Header("치명타 확률 %")]
    public float CriticalRate;
    [Header("치명타 데미지 %")]
    public float CriticalDamage;

    [Header("hit후 멈추는 시간")]
    public float HitStopDuration;


    [Header("이동속도")]
    public float Speed;
    [Header("점프 파워")]
    public float JumpPower;


    [Header("점프 파워")]
    public float SlidingStaminaCost; // 슬라이딩 시 소모되는 스테미너 양
}

[CreateAssetMenu(fileName = "_StatsInfo", menuName = "Add CharacterInfo/CharacterStatsInfo")]
public class CharacterStatsInfo : ScriptableObject
{
    public FCharacterStats CharacterStats;

}
