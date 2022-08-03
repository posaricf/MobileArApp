using DeltaReality.Quiz.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace DeltaReality.Quiz.Controllers
{
    /// <summary>
    /// Main controller used for showing/hiding application screens.
    /// </summary>
    public class ScreenController : MonoBehaviour
    {
        [SerializeField] private List<ScreenObject> _screenObjects;

        private ScreenObject _currentScreenObject;

        private void OnEnable()
        {
            ScreenManager.OnStateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            ScreenManager.OnStateChanged -= OnStateChanged;
        }

        private void Update()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                AppState currentState = ScreenManager.Instance.GetCurrentState();
                if (Input.GetKeyDown(KeyCode.Escape) && currentState != AppState.MainMenu)
                {
                    AppState stateBefore = ScreenManager.Instance.GetPreviousState();
                    ScreenManager.Instance.ChangeState(stateBefore);
                    ScreenManager.OnArEntered?.Invoke(false);
                }
            }
        }

        /// <summary>
        /// Activates the screen paired with application state given as a method argument.
        /// </summary>
        /// <param name="state">New state of the application.</param>
        private void OnStateChanged(AppState state)
        {
            _currentScreenObject?.ScreenGameObject.SetActive(false);
            _currentScreenObject = FindGameObjectRepresentingScreen(state);
            _currentScreenObject.ScreenGameObject.SetActive(true);
        }

        /// <summary>
        /// Finds a game object paired with the state given as a method argument.
        /// </summary>
        /// <param name="state">State which is paired with a game object.</param>
        /// <returns>If found, returns game object paired with state, otherwise returns null.</returns>
        private ScreenObject FindGameObjectRepresentingScreen(AppState state)
        {
            foreach (ScreenObject screenObject in _screenObjects)
            {
                if (screenObject.State.Equals(state))
                {
                    return screenObject;
                }
            }
            return null;
        }
    }
}


