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
    private ECharacterState _CharacterState = ECharacterState.Idle;

    public ECharacterState CharacterState
    {
        get => _CharacterState;
        set
        {
            if (_CharacterState == ECharacterState.Dead) return;

            _CharacterState = value;
        }

    }


    // 스텟
    FCharacterStats _CharacterStats;

    public FCharacterStats CharacterStats
    {
        get
        {
            FCharacterStats temp = _CharacterStats;

            if (owner.CharacterType == ECharacterType.Player)
            {
                temp.MaxHP += Data_Mgr.AddMaxHP;
                temp.MaxStamina += Data_Mgr.AddMaxStamina;
                temp.AttackPower += Data_Mgr.AddAttackPower;
                temp.CriticalRate += Data_Mgr.AddCriticalRate;
                temp.CriticalDamage += Data_Mgr.AddCriticalDamage;
            }

            return temp;
        }
        set => _CharacterStats = value;
    }


    float _CurrentHP;
    public float CurrentHP
    {
        get => _CurrentHP;
        set
        {
            if (_CharacterState == ECharacterState.Dead) return;

            _CurrentHP = value;

            if(owner.CharacterType == ECharacterType.Player)
                Game_Mgr.Inst.UIUpdate();
        }

    }

    // 스테미너
    float _CurrentStamina;
    public float CurrentStamina
    {
        get => _CurrentStamina;
        set
        {
            if (_CharacterState == ECharacterState.Dead) return;

            _CurrentStamina = value;

            if (owner.CharacterType == ECharacterType.Player)
            {
                Game_Mgr.Inst.UIUpdate();
            }
        }
    }



    // 플레이어 전용
    // 슬라이딩
    public float SlideSpeed = 8f;    // 슬라이딩 속도
    public float SlideDirection = 0.3f;   // 슬라이딩 시간
    









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


    private void Update()
    {
        // 스테미너가 풀인 아닌 경우
        if(_CurrentStamina <= _CharacterStats.MaxStamina)
        {
            CurrentStamina += Time.deltaTime * 8;
        }

    }


}
