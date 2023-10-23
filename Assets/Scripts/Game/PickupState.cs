using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedRPG
{
    public class PickupState<T> : State<T>
    {
        private readonly InteractionSystem _interactionSystem;
        private readonly InteractPanelLayout _interactPanelLayout;

        private readonly InputSystem _inputSystem;

        private readonly Summoner _summoner;

        private Ability _ability;

        private int _index;

        public PickupState(T uniqueID, InteractionSystem interactionSystem) : base(uniqueID)
        {
            _interactionSystem = interactionSystem;
            _interactPanelLayout = _interactionSystem.InteractPanelLayout;

            _summoner = _interactionSystem.Summoner;

            _inputSystem = App.GetSystem<InputSystem>();
        }

        public override void OnEnter()
        {
            _inputSystem.SetActionMapEnabled(GameState.Interact.ToString());

            Bind();
        }

        private void Bind()
        {
            var interactable = _interactionSystem.Interactable;

            if (interactable.CanInteract())
            {
                _ability = interactable.Interact() as Ability;

                _summoner.AddAbilityToAttributeSet(_ability);
                _interactPanelLayout.Show();
            }
            else
            {
                ChangeStateToExplore();
            }

            _index = 0;
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DisplayInteractionText();
            }
        }

        private void DisplayInteractionText()
        {
            if (!_interactPanelLayout.Bind(_ability, _index))
            {
                _interactionSystem.ChangeState(GameState.Explore);
            }

            _index++;
        }

        private void ChangeStateToExplore()
        {
            _interactionSystem.ChangeState(GameState.Explore);
        }

        public override void OnExit()
        {
            _inputSystem.SetActionMapDisabled(GameState.Interact.ToString());
        }
    }
}
