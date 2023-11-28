using UnityEngine;

public class SearchState : BaseState
{
    public SearchState(Enemy enemy, StateMachine stateMachine, EnemyTracker enemyTracker) : base(enemy, stateMachine, enemyTracker) { }

    public override void EnterState()
    {
        Debug.Log("Enter Search State");
    }
    public override void UpdateState()
    {
        _enemy.EnemySearch();

        if (_enemyTracker.DistanceToEnemy <= _enemyTracker.DetectDistance)
        {
            _stateMachine.SwitchState(_enemy.ChaseState);
        }
    }
    public override void ExitState()
    {

    }
}