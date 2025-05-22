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

    // �׾��� �� ȣ��
    public void Dead(float DestroyTime)
    {
        if(CharacterRoot.CharacterType != ECharacterType.Player) // �÷��̾ �ƴҶ� �������� ����
        {
            StartCoroutine(CharacterRoot.Damage.Dead(DestroyTime));
        }

    }
}
