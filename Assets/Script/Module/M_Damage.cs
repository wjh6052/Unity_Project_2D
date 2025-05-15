using UnityEngine;

public enum EDamageType
{
    Normal,
    Critical,
    Hill
}

public class M_Damage : Module_Base
{



    void Start()
    {
        
    }


    public void TakeDamage(float InDamage)
    {
        Game_Mgr.Inst.SpawnDamageText(InDamage, owner.transform.position);
    }

}
