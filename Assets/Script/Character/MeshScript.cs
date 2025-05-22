using UnityEngine;

public class MeshScript : MonoBehaviour
{
    Character_Base CharacterRoot;

    void Start()
    {
        Transform parentTransform = transform.parent;
        CharacterRoot = transform.parent.gameObject.GetComponent<Character_Base>();
    }

    public void EndAttack(float AttackDelay)
    {
        CharacterRoot.AttackSystem.EndAttack(AttackDelay);
    }

    public void AttackHitCheck(float range)
    {
        CharacterRoot.AttackSystem.AttackHitCheck(range);
    }

    public void EndHit()
    {
        CharacterRoot.Damage.EndHit();
    }

    // 죽었을 때 호출
    public void Dead(float DestroyTime)
    {
        if(CharacterRoot.CharacterType != ECharacterType.Player) // 플레이어가 아닐때 빨려들어가는 연출
        {
            StartCoroutine(CharacterRoot.Damage.Dead(DestroyTime));
        }

    }
}
