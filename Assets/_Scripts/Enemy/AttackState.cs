using UnityEngine;

public class AttackState : BaseState
{
    public AttackState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine) {}
    
    public override void EnterState()
    {
        Debug.Log("Enter Attack State");
    }
    public override void UpdateState()
    {
        if(_enemy.IsBlocked == true || _enemy.DistanceToEnemy > _enemy.AttackDistance)
        {
            _stateMachine.SwitchState(_enemy.ChaseState);
        }
        else
        {
            _enemy.Attack();
        }
    }
    public override void ExitState()
    {
        Debug.Log("Exit Attack State");
    }
}