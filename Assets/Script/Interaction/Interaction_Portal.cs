using UnityEngine;

public class Interaction_Portal : Interaction_Base
{
    [Header("�̵��� ���� �̸�")]
    public string SpawnMapName;

    [Header("������ ����")]
    public Vector3 SpawnPoint;


    public override void UesInteraction()
    {
        base.UesInteraction();

        Game_Mgr.Inst.ChangeMap(SpawnMapName);

        Game_Mgr.Inst.PlayerObject.transform.position = SpawnPoint;
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
