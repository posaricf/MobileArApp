using DeltaReality.Quiz.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace DeltaReality.Quiz.Controllers
{
    /// <summary>
    /// Controller for information screen.
    /// </summary>
    public class InfoController : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _buttonBack;
        [SerializeField] private Button _buttonSettings;

        private void OnEnable()
        {
            _buttonBack.onClick.AddListener(OnClickBack);
            _buttonSettings.onClick.AddListener(OnClickSettings);
        }

        private void OnDisable()
        {
            _buttonBack.onClick.RemoveListener(OnClickBack);
            _buttonSettings.onClick.RemoveListener(OnClickSettings);
        }

        /// <summary>
        /// Changes the state of the application into state before when "Back" button is clicked.
        /// <see href=""/>
        /// </summary>
        private void OnClickBack()
        {
            ScreenManager.Instance.ChangeState(AppState.MainMenu);
        }

        /// <summary>
        /// Changes the state of the application into Settings when "Settings" button is clicked.
        /// <see href=""/>
        /// </summary>
        private void OnClickSettings()
        {
            ScreenManager.Instance.ChangeState(AppState.Settings);
        }
    }
}

