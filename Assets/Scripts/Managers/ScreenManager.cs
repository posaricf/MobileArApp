using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace DeltaReality.Quiz.Managers
{
    /// <summary>
    /// Class used for application state manipulation.
    /// </summary>
    public class ScreenManager : MonoBehaviour
    {
        public static ScreenManager Instance;
        public static Action<AppState> OnStateChanged;
        public static Action<bool> OnArEntered;

        private const int HISTORY_SIZE = 5;
        private Queue<AppState> _stateHistory;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _stateHistory = new Queue<AppState>(HISTORY_SIZE);
            ChangeState(AppState.MainMenu);
        }

        /// <summary>
        /// Used for obtaining the state before current application state.
        /// </summary>
        /// <returns>State before the current application state.</returns>
        public AppState GetPreviousState()
        {
            Debug.Log(_stateHistory.Count);
            if (_stateHistory.Count > 1)
            {
                return _stateHistory.ElementAt(_stateHistory.Count-2);
            }

            return _stateHistory.ElementAt(_stateHistory.Count - 1);
        }

        /// <summary>
        /// Used for obtaining the current application state.
        /// </summary>
        /// <returns>Current application state.</returns>
        public AppState GetCurrentState()
        {
            return _stateHistory.ElementAt(_stateHistory.Count - 1);
        }

        /// <summary>
        /// Changes current state of application into newState.
        /// </summary>
        /// <param name="newState">New state of application.</param>
        public void ChangeState(AppState newState)
        {
            _stateHistory.Enqueue(newState);
            if(_stateHistory.Count > HISTORY_SIZE)
            {
                _stateHistory.Dequeue();
            }
            OnStateChanged?.Invoke(_stateHistory.ElementAt(_stateHistory.Count-1));
            if (newState == AppState.AR)
            {
                OnArEntered?.Invoke(true);
            }
        }
    }
}

