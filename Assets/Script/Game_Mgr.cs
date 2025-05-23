using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Mgr : MonoBehaviour
{
    [Header("�÷��̾� ������Ʈ")]
    public Character_Player PlayerObject;


    [Header("���� ĵ����")]
    public Transform World_Canvas = null;


    [Header("������ �ؽ�Ʈ")]
    public GameObject DmgTxetRoot = null;


    [Header("��ȣ�ۿ�UI")]
    public GameObject InteractionUIRoot = null;
    



    [Header("�̵��� ��")]
    public string MapName;

    //--- �̱��� ����
    public static Game_Mgr Inst = null;


    private void Awake()
    {
        Inst = this;
        GlobalValue.InitData();
    }

    void Start()
    {
        // ������ ����
        Application.targetFrameRate = 60;

        // �׽�Ʈ
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

    // ������ �ؽ�Ʈ ����
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


    // ��ȣ�ۿ� UI ����
    

    public void SetCameraCollider(PolygonCollider2D InPolygonCollider2D)
    {
        if (!InPolygonCollider2D) return;

        PlayerObject.Confiner.BoundingShape2D = InPolygonCollider2D;


    }
}
