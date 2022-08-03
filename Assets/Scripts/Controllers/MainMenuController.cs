using DeltaReality.Quiz.Managers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace DeltaReality.Quiz.Controllers
{
    /// <summary>
    /// Controller for main menu screen.
    /// </summary>
    public class MainMenuController : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _buttonQuiz;
        [SerializeField] private Button _buttonAR;
        [SerializeField] private Button _buttonInfo;
        [SerializeField] private Button _buttonSettings; 

        private void OnEnable()
        {
            _buttonQuiz.onClick.AddListener(OnClickQuiz);
            _buttonAR.onClick.AddListener(OnClickAR);
            _buttonInfo.onClick.AddListener(OnClickInfo);
            _buttonSettings.onClick.AddListener(OnClickSettings);
        }

        private void OnDisable()
        {
            _buttonQuiz.onClick.RemoveListener(OnClickQuiz);
            _buttonAR.onClick.RemoveListener(OnClickAR);
            _buttonInfo.onClick.RemoveListener(OnClickInfo);
            _buttonSettings.onClick.RemoveListener(OnClickSettings);
        }

        /// <summary>
        /// Changes the state of the application into AR when "AR" button is clicked.
        /// </summary>
        private void OnClickAR()
        {
            ScreenManager.Instance.ChangeState(AppState.AR);
        }

        /// <summary>
        /// Changes the state of the application into Quiz when "Quiz" button is clicked.
        /// </summary>
        private void OnClickQuiz()
        {
            ScreenManager.Instance.ChangeState(AppState.Quiz);
        }

        /// <summary>
        /// Changes the state of the application into Info when "Info" button is clicked.
        /// </summary>
        private void OnClickInfo()
        {
            ScreenManager.Instance.ChangeState(AppState.Info);
        }

        /// <summary>
        /// Changes the state of the application into Settings when "Settings" button is clicked.
        /// </summary>
        private void OnClickSettings()
        {
            ScreenManager.Instance.ChangeState(AppState.Settings);
        }
    }
}
