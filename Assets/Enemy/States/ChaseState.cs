using Assets.Shared;
using UnityEngine;

namespace Assets.Enemy.States
{
    /// <summary>
    /// Represents the chase state of an enemy.
    /// </summary>
    /// 
    public class ChaseState : BaseState<Enemy>
    {
        public void EnterState(Enemy owner)
        {
            owner.Animator.SetTrigger("ChaseState");
        }

        public void ExitState(Enemy owner)
        {
            
        }

        public void UpdateState(Enemy owner)
        {
            if (owner.TargetEnemy() != null)
            {
                owner.NavMeshAgent.SetDestination(owner.TargetEnemy().position);
                if(Vector3.Distance(owner.transform.position, owner.TargetEnemy().position) > owner.ChaseDistance)
                {
                    owner.SwitchState(owner.PatrolState);
                }
            }
        }
    }
}
