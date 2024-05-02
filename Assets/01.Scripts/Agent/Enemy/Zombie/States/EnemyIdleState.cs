using UnityEngine;

public class EnemyIdleState : EnemyGroundState
{

    private Coroutine _delayCoroutine = null;
    
    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.MovementCompo.StopImmediately(false);
    }

    public override void Exit()
    {
        base.Exit();
        if (_delayCoroutine != null)
            _enemy.StopCoroutine(_delayCoroutine);

    }

    public override void UpdateState()
    {
        base.UpdateState();

        Collider2D player = _enemy.GetPlayerInRange();
        if (player != null)
        {
            _enemy.targetTrm = player.transform;
            _stateMachine.ChangeState(EnemyEnum.Chase);
        }

    }
}
