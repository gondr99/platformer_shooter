using UnityEngine;

public class EnemyIdleState : EnemyGroundState
{
    private float _delayTime = 0.3f; //최초 포탈에서 생성되고 나서 잠깐 대기후 추적
    private bool _readyToChase = false;

    private Coroutine _delayCoroutine = null;
    
    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (_readyToChase) return;
        _delayCoroutine = _enemy.DelayCallback(_delayTime, () => _readyToChase = true);
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

        if (!_readyToChase) return;

        Collider2D player = _enemy.GetPlayerInRange();
        if (player != null)
        {
            _enemy.targetTrm = player.transform;
            _stateMachine.ChangeState(EnemyEnum.Chase);
        }

    }
}
