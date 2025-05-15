using UnityEngine;

public enum ECharacterType
{
    none,
    Player
}

public class Character_Base : MonoBehaviour
{
    // ����
    [HideInInspector]
    public M_Stats Stats;

    // ������
    [HideInInspector]
    public M_Movement Movement;

    // �ִϸ�����
    [HideInInspector]
    public M_Animation Animation;

    // ���ݱ��
    [HideInInspector]
    public M_AttackSystem AttackSystem;


    // ĳ������ Ÿ��
    public ECharacterType CharacterType;


    protected virtual void Awake()
    {
        Movement = gameObject.AddComponent<M_Movement>();
        Movement.SetOwner(this);

        Stats = gameObject.AddComponent<M_Stats>();
        Stats.SetOwner(this);

        Animation = gameObject.AddComponent<M_Animation>();
        Animation.SetOwner(this);

        AttackSystem = gameObject.AddComponent<M_AttackSystem>();
        AttackSystem.SetOwner(this);
        
    }


    protected virtual void Start()
    {
        
    }


    protected virtual void Update()
    {
        
    }
}
