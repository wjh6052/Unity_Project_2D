using UnityEngine;

public class M_Animation : Module_Base
{
    public Animator OwnerAnimator;

    


    void Start()
    {
        OwnerAnimator = owner.Mesh.GetComponent<Animator>();
    }

    bool IsAnimParameter(string ParameterName)
    {
        foreach (AnimatorControllerParameter param in OwnerAnimator.parameters)
        {
            if (param.name == ParameterName)
                return true;
        }
        return false;
    }

    // 애니메이션 파라미터
    public void IdleTrigger()
    {
        if (!IsAnimParameter("IdleTrigger")) return;

        OwnerAnimator.SetTrigger("IdleTrigger");
    }

    public void WalikingAnimator(float moveX)
    {
        if (!IsAnimParameter("OnWaliking")) return;

        bool bOnMove = moveX != 0;

        OwnerAnimator.SetBool("OnWaliking", bOnMove);


        // 좌우 반전
        if (bOnMove)
        {
            Vector3 scale = owner.transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(moveX); // +1 또는 -1 방향으로 고정
            owner.transform.localScale = scale;
        }
           
    }

    public void OnJump()
    {
        if (!IsAnimParameter("JumpTrigger")) return;

        OwnerAnimator.SetTrigger("JumpTrigger");
    }

    public void OnFalling(bool bOnFalling)
    {
        if (!IsAnimParameter("Falling")) return;

        OwnerAnimator.SetBool("Falling", bOnFalling);
    }

    public void OnSliding(bool bOnSliding)
    {
        if (!IsAnimParameter("Sliding")) return;

        OwnerAnimator.SetBool("Sliding", bOnSliding);
    }

    public void AttackStart(int SkillNum)
    {
        if(SkillNum == 0)
        {
            if (!IsAnimParameter("AttackTrigger")) return;

            OwnerAnimator.SetTrigger("AttackTrigger");
        }
        else
        {
            int stateHash = Animator.StringToHash($"{owner.CharacterType} Attack{SkillNum}");
            if (!OwnerAnimator.HasState(0, stateHash)) return;

            OwnerAnimator.Play($"{owner.CharacterType.ToString()} Attack{SkillNum}");
        }

        
    }

    public void OnHit()
    {
        if (!IsAnimParameter("HitTrigger")) return;

        OwnerAnimator.SetTrigger("HitTrigger");
    }


}
