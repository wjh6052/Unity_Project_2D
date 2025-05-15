using UnityEngine;

public class Character_Player : Character_Base
{
    M_InputSystem InputSystem;


    protected override void Awake()
    {
        base.Awake();

        CharacterType = ECharacterType.Player;

        InputSystem = gameObject.AddComponent<M_InputSystem>();
        InputSystem.SetOwner(this);
    }


    protected override void Start()
    {
        
    }


    protected override void Update()
    {
        
    }
}
