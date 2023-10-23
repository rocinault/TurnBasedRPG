#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedRPG
{
    public class InteractPanelLayout : PanelLayout
    {
        [Header("Panels")]
        [SerializeField] private InteractPanel interactPanel;

        private void Awake()
        {
            Initialise();         
        }

        protected override void Initialise()
        {
            panels.Add(interactPanel);

            currentPanel = interactPanel;
        }

        public bool Bind(Dialogue dialogue, int index)
        {
            if (index < dialogue.Sentences.Length)
            {
                string text = dialogue.Sentences[index];

                interactPanel.Bind(text);

                return true;
            }

            return false;
        }

        public bool Bind(Ability ability, int index)
        {
            if (index < 1)
            {
                string text = ability.Description;

                interactPanel.Bind(text);

                return true;
            }

            return false;
        }
    }
}
