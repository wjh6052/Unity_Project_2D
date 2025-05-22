using UnityEngine;

public class Interaction_Base : MonoBehaviour
{

    
    public virtual void UesInteraction()
    {
        Debug.Log(this.gameObject.name);
    }

    // �÷��̾�� �浹������
    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Game_Mgr.Inst.PlayerObject.PlayerUI.SpawnInteractionUI(this);
        }
    }

    // �÷��̾�� �浹�� ��������
    protected virtual void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Game_Mgr.Inst.PlayerObject.PlayerUI.DespawnInteractionUI(this);
        }
    }
}
