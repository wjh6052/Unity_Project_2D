using UnityEngine;

public enum ECharacterState
{
    Idle,
    Falling,
    Sliding,
    Attacking,
    Hit,
    Dead
}


public class M_Stats : Module_Base
{
    // 캐릭터의 상태
    private ECharacterState _characterState = ECharacterState.Idle;

    public ECharacterState CharacterState
    {
        get => _characterState;
        set
        {
            if (_characterState == ECharacterState.Dead) return;

            _characterState = value;
        }

    }


    FCharacterStats CharacterStats;

    public FCharacterStats GetCharacterStats() { return CharacterStats; }

    public float CurrentHP;
    public float CurrentStamina;


    // 플레이어 전용
    // 슬라이딩
    public float SlideSpeed = 12.5f;    // 슬라이딩 속도
    public float SlideDirection = 1f;   // 슬라이딩 시간


    


    

  
    void Start()
    {
        foreach(CharacterStatsInfo statsInfo in GlobalValue.CharacterStatsArr)
        {
            if (statsInfo.CharacterStats.CharacterType == owner.CharacterType)
            {
                this.CharacterStats = statsInfo.CharacterStats;

                CurrentHP = CharacterStats.MaxHP;
                CurrentStamina = CharacterStats.MaxStamina;
            }
        }

        
    }

}
