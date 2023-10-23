#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedRPG
{
    [RequireComponent(typeof(AudioSource))]
    public class UserInterfaceAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip onButtonSelectClip;
        [SerializeField] private AudioClip onButtonSubmitClip;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayButtonSelect()
        {
            _audioSource.clip = onButtonSelectClip;
            _audioSource.Play();
        }

        public void PlayButtonSubmit()
        {
            _audioSource.clip = onButtonSubmitClip;
            _audioSource.Play();
        }
    }
}
