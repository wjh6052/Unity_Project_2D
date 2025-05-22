using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Game_Mgr : MonoBehaviour
{

    // 데미지 텍스트
    public Transform Damage_Canvas = null; // 캔버스
    public GameObject DmgTxetRoot = null;  // 데미지 텍스트

    // 플레이어
    public Character_Player PlayerObject;


    //--- 싱글턴 패턴
    public static Game_Mgr Inst = null;


    private void Awake()
    {
        Inst = this;
        GlobalValue.InitData();
    }

    void Start()
    {
        // 테스트
        SceneManager.LoadScene("DemoScene", LoadSceneMode.Additive);
    }

    // 데미지 텍스트 스폰
    public void SpawnDamageText(float InDamage, Vector3 InPos, EDamageType InEDamageType)
    {
        if (Damage_Canvas == null || DmgTxetRoot == null)
            return;
 
        //Debug.Log(InDamage);

        GameObject dmgtxt = Instantiate(DmgTxetRoot);
        dmgtxt.transform.SetParent(Damage_Canvas);
        DmgTxet_Ctrl dmgtext_C = dmgtxt.GetComponent<DmgTxet_Ctrl>();
        if (dmgtext_C)
            dmgtext_C.InitDamage(InDamage, InEDamageType);

        InPos.x += Random.Range(-0.2f, 0.2f);
        InPos.y += 1.1f + Random.Range(-0.1f, 0.1f);
        InPos.z = 0;

        dmgtxt.transform.position = InPos;
    }


    public void SetCameraCollider(PolygonCollider2D InPolygonCollider2D)
    {
        if (!InPolygonCollider2D) return;

        PlayerObject.Confiner.BoundingShape2D = InPolygonCollider2D;


    }
}
