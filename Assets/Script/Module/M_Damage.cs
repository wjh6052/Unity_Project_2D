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

    // �������� ���� �Լ�
    public void TakeDamage(float InDamage, bool IsCritical, bool IsHill = false)
    {
        EDamageType damageType = EDamageType.Normal;
        if (IsHill)
            damageType = EDamageType.Hill;
        else if (IsCritical)
            damageType = EDamageType.Critical;


        // ���� ����
        owner.Stats.CharacterState = ECharacterState.Hit;

        // ��Ʈ �ִϸ��̼� ���
        owner.Animation.OnHit();

        // ������ �ؽ�Ʈ ���
        Game_Mgr.Inst.SpawnDamageText(InDamage, owner.transform.position, damageType);
    }

    public void EndHit()
    {
        HitStopDurationTime = owner.Stats.HitStopDuration;
        owner.Animation.IdleTrigger();
    }
}
