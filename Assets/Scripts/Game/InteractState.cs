using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedRPG
{
    public class InteractState<T> : State<T>
    {
        private readonly InteractionSystem _interactionSystem;
        private readonly InteractPanelLayout _interactPanelLayout;

        private readonly InputSystem _inputSystem;

        private Dialogue _dialogue;

        private int _index;

        public InteractState(T uniqueID, InteractionSystem interactionSystem) : base(uniqueID)
        {
            _interactionSystem = interactionSystem;
            _interactPanelLayout = _interactionSystem.InteractPanelLayout;

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
                _dialogue = interactable.Interact() as Dialogue;
                _interactPanelLayout.Show();
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
            if (!_interactPanelLayout.Bind(_dialogue, _index))
            {
                _interactionSystem.ChangeState(GameState.Explore);
            }

            _index++;
        }

        public override void OnExit()
        {
            _inputSystem.SetActionMapDisabled(GameState.Interact.ToString());
        }
    }
}
