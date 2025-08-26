using System.Collections.Generic;
using Assets.Enemy.States;
using Assets.Shared;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Enemy
{
    /// <summary>
    /// Represents an enemy entity in the game.
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        private BaseState<Enemy> _currentState;
        public PatrolState PatrolState = new();
        public ChaseState ChaseState = new();
        public RetreatState RetreatState = new();
        [SerializeField]
        private List<Transform> _waypoints = new();

        public List<Transform> Waypoints => _waypoints;
        [HideInInspector]
        public NavMeshAgent NavMeshAgent;
        private void Awake()
        {
            _currentState = PatrolState;
            _currentState?.EnterState(this);
            NavMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        private void Update()
        {
            _currentState?.UpdateState(this);
        }
    }
}
