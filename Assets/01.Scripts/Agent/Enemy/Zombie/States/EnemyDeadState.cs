using UnityEngine;

public class EnemyDeadState : EnemyState
{
    private readonly int _deadLayer = LayerMask.NameToLayer("DeadBody");
    private bool _onExplosion = false;

    public EnemyDeadState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.gameObject.layer = _deadLayer;
        _enemy.MovementCompo.StopImmediately();
        _enemy.isDead = true;
        _onExplosion = false;
        _enemy.canStateChangeable = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_endTriggerCalled && !_onExplosion)
        {
            _onExplosion = true;
            PlayExplosion();
        }
    }

    private void PlayExplosion()
    {
        var effect = PoolManager.Instance.Pop("ZombieExplosion") as EffectPlayer;
        effect.SetPositionAndPlay(_enemy.transform.position);

        IPoolable iPooalbe = _enemy.GetComponent<IPoolable>();
        if(iPooalbe != null)
        {
            PoolManager.Instance.Push(iPooalbe);
        }
        else
        {
            GameObject.Destroy(_enemy.gameObject);
        }
        
    }
}
