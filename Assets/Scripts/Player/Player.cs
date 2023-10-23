using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedRPG
{
    public enum PlayerState
    {
        Idle,
        Movement
    }

    public class Player : EntityComponent, IInput
    {
        public Vector2 Input { get; set; }
        public Vector2 Direction { get; set; }

        private RetroAnimator _retroAnimator;
        private StateMachine<PlayerState> _stateMachine;

        private PlayerState _currentState;

        private readonly float _radius = 1f;

        private void Awake()
        {
            _retroAnimator = GetComponent<RetroAnimator>();

            Create();
        }

        private void Create()
        {
            _currentState = PlayerState.Idle;

            var stateList = new List<IState<PlayerState>>()
            {
                new PlayerIdleState<PlayerState>(PlayerState.Idle, this),
                new PlayerMovementState<PlayerState>(PlayerState.Movement, this)
            };

            _stateMachine = new StateMachine<PlayerState>(stateList.ToArray(), _currentState);
            _stateMachine.OnEnter();
        }

        private void Update()
        {
            _stateMachine.OnUpdate();

            var direction = GetDirectionFromVector(Direction);

            _retroAnimator.Set((int)_currentState);
            _retroAnimator.Set(direction);
        }

        public void ChangeState(PlayerState state)
        {
            _stateMachine.ChangeState(state);
            _currentState = state;
        }

        public void Interact()
        {
            var hit = Physics2D.OverlapCircle(transform.position, _radius, 7 << 8);

            if (hit && hit.TryGetComponent(out Interactable component))
            {
                Interactable.OnInteract.Raise(component);
            }
        }

        private Direction GetDirectionFromVector(Vector2 value)
        {
            float angle = Mathf.Atan2(value.x, value.y);
            int index = Mathf.RoundToInt(4 * angle / (2 * Mathf.PI) + 4) % 4;

            return (Direction)index;
        }
    }
}
