using System;
using UnityEngine;
using I2.Loc;
using System.Collections.Generic;

namespace DeltaReality.Quiz.Managers
{
    /// <summary>
    /// Class used for managing languages
    /// </summary>
    public class SettingsManager : MonoBehaviour
    {
        public static Action<string> OnLanguageChanged;

        /// <summary>
        /// Changes language by cycling through a list containing all languages and returns the
        /// next in line.
        /// </summary>
        public void ChangeLanguage()
        {
            List<string> languages = LocalizationManager.GetAllLanguages();
            int indexOfCurrentLanguage = languages.IndexOf(LocalizationManager.CurrentLanguage);
            LocalizationManager.CurrentLanguage = indexOfCurrentLanguage+1 < languages.Count ? languages[indexOfCurrentLanguage+1] : languages[0];
            OnLanguageChanged?.Invoke(LocalizationManager.CurrentLanguage);
        }
    }
}
