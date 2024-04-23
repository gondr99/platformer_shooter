using System.Collections.Generic;

public class EnemyStateMachine
{
    public EnemyState CurrentState { get; private set; }
    public Dictionary<EnemyEnum, EnemyState> StateDictionary = new Dictionary<EnemyEnum, EnemyState>();

    public Enemy _enemyBase;

    public void Initialize(EnemyEnum startState, Enemy enemy)
    {
        _enemyBase = enemy;
        CurrentState = StateDictionary[startState];
        CurrentState.Enter();
    }

    public void ChangeState(EnemyEnum newState, bool forceMode = false)
    {
        if (_enemyBase.canStateChangeable == false && forceMode == false) return;
        if (_enemyBase.isDead) return;

        CurrentState.Exit();
        CurrentState = StateDictionary[newState];
        CurrentState.Enter();
    }

    public void AddState(EnemyEnum stateEnum, EnemyState enemyState)
    {
        StateDictionary.Add(stateEnum, enemyState);
    }
}
