#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedRPG
{
    public class PickupAbilityOnInteract : Interactable, IPersistable
    {
        [SerializeField] private Ability ability;
        [SerializeField] private DataSettings dataSettings;

        private bool _canInteract = true;

        public override bool CanInteract()
        {
            return _canInteract;
        }

        public override object Interact()
        {
            _canInteract = false;

            return ability;
        }

        public DataSettings GetDataSettings()
        {
            return dataSettings;
        }

        public void Load(Data data)
        {
            var save = (Data<SaveData>)data;

            _canInteract = save.Content.CanInteract;
        }

        public Data Save()
        {
            var save = new SaveData();

            save.CanInteract = _canInteract;

            return new Data<SaveData>(save);
        }

        internal struct SaveData
        {
            public bool CanInteract { get; set; }
        }
    }
}
