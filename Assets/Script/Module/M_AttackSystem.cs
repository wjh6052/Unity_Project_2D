using UnityEngine;

public class M_AttackSystem : Module_Base
{

    int CurCombo = 0;
    int MixCombo = -1;

    bool NextCombo = false;


    void Start()
    {
        // 콤보공격의 갯수 등록
        CheckAttackCount();
    }


    void CheckAttackCount()
    {
        int count = -1;

        while(true)
        {
            count++;

            int stateHash = Animator.StringToHash($"{owner.CharacterType.ToString()} Attack{count}");
            if (!owner.Animation.OwnerAnimator.HasState(0, stateHash)) // 0은 기본 Layer
            {
                break;
            }
        }
        
        MixCombo = count;
    }

    public void OnAttack()
    {
        if (owner.Stats.CharacterState == ECharacterState.Attacking)
        {
            if (CurCombo + 1 < MixCombo)
                NextCombo = true;
            else
                NextCombo = false;
        }
        else 
        {
            if (owner.Stats.CharacterState != ECharacterState.Idle) return;

            CurCombo = 0;
            owner.Stats.CharacterState = ECharacterState.Attacking;
            owner.Animation.AttackStart(CurCombo);
                
        }       
    }

    public void EndAttack()
    {
        if(NextCombo)
        {
            NextAttack();
        }
        else
        {
            CurCombo = 0;
            NextCombo = false;
            owner.Stats.CharacterState = ECharacterState.Idle;
            owner.Animation.IdleTrigger();
        }
    }

    void NextAttack()
    {
        CurCombo++;
        owner.Animation.AttackStart(CurCombo);
        NextCombo = false;
    }


}
