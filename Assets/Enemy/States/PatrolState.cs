using Assets.Shared;
using UnityEngine;

namespace Assets.Enemy.States
{
    /// <summary>
    /// Represents the patrol state of an enemy.
    /// </summary>
    public class PatrolState : BaseState<Enemy>
    {
        private bool _isMoving = false;
        private Vector3 _destination;
        public void EnterState(Enemy owner)
        {
            _isMoving = false;
            owner.Animator.SetTrigger("PatrolState");
        }

        public void ExitState(Enemy owner)
        {
            Debug.Log("Exiting Patrol State");
        }

        public void UpdateState(Enemy owner)
        {
            if(Vector3.Distance(owner.transform.position, owner.TargetEnemy().position) < owner.ChaseDistance)
            {
                owner.SwitchState(owner.ChaseState);
                return;
            }
            if (!_isMoving)
            {
                _isMoving = true;
                int index = Random.Range(0, owner.Waypoints.Count);
                _destination = owner.Waypoints[index].position;
                owner.NavMeshAgent.destination = _destination;
            }
            else
            {
                if (Vector3.Distance(_destination, owner.transform.position) <= 0.1f)
                {
                    _isMoving = false;
                }
            }
        }
    }
}
