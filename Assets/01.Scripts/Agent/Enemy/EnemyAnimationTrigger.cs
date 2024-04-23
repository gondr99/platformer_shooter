using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = transform.parent.GetComponent<Enemy>();
    }

    private void AnimationEnd()
    {
        _enemy.AnimationEndTrigger();
    }

    private void AnimationAttack()
    {
        _enemy.Attack();
    }
}
