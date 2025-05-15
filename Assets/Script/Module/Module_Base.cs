using UnityEngine;

public class Module_Base : MonoBehaviour
{
    protected Character_Base owner;

    public void SetOwner(Character_Base newOwner)
    {
        owner = newOwner;
    }
}
