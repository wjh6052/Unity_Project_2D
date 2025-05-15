using UnityEngine;

public class Character_Base : MonoBehaviour
{
    [HideInInspector]
    public M_Movement Movement;

    [HideInInspector]
    public M_Stats Stats;

    [HideInInspector]
    public M_Animation Animation;


    protected virtual void Awake()
    {
        Movement = gameObject.AddComponent<M_Movement>();
        Movement.SetOwner(this);

        Stats = gameObject.AddComponent<M_Stats>();
        Stats.SetOwner(this);

        Animation = gameObject.AddComponent<M_Animation>();
        Animation.SetOwner(this);
    }


    protected virtual void Start()
    {
        
    }


    protected virtual void Update()
    {
        
    }
}
