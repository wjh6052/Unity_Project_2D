using UnityEngine.InputSystem;
using UnityEngine;

public class M_InputSystem : Module_Base
{
    public InputActionAsset PlayerInputSystem;

    public InputAction moveAction;

    public InputAction JumpAction;
    public InputAction SlidingAction;
    public InputAction AttackAction;



    private void OnEnable()
    {
        PlayerInputSystem = Resources.Load<InputActionAsset>("InputSystem/Player InputSystem");


        if (PlayerInputSystem == null) return;

            
        // 오타 주의: Plyaer
        var actionMap = PlayerInputSystem.FindActionMap("PlayerInput");
        moveAction = actionMap.FindAction("Move");
        JumpAction = actionMap.FindAction("Jump");
        SlidingAction = actionMap.FindAction("Sliding");
        AttackAction = actionMap.FindAction("Attack");


        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        moveAction.Enable();

        JumpAction.started += OnJump;
        JumpAction.Enable();

        SlidingAction.started += OnSliding;
        SlidingAction.Enable();

        AttackAction.started += OnAttack;
        AttackAction.Enable();

    }

    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        moveAction.Disable();


        JumpAction.started -= OnJump;
        JumpAction.Disable();


        SlidingAction.started -= OnSliding;
        SlidingAction.Disable();

    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();


        if(owner)
            owner.Movement.SetMove(input.x);
    }

   void OnJump(InputAction.CallbackContext context)
    {
        if (owner)
            owner.Movement.Jump();
    }

    void OnSliding(InputAction.CallbackContext context)
    {
        if (owner)
            owner.Movement.OnSliding();
    }

    void OnAttack(InputAction.CallbackContext context)
    {
        if (owner)
            owner.AttackSystem.OnAttack();
    }
}
