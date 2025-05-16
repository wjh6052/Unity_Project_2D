using UnityEngine;

public enum EDamageType
{
    Normal,
    Critical,
    Hill
}

public class M_Damage : Module_Base
{
    float HitStopDurationTime = 0;

    void Start()
    {
        
    }

    private void Update()
    {
        if(HitStopDurationTime > 0)
        {
            HitStopDurationTime -= Time.deltaTime;
            if (HitStopDurationTime <= 0)
            {
                owner.Stats.CharacterState = ECharacterState.Idle;
            }
        }
       
    }

    // 데미지를 받을 함수
    public void TakeDamage(float InDamage, bool IsCritical, bool IsHill = false)
    {
        EDamageType damageType = EDamageType.Normal;
        if (IsHill)
            damageType = EDamageType.Hill;
        else if (IsCritical)
            damageType = EDamageType.Critical;


        // 상태 변경
        owner.Stats.CharacterState = ECharacterState.Hit;

        // 히트 애니메이션 출력
        owner.Animation.OnHit();

        // 데미지 텍스트 출력
        Game_Mgr.Inst.SpawnDamageText(InDamage, owner.transform.position, damageType);
    }

    public void EndHit()
    {
        HitStopDurationTime = owner.Stats.HitStopDuration;
        owner.Animation.IdleTrigger();
    }
}
