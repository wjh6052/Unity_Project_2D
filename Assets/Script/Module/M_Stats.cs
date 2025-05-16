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
        set => _characterState = value;
    }


    [Header("�⺻ ����")]
    public int MaxHP = 100;
    public int CurrentHP;

    public int MaxMP = 50;
    public int CurrentMP;

    public int MaxStamina = 100;
    public int CurrentStamina;

    public float AttackPower = 10;
    public float CriticalRate = 50;
    public float CriticalDamage = 1.2f;


    // hit�� ���ߴ� �ð�
    public float HitStopDuration = 1.0f;


    public float Speed = 3;
    public float JumpPower = 6;

    // �����̵�
    public float SlideSpeed = 12.5f;    // �����̵� �ӵ�
    public float SlideDirection = 2f;   // �����̵� �ð�

    


    private void Start()
    {
        CurrentHP = MaxHP;
        CurrentMP = MaxMP;
        CurrentStamina = MaxStamina;
    }

}
