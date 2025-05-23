using UnityEngine;




public class M_Movement : Module_Base
{
    Rigidbody2D Rig2D;
    CapsuleCollider2D OwnerCollider;
    Vector2 ColliderSize;


    // Move
    Vector2 moveInput;


    //Jump
    public LayerMask GroundLayer;           // ĳ���� �ϴܿ� ��ġ�� �� ������Ʈ
    public float groundCheckRadius = 0.2f;  // �ٴ� üũ ���� (���� ��)
    float footOffset;

    bool IsGrounded = false;
    bool IsJumping = false;


    // �����̵�
    bool IsSliding = false;
    public bool GetIsSliding() { return IsSliding; }

    float SlideTimer = 0f;


    // �� ����
    bool IsDropping = false;

    float DropDuration = 0.5f;      // �ݸ����� ������ �ð�
    float DropDurationTime = 0.0f;  


    void Start()
    {
        Rig2D = owner.GetComponent<Rigidbody2D>();

        OwnerCollider = owner.GetComponent<CapsuleCollider2D>();
        ColliderSize = OwnerCollider.size;

        GroundLayer = LayerMask.GetMask("Ground");

        footOffset = owner.Mesh.GetComponent<SpriteRenderer>().bounds.extents.y;
    }



    void Update()
    {
        // �����̵�
        {
            if (IsSliding)
            {
                MoveSliding();
                return;
            }
        }


        if (owner.Stats.CharacterState == ECharacterState.Dead || owner.Stats.CharacterState == ECharacterState.Hit)
        {
            return;
        }
        // ���� ����
        {
            IsGrounded = Physics2D.Raycast(owner.transform.position, Vector2.down, footOffset + groundCheckRadius, GroundLayer);

            if (!IsGrounded)
            {
                owner.Stats.CharacterState = ECharacterState.Falling;
                owner.Animation.OnFalling(true);
            }
            else
            {
                EndFalling();
            }

            // �� ����
            if(IsDropping)
            {
                DropDurationTime -= Time.deltaTime;

                if(DropDurationTime <= 0)
                {
                    IsDropping = false;
                    OwnerCollider.enabled = true; // �ݸ��� Ȱ��ȭ
                }
                
            }
        }



        // �̵� ����
        {
            Move();
        }
        
    }

    // �����̵�
    public void OnSliding()
    {
        switch(owner.Stats.CharacterState)
        {
            case ECharacterState.Idle:
                break;

            case ECharacterState.Attacking:
                owner.AttackSystem.EndAttack();
                break;

            default:
                return;
        }

        SlideTimer = owner.Stats.SlideDirection;
        IsSliding = true;

        // ���� ����
        owner.Stats.CharacterState = ECharacterState.Sliding;

        // �ִϸ��̼� ����
        owner.Animation.OnSliding(true);
    }

    void MoveSliding()
    {
        SlideTimer -= Time.fixedDeltaTime;
        if (SlideTimer <= 0f)
        {
            owner.Stats.CharacterState = ECharacterState.Idle;
            OwnerCollider.size = ColliderSize;
            IsSliding = false;
            owner.Animation.OnSliding(false);

            return;
        }

        float facingDir = Mathf.Sign(owner.transform.localScale.x);
        Rig2D.linearVelocity = new Vector2(facingDir * owner.Stats.SlideSpeed, Rig2D.linearVelocity.y);
    }

    // ����
    public void Jump()
    {
        if (owner.Stats.CharacterState == ECharacterState.Attacking) return;




        if (IsGrounded && !IsJumping && Rig2D)
        {
            // �Ʒ�����Ű�� ������ ����
            if (moveInput.y < 0f)
            {
                Vector2 point = owner.transform.position;
                point += Vector2.up * -1f;
                Collider2D hits = Physics2D.OverlapPoint(point);

                if(hits.tag != "EndGround")
                {
                    OwnerCollider.enabled = false; // �ݸ��� ��Ȱ��ȭ

                    DropDurationTime = DropDuration;
                    IsDropping = true;
                }
            }
            else // �Ϲ� ����
            {
                Rig2D.linearVelocity = new Vector2(Rig2D.linearVelocity.x, 0);
                Rig2D.AddForce(Vector2.up * owner.Stats.GetCharacterStats().JumpPower, ForceMode2D.Impulse);

                IsJumping = true;
                owner.Animation.OnJump();
            }
            
        }
    }

    void EndFalling()
    {
        IsJumping = false;
        owner.Animation.OnFalling(false);

        if (owner.Stats.CharacterState == ECharacterState.Falling)
            owner.Stats.CharacterState = ECharacterState.Idle;
    }

     
    // �̵�
    void Move()
    {
        if (Rig2D == null) return;

        if (moveInput == Vector2.zero)
        {
            Rig2D.linearVelocity = new Vector2(0, Rig2D.linearVelocity.y);
            owner.Animation.WalikingAnimator(0);
            return;
        }

        float speed = moveInput.x * owner.Stats.GetCharacterStats().Speed;
        if (owner.Stats.CharacterState == ECharacterState.Attacking)
            speed /= 2;


        Rig2D.linearVelocity = new Vector2(speed, Rig2D.linearVelocity.y);

        owner.Animation.WalikingAnimator(moveInput.x);
    }

    public void SetMove(Vector2 input)
    {
        if (Rig2D == null) return;

        moveInput = input;

    }

    
}
