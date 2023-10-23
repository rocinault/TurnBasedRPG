using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedRPG
{
    public class ExploreState<T> : State<T>
    {
        private readonly InteractionSystem _interactionSystem;
        private readonly InteractPanelLayout _interactPanelLayout;

        private readonly SceneRuntimeSet _sceneRuntimeSet;

        private readonly InputSystem _inputSystem;
        private readonly SceneManagementSystem _sceneSystem;

        public ExploreState(T uniqueID, InteractionSystem interactionSystem) : base(uniqueID)
        {
            _interactionSystem = interactionSystem;

            _interactPanelLayout = _interactionSystem.InteractPanelLayout;
            _sceneRuntimeSet = _interactionSystem.SceneRuntimeSet;

            _inputSystem = App.GetSystem<InputSystem>();
            _sceneSystem = App.GetSystem<SceneManagementSystem>();
        }

        public override void OnEnter()
        {
            _interactPanelLayout.Hide();

            _inputSystem.SetActionMapEnabled(GameState.Explore.ToString());

            Interactable.OnInteract.AddHandler(OnInteractPerformed);

            Debug.Log("Entered into explore state");
        }

        public override void OnUpdate()
        {
            SceneOverworldLoading();
        }

        private void SceneOverworldLoading()
        {
            for (int i = 0; i < _sceneRuntimeSet.Count; i++)
            {
                var context = _sceneRuntimeSet.Get(i);
                _sceneSystem.LoadSceneByDistance(context);
            }
        }

        private void OnInteractPerformed(Interactable interactable)
        {
            _interactionSystem.Interactable = interactable;

            if (interactable.GetType() == typeof(Actor))
            {
                _interactionSystem.ChangeState(GameState.Interact);
            }
            else
            {
                _interactionSystem.ChangeState(GameState.Pickup);
            }
        }

        public override void OnExit()
        {
            _inputSystem.SetActionMapDisabled(GameState.Explore.ToString());

            Interactable.OnInteract.RemoveHandler(OnInteractPerformed);
        }
    }
}
