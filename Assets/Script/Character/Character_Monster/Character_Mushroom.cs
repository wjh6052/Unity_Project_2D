using UnityEngine;

public class Character_Mushroom : Character_Base
{
    GameObject TargetPlayer;

    // 멈추는 공격 사거리
    float AttackRange = 1;


    protected override void Awake()
    {
        base.Awake();

        CharacterType = ECharacterType.Mushroom;
    }

    protected override void Start()
    {

    }


    protected override void Update()
    {
        if (this.Stats.CharacterState == ECharacterState.Idle)
        {
            if (!TargetPlayer)
                TargetPlayer = Game_Mgr.Inst.PlayerObject.gameObject;

            Vector2 dis = TargetPlayer.transform.position - this.transform.position;
           
            if(dis.magnitude >= AttackRange) // 공격사거리 밖인 경우
            {
                dis.Normalize();
                this.Movement.SetMove(dis);
            }
            else // 공격사러기 안일 경우
            {
                this.Movement.SetMove(Vector2.zero);
                this.AttackSystem.OnAttack();
            }
        }

        else
        {
            this.Movement.SetMove(Vector2.zero);
        }

        

    }
}
