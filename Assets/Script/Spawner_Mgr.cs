using UnityEngine;

public class Spawner_Mgr : MonoBehaviour
{
    [Header("����")]
    public GameObject MonsterRoot;


    [Header("���� ����")]
    public Transform[] SpawnPoirnt;

    public float SpawnTimer = 2.0f;
    public float SpawnCurTimer = 0.0f;

    void Start()
    {
        
    }


    void Update()
    {
        SpawnCurTimer -= Time.deltaTime;

        if(SpawnCurTimer <= 0)
        {
            SpawnCurTimer = SpawnTimer;
            SpawnMonster();
        }





    }

    void SpawnMonster()
    {
        GameObject monster =  Instantiate(MonsterRoot);
        monster.transform.position = SpawnPoirnt[Random.Range(0, SpawnPoirnt.Length)].position;

    }
}
