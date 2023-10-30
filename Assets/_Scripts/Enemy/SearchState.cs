using UnityEngine;

public class SearchState : BaseState
{
    public SearchState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine) { }

    public override void EnterState()
    {
        Debug.Log("Enter Search State");
    }
    public override void UpdateState()
    {
        _enemy.EnemySearch();

        if (_enemy.DistanceToEnemy <= _enemy.DetectDistance && _enemy.target != null)
        {
            _stateMachine.SwitchState(_enemy.ChaseState);
        }
    }
    public override void ExitState()
    {
        Debug.Log("Exit Search State");
    }
}