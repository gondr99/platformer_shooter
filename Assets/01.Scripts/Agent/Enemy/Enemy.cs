using UnityEngine;

public abstract class Enemy : Agent
{
    [Header("Attack Settings")]
    public float detectRadius;
    public float attackRadius;
    public float attackCooldown;

    [HideInInspector] public float lastAttackTime;

    private int _enemyLayer;
    public bool canStateChangeable = true;

    protected override void Awake()
    {
        base.Awake();

        _enemyLayer = LayerMask.NameToLayer("Enemy");
    }
}
