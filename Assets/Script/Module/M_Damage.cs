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

    // 데미지를 받을 함수
    public void TakeDamage(float InDamage, bool isCritical = false, bool IsHill = false)
    {
        if (owner.Stats.CharacterState == ECharacterState.Dead) return;
        if (owner.Movement.GetIsSliding()) return; // 슬라이딩 중일 때는 데미지를 받지 않음



        // 치명타 데미지 적용
        if (isCritical)
        {
            InDamage += (owner.Stats.CharacterStats.CriticalDamage * 0.01f);
        }

        // 데미지 적용
        owner.Stats.CurrentHP -= InDamage;

        

        // 데미지 텍스트 출력
        EDamageType damageType = EDamageType.Normal;
        if (IsHill)
            damageType = EDamageType.Hill;
        else if (isCritical)
            damageType = EDamageType.Critical;

        Game_Mgr.Inst.SpawnDamageText(InDamage, owner.transform.position, damageType);

        
        if (owner.Stats.CurrentHP <= 0f) // 죽었을 때
        {
            Dead();
        }
        else  // 죽지 않았을때
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
        float animationTime = 1.0f; // 흡수 연출은 항상 1초

        // 1. DestroyTime 동안 대기
        yield return new WaitForSeconds(DestroyTime);

        float elapsed = 0.0f;

        // SpriteRenderer 중심 기준
        SpriteRenderer sr = owner.GetComponentInChildren<SpriteRenderer>();
        Vector2 startPos = sr.bounds.center;
        Vector2 startScale = owner.transform.localScale;

        while (elapsed < animationTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / animationTime);

            // 플레이어 현재 위치
            Vector2 endPos = Game_Mgr.Inst.PlayerObject.transform.position;

            // 중심 보간 이동
            Vector2 lerpedPos = Vector2.Lerp(startPos, endPos, t);
            owner.transform.position = lerpedPos - ((Vector2)sr.bounds.center - (Vector2)owner.transform.position);

            // 크기 줄이기
            owner.transform.localScale = Vector2.Lerp(startScale, Vector2.zero, t);

            yield return null;
        }


        // 보상 지급 예정-------

        //---------------------

        if(owner.CharacterType != ECharacterType.Player)
        {
            Character_Monster monster = (Character_Monster)owner;

            Data_Mgr.Score += monster.KillScore;
        }
        
        // 제거
        Destroy(owner.gameObject);
    }

}
