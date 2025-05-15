using UnityEngine;

public enum ECharacterType
{
    none,
    Player
}

public class Character_Base : MonoBehaviour
{
    // 스텟
    [HideInInspector]
    public M_Stats Stats;

    // 움직임
    [HideInInspector]
    public M_Movement Movement;

    // 애니메이터
    [HideInInspector]
    public M_Animation Animation;

    // 공격기능
    [HideInInspector]
    public M_AttackSystem AttackSystem;


    // 캐릭터의 타입
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
