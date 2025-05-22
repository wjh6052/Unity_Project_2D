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
    // ĳ������ ����
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


    // �÷��̾� ����
    // �����̵�
    public float SlideSpeed = 12.5f;    // �����̵� �ӵ�
    public float SlideDirection = 1f;   // �����̵� �ð�


    


    

  
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
