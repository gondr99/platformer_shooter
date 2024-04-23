using UnityEngine;

public class EnemyChaseState : EnemyGroundState
{
    public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Vector2 dir = _enemy.targetTrm.position - _enemy.transform.position;
        _enemy.MovementCompo.SetMovement(Mathf.Sign(dir.x));

        float distance = dir.magnitude;
        if (distance < _enemy.attackRadius &&
          _enemy.lastAttackTime + _enemy.attackCooldown < Time.time)
        {
            _stateMachine.ChangeState(EnemyEnum.Attack);
            return;
        }
    }
}
