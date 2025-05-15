using UnityEngine;

public enum ECharacterState
{
    Idle,
    Falling,
    Sliding,
    Attacking,
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

    public int AttackPower = 10;

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
