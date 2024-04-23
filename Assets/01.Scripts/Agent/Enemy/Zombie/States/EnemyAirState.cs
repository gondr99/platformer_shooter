public class EnemyAirState : EnemyState
{
    public EnemyAirState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_enemy.MovementCompo.isGround.Value)
        {
            _stateMachine.ChangeState(EnemyEnum.Idle);
        }
    }
}
