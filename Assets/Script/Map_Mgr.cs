using UnityEngine;

public class Map_Mgr : MonoBehaviour
{
    PolygonCollider2D Polygon2D;

    private void Awake()
    {
        Game_Mgr.Inst.SetCameraCollider(GetComponent<PolygonCollider2D>());
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
