using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Mgr : MonoBehaviour
{
    [Header("플레이어 오브젝트")]
    public Character_Player PlayerObject;


    [Header("월드 캔버스")]
    public Transform World_Canvas = null;


    [Header("데미지 텍스트")]
    public GameObject DmgTxetRoot = null;


    [Header("상호작용UI")]
    public GameObject InteractionUIRoot = null;
    



    [Header("이동할 맵")]
    public string MapName;

    //--- 싱글턴 패턴
    public static Game_Mgr Inst = null;


    private void Awake()
    {
        Inst = this;
        GlobalValue.InitData();
    }

    void Start()
    {
        // 프레임 고정
        Application.targetFrameRate = 60;

        // 테스트
        ChangeMap(MapName);
    }


    public void ChangeMap(string ChangeMapName)
    {
        if (ChangeMapName == "") return;

        if (SceneManager.GetSceneByName(MapName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(MapName);
        }

        MapName = ChangeMapName;

        SceneManager.LoadScene(ChangeMapName, LoadSceneMode.Additive);
    }

    // 데미지 텍스트 스폰
    public void SpawnDamageText(float InDamage, Vector3 InPos, EDamageType InEDamageType)
    {
        if (World_Canvas == null || DmgTxetRoot == null)
            return;
 
        GameObject dmgtxt = Instantiate(DmgTxetRoot);
        dmgtxt.transform.SetParent(World_Canvas);
        DmgTxet_Ctrl dmgtext_C = dmgtxt.GetComponent<DmgTxet_Ctrl>();
        if (dmgtext_C)
            dmgtext_C.InitDamage(InDamage, InEDamageType);

        InPos.x += Random.Range(-0.2f, 0.2f);
        InPos.y += 1.1f + Random.Range(-0.1f, 0.1f);
        InPos.z = 0;

        dmgtxt.transform.position = InPos;
    }


    // 상호작용 UI 스폰
    

    public void SetCameraCollider(PolygonCollider2D InPolygonCollider2D)
    {
        if (!InPolygonCollider2D) return;

        PlayerObject.Confiner.BoundingShape2D = InPolygonCollider2D;


    }
}
