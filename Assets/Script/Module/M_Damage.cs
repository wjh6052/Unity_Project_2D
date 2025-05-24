using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum EDamageType
{
    Normal,
    Critical,
    Hill
}

public class M_Damage : Module_Base
{
    float HitStopDurationTime = 0;

    bool HitDuration = false;

    void Start()
    {
        
    }

    private void Update()
    {
        if (owner.Stats.CharacterState == ECharacterState.Hit && HitDuration)
        {
            HitStopDurationTime -= Time.deltaTime;
            if (HitStopDurationTime <= 0)
            {
                owner.Stats.CharacterState = ECharacterState.Idle;
                HitDuration = false;
            }
        }
        

    }

    // �������� ���� �Լ�
    public void TakeDamage(float InDamage, bool isCritical = false, bool IsHill = false)
    {
        if (owner.Stats.CharacterState == ECharacterState.Dead) return;
        if (owner.Movement.GetIsSliding()) return; // �����̵� ���� ���� �������� ���� ����



        // ġ��Ÿ ������ ����
        if (isCritical)
        {
            InDamage += (owner.Stats.CharacterStats.CriticalDamage * 0.01f);
        }

        // ������ ����
        owner.Stats.CurrentHP -= InDamage;

        

        // ������ �ؽ�Ʈ ���
        EDamageType damageType = EDamageType.Normal;
        if (IsHill)
            damageType = EDamageType.Hill;
        else if (isCritical)
            damageType = EDamageType.Critical;

        Game_Mgr.Inst.SpawnDamageText(InDamage, owner.transform.position, damageType);

        
        if (owner.Stats.CurrentHP <= 0f) // �׾��� ��
        {
            Dead();
        }
        else  // ���� �ʾ�����
        {
            owner.Stats.CharacterState = ECharacterState.Hit;

            owner.Animation.OnHit();
        }
    }


    public void EndHit()
    {
        if (owner.Stats.CharacterState == ECharacterState.Dead) return;

        HitStopDurationTime = owner.Stats.CharacterStats.HitStopDuration;
        HitDuration = true;

        owner.Animation.IdleTrigger();
    }


    public void Dead()
    {
        owner.Stats.CharacterState = ECharacterState.Dead;
        owner.Movement.SetMove(Vector2.zero);


        owner.Animation.DeadTrigger();
    }
   

    public IEnumerator Dead(float DestroyTime = 1.0f)
    {
        float animationTime = 1.0f; // ��� ������ �׻� 1��

        // 1. DestroyTime ���� ���
        yield return new WaitForSeconds(DestroyTime);

        float elapsed = 0.0f;

        // SpriteRenderer �߽� ����
        SpriteRenderer sr = owner.GetComponentInChildren<SpriteRenderer>();
        Vector2 startPos = sr.bounds.center;
        Vector2 startScale = owner.transform.localScale;

        while (elapsed < animationTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / animationTime);

            // �÷��̾� ���� ��ġ
            Vector2 endPos = Game_Mgr.Inst.PlayerObject.transform.position;

            // �߽� ���� �̵�
            Vector2 lerpedPos = Vector2.Lerp(startPos, endPos, t);
            owner.transform.position = lerpedPos - ((Vector2)sr.bounds.center - (Vector2)owner.transform.position);

            // ũ�� ���̱�
            owner.transform.localScale = Vector2.Lerp(startScale, Vector2.zero, t);

            yield return null;
        }


        // ���� ���� ����-------

        //---------------------

        if(owner.CharacterType != ECharacterType.Player)
        {
            Character_Monster monster = (Character_Monster)owner;

            Data_Mgr.Score += monster.KillScore;
        }
        
        // ����
        Destroy(owner.gameObject);
    }

}
