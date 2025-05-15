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

    float SlideTimer = 0f;


    void Start()
    {
        Rig2D = owner.GetComponent<Rigidbody2D>();

        OwnerCollider = owner.GetComponent<CapsuleCollider2D>();
        ColliderSize = OwnerCollider.size;

        GroundLayer = LayerMask.GetMask("Ground");

        footOffset = GetComponent<SpriteRenderer>().bounds.extents.y;
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

        // �����̵� �ݸ����� ũ�⸸ŭ �������� ���߱�
        //Vector2 size = ColliderSize;
        //size.y = 0.2f;
        //OwnerCollider.size = size;
        //owner.transform.position -= new Vector3(0, (ColliderSize.y - size.y) / 2, 0);

        SlideTimer = owner.Stats.SlideDirection;
        IsSliding = true;
    }

    void MoveSliding()
    {
        owner.Animation.OnSliding(true);
        owner.Stats.CharacterState = ECharacterState.Sliding;
        float facingDir = -Mathf.Sign(owner.transform.localScale.x);
        Rig2D.linearVelocity = new Vector2(facingDir * owner.Stats.SlideSpeed, Rig2D.linearVelocity.y);


        SlideTimer -= Time.fixedDeltaTime;
        if (SlideTimer <= 0f)
        {
            owner.Stats.CharacterState = ECharacterState.Idle;
            OwnerCollider.size = ColliderSize;
            IsSliding = false;
            owner.Animation.OnSliding(false);
        }
    }

    // ����
    public void Jump()
    {
        if (IsGrounded && !IsJumping && Rig2D)
        {
            Rig2D.linearVelocity = new Vector2(Rig2D.linearVelocity.x, 0);
            Rig2D.AddForce(Vector2.up * owner.Stats.JumpPower, ForceMode2D.Impulse);

            IsJumping = true;
            owner.Animation.OnJump();
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

        if (owner.Stats.CharacterState != ECharacterState.Attacking)
            Rig2D.linearVelocity = new Vector2(moveInput.x * owner.Stats.Speed, Rig2D.linearVelocity.y);

        owner.Animation.WalikingAnimator(moveInput.x);
    }

    public void SetMove(float input)
    {
        if (Rig2D == null) return;

        moveInput = new Vector2(input, 0);

    }

    
}
