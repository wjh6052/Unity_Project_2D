using UnityEngine;

public class Interaction_Base : MonoBehaviour
{

    
    public virtual void UesInteraction()
    {
        Debug.Log(this.gameObject.name);
    }

    // 플레이어와 충돌했을때
    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Game_Mgr.Inst.PlayerObject.PlayerUI.SpawnInteractionUI(this);
        }
    }

    // 플레이어와 충돌이 끝났을때
    protected virtual void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Game_Mgr.Inst.PlayerObject.PlayerUI.DespawnInteractionUI(this);
        }
    }
}
