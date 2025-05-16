using UnityEngine;

public class Character_Mushroom : Character_Base
{
    GameObject TargetPlayer;

    // ���ߴ� ���� ��Ÿ�
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
           
            if(dis.magnitude >= AttackRange) // ���ݻ�Ÿ� ���� ���
            {
                dis.Normalize();
                this.Movement.SetMove(dis);
            }
            else // ���ݻ緯�� ���� ���
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
