using UnityEngine;

public class Interaction_Portal : Interaction_Base
{
    [Header("이동할 맵의 이름")]
    public string SpawnMapName;

    [Header("스폰할 지점")]
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
