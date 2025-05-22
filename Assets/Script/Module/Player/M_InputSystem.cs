using UnityEngine.InputSystem;
using UnityEngine;

public class M_InputSystem : Module_Base
{
    public InputActionAsset PlayerInputSystem;

    public InputAction moveAction;

    public InputAction JumpAction;
    public InputAction SlidingAction;
    public InputAction AttackAction;
    public InputAction InteractionAction;



    private void OnEnable()
    {
        PlayerInputSystem = Resources.Load<InputActionAsset>("InputSystem/Player InputSystem");


        if (PlayerInputSystem == null) return;

            
        // 오타 주의: Plyaer
        var actionMap = PlayerInputSystem.FindActionMap("PlayerInput");
        moveAction = actionMap.FindAction("Move");
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        moveAction.Enable();


        JumpAction = actionMap.FindAction("Jump");
        JumpAction.started += OnJump;
        JumpAction.Enable();


        SlidingAction = actionMap.FindAction("Sliding");
        SlidingAction.started += OnSliding;
        SlidingAction.Enable();


        AttackAction = actionMap.FindAction("Attack");
        AttackAction.started += OnAttack;
        AttackAction.Enable();

        InteractionAction = actionMap.FindAction("Interaction");
        InteractionAction.started += OnInteraction;
        InteractionAction.Enable();



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


        InteractionAction.started -= OnInteraction;
        InteractionAction.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        
        if(owner)
            owner.Movement.SetMove(input);
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

    void OnInteraction(InputAction.CallbackContext context)
    {
        if (owner)
        {
            Character_Player player = (Character_Player)owner;
            player.PlayerUI.UesInteraction();
        }
        
    }
}
