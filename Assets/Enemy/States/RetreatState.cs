using Assets.Shared;

namespace Assets.Enemy.States
{
    /// <summary>
    /// Represents the retreat state of an enemy.
    /// </summary>
    public class RetreatState : BaseState<Enemy>
    {
        public void EnterState(Enemy owner)
        {
            owner.Animator.SetTrigger("RetreatState");
        }

        public void ExitState(Enemy owner)
        {

        }

        public void UpdateState(Enemy owner)
        {
            if(owner.TargetEnemy() != null)
            {
                owner.NavMeshAgent.SetDestination(owner.transform.position - owner.TargetEnemy().position);
            }
        }
    }
}
