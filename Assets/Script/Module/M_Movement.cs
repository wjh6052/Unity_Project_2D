using UnityEngine;




public class M_Movement : Module_Base
{
    Rigidbody2D Rig2D;
    CapsuleCollider2D OwnerCollider;
    Vector2 ColliderSize;


    // Move
    Vector2 moveInput;


    //Jump
    public LayerMask GroundLayer;           // 캐릭터 하단에 위치한 빈 오브젝트
    public float groundCheckRadius = 0.2f;  // 바닥 체크 범위 (작은 원)
    float footOffset;

    bool IsGrounded = false;
    bool IsJumping = false;


    // 슬라이딩
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
        // 슬라이딩
        {
            if (IsSliding)
            {
                MoveSliding();
                return;
            }
        }


        // 점프 관련
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



        // 이동 관련
        {
            Move();
        }
        
    }

    // 슬라이딩
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

        // 슬라이딩 콜리전의 크기만큼 포지션을 맞추기
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

    // 점프
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

     
    // 이동
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
