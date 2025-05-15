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
    public int maxHP = 100;
    public int currentHP;

    public int maxMP = 50;
    public int currentMP;

    public int maxStamina = 100;
    public int currentStamina;

    public int attackPower = 10;

    public float Speed = 3;
    public float JumpPower = 6;

    // �����̵�
    public float SlideSpeed = 12.5f;    // �����̵� �ӵ�
    public float SlideDirection = 2f;   // �����̵� �ð�

    


    private void Start()
    {
        currentHP = maxHP;
        currentMP = maxMP;
        currentStamina = maxStamina;
    }

}
