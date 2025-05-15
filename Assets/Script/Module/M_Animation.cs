using UnityEngine;

public class M_Animation : Module_Base
{
    Animator OwnerAnimator;

    


    void Start()
    {
        OwnerAnimator = owner.GetComponent<Animator>();
    }

    
    public void WalikingAnimator(float moveX)
    {
        bool bOnMove = moveX != 0;

        //if(!OwnerAnimator.GetBool("Falling"))
            OwnerAnimator.SetBool("OnWaliking", bOnMove);


        // 좌우 반전
        if (bOnMove)
        {
            Vector3 scale = owner.transform.localScale;
            scale.x = Mathf.Abs(scale.x) * -Mathf.Sign(moveX); // +1 또는 -1 방향으로 고정
            owner.transform.localScale = scale;
        }
           
    }

    public void OnJump()
    {
        OwnerAnimator.SetTrigger("JumpTrigger");
    }

    public void OnFalling(bool bOnFalling)
    {
        OwnerAnimator.SetBool("Falling", bOnFalling);
    }

    public void OnSliding(bool bOnSliding)
    {
        OwnerAnimator.SetBool("Sliding", bOnSliding);
    }
}
