using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;
using DeltaReality.Quiz.Settings;

namespace DeltaReality.Quiz.QuizFunctions
{
    /// <summary>
    /// Class used for handling language icons.
    /// </summary>
    public class SettingsIconHandler : MonoBehaviour
    {
        [SerializeField] private List<SettingsData> _settingsData;

        /// <summary>
        /// Returns an object matching current display language.
        /// </summary>
        /// <returns>SettingsData object.</returns>
        public SettingsData GetDataMatchingLanguage()
        {
            foreach (SettingsData data in _settingsData)
            {
                if (data.Language.Equals(LocalizationManager.CurrentLanguage))
                {
                    return data;
                }
            }
            return null;
        }

        /// <summary>
        /// Matches a language icon with the current display language.
        /// </summary>
        /// <param name="data">Data of desired language.</param>
        /// <param name="button">Target of icon change.</param>
        public void MatchIconWithLanguage(SettingsData data, Button button)
        {
            button.image.sprite = data.LanguageIcon;
        }
    }
}
