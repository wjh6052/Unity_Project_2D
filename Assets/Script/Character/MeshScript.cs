using UnityEngine;

public class MeshScript : MonoBehaviour
{
    Character_Base CharacterRoot;

    void Start()
    {
        Transform parentTransform = transform.parent;
        CharacterRoot = transform.parent.gameObject.GetComponent<Character_Base>();
    }

    public void EndAttack()
    {
        CharacterRoot.AttackSystem.EndAttack();
    }

    public void AttackHitCheck(float range)
    {
        CharacterRoot.AttackSystem.AttackHitCheck(range);
    }

    public void EndHit()
    {
        CharacterRoot.Damage.EndHit();
    }
}
