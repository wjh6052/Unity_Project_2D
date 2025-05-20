using UnityEngine;

public class FollowCamera : MonoBehaviour
{


    void Start()
    {
        
    }

    void Update()
    {
        if (Game_Mgr.Inst.PlayerObject)
        {
            Vector3 pos = Game_Mgr.Inst.PlayerObject.transform.position;
            pos.z = this.transform.position.z;
            pos += Vector3.up * 2;
            this.transform.position = pos;
        }
    }
}
