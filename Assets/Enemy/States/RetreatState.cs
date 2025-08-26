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
            
        }

        public void ExitState(Enemy owner)
        {
            if(owner.TargetEnemy() != null)
            {
                owner.NavMeshAgent.SetDestination(owner.transform.position - owner.TargetEnemy().position);
            }
        }

        public void UpdateState(Enemy owner)
        {
            
        }
    }
}
