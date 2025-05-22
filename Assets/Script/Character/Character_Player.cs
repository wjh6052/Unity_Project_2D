using UnityEngine;
using Unity.Cinemachine;


public class Character_Player : Character_Base
{
    M_InputSystem InputSystem;
    public M_PlayerUI PlayerUI;

    public CinemachineConfiner2D Confiner;


    protected override void Awake()
    {
        base.Awake();

        CharacterType = ECharacterType.Player;

        InputSystem = gameObject.AddComponent<M_InputSystem>();
        InputSystem.SetOwner(this);

        PlayerUI = gameObject.AddComponent<M_PlayerUI>();
        PlayerUI.SetOwner(this);
    }


    protected override void Start()
    {
        
    }


    protected override void Update()
    {
    }
}
