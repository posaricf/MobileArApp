using UnityEngine;
using UnityEngine.UI;
using I2.Loc;
using DeltaReality.Quiz.QuizFunctions;
using DeltaReality.Quiz.Managers;
using DeltaReality.Quiz.Settings;

namespace DeltaReality.Quiz.Controllers
{
    /// <summary>
    /// Controller for settings screen.
    /// </summary>
    public class SettingsController : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _buttonBack;
        [SerializeField] private Button _buttonLanguage;

        [Header("Settings handlers")]
        [SerializeField] private SettingsManager _settingsManager;
        [SerializeField] private SettingsIconHandler _settingsIconHandler;

        private void OnEnable()
        {
            _buttonBack.onClick.AddListener(OnClickBack);
            _buttonLanguage.onClick.AddListener(OnClickChangeLanguage);
            SettingsManager.OnLanguageChanged += OnClickChangeIcon;
        }

        private void OnDisable()
        {
            _buttonBack.onClick.RemoveListener(OnClickBack);
            _buttonLanguage.onClick.RemoveListener(OnClickChangeLanguage);
            SettingsManager.OnLanguageChanged -= OnClickChangeIcon;
        }

        private void Start()
        {
            OnClickChangeIcon(LocalizationManager.CurrentLanguage);
        }

        /// <summary>
        /// Changes the state of the application into the state that occured before the current 
        /// one when "Back" button is clicked.
        /// </summary>
        private void OnClickBack()
        {
            AppState stateBefore = ScreenManager.Instance.GetPreviousState();
            ScreenManager.Instance.ChangeState(stateBefore);
        }

        /// <summary>
        /// Changes current text language when language icon is clicked.
        /// </summary>
        private void OnClickChangeLanguage()
        {
            _settingsManager.ChangeLanguage();
        }

        /// <summary>
        /// Changes current language icon.
        /// </summary>
        /// <param name="language">Icon will represent this language.</param>
        private void OnClickChangeIcon(string language)
        {
            SettingsData currentSettingsData = _settingsIconHandler.GetDataMatchingLanguage();
            _settingsIconHandler.MatchIconWithLanguage(currentSettingsData, _buttonLanguage);
        }
    }
}

