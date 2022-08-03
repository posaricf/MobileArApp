using System;
using UnityEngine;

namespace DeltaReality.Quiz.Managers
{
    /// <summary>
    /// Class used for managing touch behaviour.
    /// </summary>
    public class TouchManager : MonoBehaviour
    {
        public static TouchManager Instance;
        public static Action<Vector3> OnSingleTouch;
        public static Action<Vector3> OnSwipe;
        public static Action<float> OnPinch;
        public static Action OnDone;

        private Vector3 _previousPosition;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    SingleTouch();
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Swipe();
                }

                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    InteractionEnded();
                }
            }
            else if (Input.touchCount == 2)
            {
                Pinch();
            }
        }

        /// <summary>
        /// Triggers an event with touch position.
        /// </summary>
        private void SingleTouch()
        {
            OnSingleTouch?.Invoke(Input.GetTouch(0).position);
            _previousPosition = Input.GetTouch(0).position;
        }

        /// <summary>
        /// Triggers an event with a velocity vector.
        /// </summary>
        private void Swipe()
        {
            Vector3 position = Input.GetTouch(0).position;
            Vector3 velocity = (_previousPosition - position) / Time.deltaTime;

            OnSwipe?.Invoke(velocity);
            _previousPosition = position;
        }

        /// <summary>
        /// Triggers an event with distance between two touches.
        /// </summary>
        private void Pinch()
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                float distanceBetweenTouches = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                OnPinch?.Invoke(distanceBetweenTouches);
            }
        }

        /// <summary>
        /// Triggers an event signaling end of interaction.
        /// </summary>
        private void InteractionEnded()
        {
            OnDone?.Invoke();
        }
    }
}
