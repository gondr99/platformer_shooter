using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float _attackJumpPower = 3f;
    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.MovementCompo.StopImmediately();

        Vector2 dir = _enemy.targetTrm.position - _enemy.transform.position;
        dir.y = _attackJumpPower;
        dir.x *= 0.5f;
        _enemy.MovementCompo.JumpTo(dir);
    }

    public override void Exit()
    {
        base.Exit();
        _enemy.lastAttackTime = Time.time;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_endTriggerCalled)
        {
            _stateMachine.ChangeState(EnemyEnum.Chase);
        }
    }
}
