using System.Collections.Generic;
using Assets.Enemy.States;
using Assets.Shared;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    /// <summary>
    /// Represents an enemy entity in the game.
    /// </summary>
    public class Enemy : MonoBehaviour
    {
#region States
        private BaseState<Enemy> _currentState;
        public PatrolState PatrolState = new();
        public ChaseState ChaseState = new();
        public RetreatState RetreatState = new();
#endregion
        [SerializeField]
        private List<Transform> _waypoints = new();
        [SerializeField]
        private Transform _targetEnemy;
        [SerializeField]
        private float _chaseDistance;
#region Properties
        public Transform TargetEnemy() => _targetEnemy;
        public float ChaseDistance => _chaseDistance;
        public List<Transform> Waypoints => _waypoints;
        [HideInInspector]
        public NavMeshAgent NavMeshAgent;
        [HideInInspector]
        public Animator Animator;
        #endregion
        private void Awake()
        {
            Animator = GetComponent<Animator>();
            NavMeshAgent = GetComponent<NavMeshAgent>();
            _currentState = PatrolState;
            _currentState?.EnterState(this);
        }

        private void Start()
        {
            PlayerController player = _targetEnemy.GetComponent<PlayerController>();
            if (player != null)
            {
                player.OnPowerUpStart += StartRetreating;
                player.OnPowerUpStop += StopRetreating;
            }
        }

        private void StartRetreating()
        {
            SwitchState(RetreatState);
        }

        private void StopRetreating()
        {
            SwitchState(PatrolState);
        }

        public void SwitchState(BaseState<Enemy> newState)
        {
            _currentState?.ExitState(this);
            _currentState = newState;
            _currentState?.EnterState(this);
        }

        // Update is called once per frame
        private void Update()
        {
            _currentState?.UpdateState(this);
        }
    }
}
