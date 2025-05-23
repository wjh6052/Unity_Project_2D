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
    public bool GetIsSliding() { return IsSliding; }

    float SlideTimer = 0f;


    // 밑 점프
    bool IsDropping = false;

    float DropDuration = 0.5f;      // 콜리전을 종료할 시간
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
        // 슬라이딩
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

            // 밑 점프
            if(IsDropping)
            {
                DropDurationTime -= Time.deltaTime;

                if(DropDurationTime <= 0)
                {
                    IsDropping = false;
                    OwnerCollider.enabled = true; // 콜리전 활성화
                }
                
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

        SlideTimer = owner.Stats.SlideDirection;
        IsSliding = true;

        // 상태 변경
        owner.Stats.CharacterState = ECharacterState.Sliding;

        // 애니메이션 실행
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

    // 점프
    public void Jump()
    {
        if (owner.Stats.CharacterState == ECharacterState.Attacking) return;




        if (IsGrounded && !IsJumping && Rig2D)
        {
            // 아래방향키를 누르며 점프
            if (moveInput.y < 0f)
            {
                Vector2 point = owner.transform.position;
                point += Vector2.up * -1f;
                Collider2D hits = Physics2D.OverlapPoint(point);

                if(hits.tag != "EndGround")
                {
                    OwnerCollider.enabled = false; // 콜리전 비활성화

                    DropDurationTime = DropDuration;
                    IsDropping = true;
                }
            }
            else // 일반 점프
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
