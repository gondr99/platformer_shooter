using System;
using System.Collections;
using UnityEngine;

public abstract class Enemy : Agent
{
    [Header("Attack Settings")]
    public float detectRadius;
    public float attackRadius;
    public float attackCooldown;
    public float knockBackPower;
    public int attackDamage;

    [HideInInspector] public float lastAttackTime;

    protected int _enemyLayer;
    public bool canStateChangeable = true;

    [HideInInspector] public Transform targetTrm = null;
    public LayerMask whatIsPlayer;
    private Collider2D[] _colliders;

    public DamageCaster DamageCasterCompo { get; protected set; }

    protected override void Awake()
    {
        base.Awake();

        DamageCasterCompo = transform.Find("DamageCaster").GetComponent<DamageCaster>();
        _colliders = new Collider2D[1];
        _enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    public Collider2D GetPlayerInRange()
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(whatIsPlayer);
        int cnt = Physics2D.OverlapCircle(transform.position, detectRadius, filter, _colliders);

        return cnt > 0 ? _colliders[0] : null;
    }

    #region DelayCallback Routine
    public Coroutine DelayCallback(float time, Action Callback)
    {
        return StartCoroutine(DelayCallbackCoroutine(time, Callback));
    }

    protected IEnumerator DelayCallbackCoroutine(float time, Action Callback)
    {
        yield return new WaitForSeconds(time);
        Callback?.Invoke();
    }
    #endregion

    public abstract void AnimationEndTrigger();
    
    public virtual void Attack()
    {
        DamageCasterCompo.CastDamage(attackDamage, knockBackPower);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = Color.white;
    }
#endif
}
