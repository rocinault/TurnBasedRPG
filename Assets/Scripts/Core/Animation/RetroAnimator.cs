#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedRPG
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    [RequireComponent(typeof(SpriteRenderer))]
    public class RetroAnimator : MonoBehaviour
    {
        [SerializeField] private AnimationNode root;

        private SpriteRenderer _spriteRenderer;

        private Direction _direction;

        private int _enumState;
        private int _frame;

        private const float fps = 8f;
        private const float secondsPerFrame = 1f / fps;

        private float _tick;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _tick += Time.deltaTime;

            while (_tick >= secondsPerFrame)
            {
                _tick -= secondsPerFrame;
                UpdateSpriteRenderer();
            }
        }

        private void UpdateSpriteRenderer()
        {
            var cel = root.Resolve(_enumState).ResolveCel((int)_direction);

            _frame = (_frame + 1) % cel.Sprites.Length;

            _spriteRenderer.sprite = cel.Resolve(_frame);
        }

        public void Set(int enumState)
        {
            _enumState = enumState;
        }

        public void Set(Direction direction)
        {
            _direction = direction;
        }
    }
}
