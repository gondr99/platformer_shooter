using UnityEngine;

public enum EnemyEnum
{
    Air,
    Idle,
    Chase,
    Attack,
    Dead
}

public class ZombieEnemy : Enemy, IPoolable
{
    public EnemyStateMachine stateMachine;

    public string ItemName => "ToastZombie";

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new EnemyStateMachine();

        stateMachine.AddState(EnemyEnum.Idle, new EnemyIdleState(this, stateMachine, "Idle"));
        stateMachine.AddState(EnemyEnum.Chase, new EnemyChaseState(this, stateMachine, "Chase"));
        stateMachine.AddState(EnemyEnum.Air, new EnemyAirState(this, stateMachine, "Air"));
        stateMachine.AddState(EnemyEnum.Attack, new EnemyAttackState(this, stateMachine, "Attack"));
        stateMachine.AddState(EnemyEnum.Dead, new EnemyDeadState(this, stateMachine, "Dead"));


        stateMachine.Initialize(EnemyEnum.Idle, this);
    }

    private void Update()
    {
        stateMachine.CurrentState.UpdateState();

        if (targetTrm != null && isDead == false)
            HandleSpriteFlip(targetTrm.position);
    }

    public override void AnimationEndTrigger()
    {
        stateMachine.CurrentState.AnimationEndTrigger();
    }

    public override void SetDead()
    {
        stateMachine.ChangeState(EnemyEnum.Dead);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void ResetItem()
    {
        canStateChangeable = true;
        isDead = false;
        targetTrm = null;
        HealthCompo.ResetHealth();
        stateMachine.ChangeState(EnemyEnum.Idle);
        gameObject.layer = _enemyLayer;
    }
}
