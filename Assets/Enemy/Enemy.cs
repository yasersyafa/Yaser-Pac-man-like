using Assets.Enemy.States;
using Assets.Shared;
using UnityEngine;

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
        private void Awake()
        {
            _currentState = PatrolState;
            _currentState?.EnterState(this);
        }

        // Update is called once per frame
        private void Update()
        {
            _currentState?.UpdateState(this);
        }
    }
}
