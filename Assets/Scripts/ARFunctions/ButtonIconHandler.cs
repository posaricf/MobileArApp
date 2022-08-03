using DeltaReality.Quiz.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace DeltaReality.Quiz.ArFunctions
{
    /// <summary>
    /// Class that handles button icon loading.
    /// </summary>
    public class ButtonIconHandler : MonoBehaviour
    {
        private Button button;

        private void OnEnable()
        {
            ArController.OnIconLoad += SetIcon;
        }

        private void OnDisable()
        {
            ArController.OnIconLoad -= SetIcon;
        }

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        /// <summary>
        /// Sets button sprite to method argument sprite.
        /// </summary>
        /// <param name="sprite">Sprite to be set.</param>
        private void SetIcon(Sprite sprite)
        {
            button.image.sprite = sprite;
        }
    }
}
