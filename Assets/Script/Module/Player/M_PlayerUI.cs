using System.Collections.Generic;
using UnityEngine;

public class M_PlayerUI : Module_Base
{
    Interaction_Base IDict = null;
    GameObject InteractionUIObject = null;


    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void UesInteraction()
    {
        if (!IDict) return;

        IDict.UesInteraction();
    }

    public void SpawnInteractionUI(Interaction_Base InInteraction)
    {
        if (Game_Mgr.Inst.World_Canvas == null || Game_Mgr.Inst.InteractionUIRoot == null)
            return;

        GameObject interactionUIR = Instantiate(Game_Mgr.Inst.InteractionUIRoot);
        interactionUIR.transform.SetParent(Game_Mgr.Inst.World_Canvas);

        interactionUIR.transform.position = InInteraction.transform.position;

        InteractionUIObject = interactionUIR;
        IDict = InInteraction;
    }

    public void DespawnInteractionUI(Interaction_Base InInteraction)
    {
        if (!IDict) return;

        Destroy(InteractionUIObject);
        InteractionUIObject = null;
        IDict = null;

    }
}
