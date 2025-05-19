using UnityEngine;

public class M_AttackSystem : Module_Base
{

    int CurCombo = 0;
    int MixCombo = -1;

    bool NextCombo = false;


    private Vector2 debugBoxCenter;
    private Vector2 debugBoxSize;
    private bool showDebugBox = false;

    void Start()
    {
        // �޺������� ���� ���
        CheckAttackCount();
    }


    void CheckAttackCount()
    {
        int count = -1;

        while(true)
        {
            count++;

            int stateHash = Animator.StringToHash($"{owner.CharacterType.ToString()} Attack{count}");
            if (!owner.Animation.OwnerAnimator.HasState(0, stateHash)) // 0�� �⺻ Layer
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


    public void AttackHitCheck(float range)
    {
        // ����
        float direction = owner.transform.localScale.x > 0 ? 1f : -1f;



        // �ڽ��� ũ��� �߽�
        SpriteRenderer Sprite = owner.Mesh.GetComponent<SpriteRenderer>();
        float height = Sprite.bounds.size.y;
        float width = range;


        Vector2 boxSize = new Vector2(width, height);


        //owner.GetComponent<CapsuleCollider2D>().transform.position;
        Vector2 origin = owner.transform.position;
        //origin.y += Sprite.bounds.size.y /2;
        Vector2 boxCenter = origin + new Vector2(direction * (range / 2), 0);

        

        // ���̾�
        LayerMask Layer = owner.CharacterType == ECharacterType.Player 
            ? LayerMask.GetMask("Monster") : LayerMask.GetMask("Player");


        // �浹 �˻�
        Collider2D[] hits = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f, Layer);


        foreach (var hit in hits)
        {
            Character_Base character = hit.gameObject.GetComponent<Character_Base>();
            if(character)
            {
                float damage = owner.Stats.GetCharacterStats().AttackPower;
                damage += (damage * (float)CurCombo / 5);

                bool isCritical = owner.Stats.GetCharacterStats().CriticalRate >= Random.Range(0f, 100.0f);

                character.Damage.TakeDamage(damage, isCritical);
            }

        }

        debugBoxCenter = boxCenter;
        debugBoxSize = boxSize;
        showDebugBox = true;

    }

    private void OnDrawGizmos()
    {
        if (showDebugBox)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(debugBoxCenter, debugBoxSize);
        }
    }

}
