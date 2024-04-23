public abstract class EnemyGroundState : EnemyState
{
    public EnemyGroundState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.MovementCompo.isGround.OnValueChanged += HandleGroundChanged;

        HandleGroundChanged(false, _enemy.MovementCompo.isGround.Value);
    }

    public override void Exit()
    {
        base.Exit();
        _enemy.MovementCompo.isGround.OnValueChanged -= HandleGroundChanged;
    }

    private void HandleGroundChanged(bool prev, bool now)
    {
        if (!now) //공중에 떴다면
        {
            _stateMachine.ChangeState(EnemyEnum.Air);
        }
    }

}
