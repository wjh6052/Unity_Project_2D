using UnityEngine;

public enum ECharacterType
{
    none,
    Player,
    Mushroom
}

public class Character_Base : MonoBehaviour
{
    // 메쉬
    [HideInInspector]
    public GameObject Mesh;

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

    // 데미지 처리
    [HideInInspector]
    public M_Damage Damage;

    // 캐릭터의 타입
    public ECharacterType CharacterType;

    

    protected virtual void Awake()
    {
        Transform meshTransform =  this.transform.Find("Mesh");
        if (meshTransform != null)
        {
            Mesh = meshTransform.gameObject;
        }

        Movement = gameObject.AddComponent<M_Movement>();
        Movement.SetOwner(this);

        Stats = gameObject.AddComponent<M_Stats>();
        Stats.SetOwner(this);

        Animation = gameObject.AddComponent<M_Animation>();
        Animation.SetOwner(this);

        AttackSystem = gameObject.AddComponent<M_AttackSystem>();
        AttackSystem.SetOwner(this);

        Damage = gameObject.AddComponent<M_Damage>();
        Damage.SetOwner(this);

    }


    protected virtual void Start()
    {
        
    }


    protected virtual void Update()
    {
        
    }
}
