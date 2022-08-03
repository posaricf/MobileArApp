using I2.Loc;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace DeltaReality.Quiz.QuizFunctions
{
    /// <summary>
    /// Class that handles button options.
    /// </summary>
    public class AnswerButton : MonoBehaviour
    {
        [Header("Local References")]
        [SerializeField] private Button _button;
        [SerializeField] private Text _text;

        [Header("Settings")]
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _correctColor;
        [SerializeField] private Color _invalidColor;

        public static Action<Answer> OnAnswerClicked;
        public static Action OnNextQuestion;

        private Answer _answer;

        private void OnEnable()
        {
            _button.onClick.AddListener(AnswerButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(AnswerButtonClicked);
        }
        
        /// <summary>
        /// Sets button text, interactability and color.
        /// </summary>
        /// <param name="answer">Answer binded to the button.</param>
        public void SetButton(Answer answer)
        {
            _answer = answer;
            Localize localization = _button.GetComponentInChildren<Localize>();
            localization.Term = _answer.AnswerText;
            ToggleButtonInteractability(true);
            PaintButton(_defaultColor);
        }

        /// <summary>
        /// Toggles button interactability.
        /// </summary>
        /// <param name="toggleValue">True if interactable, false otherwise.</param>
        public void ToggleButtonInteractability(bool toggleValue)
        {
            _button.interactable = toggleValue;
        }

        /// <summary>
        /// Paints the button and invokes an event when an answer button is clicked.
        /// </summary>
        private void AnswerButtonClicked()
        {
            PaintButton(_answer.Correct ? _correctColor : _invalidColor);
            OnAnswerClicked?.Invoke(_answer);
            OnNextQuestion?.Invoke();
        }

        /// <summary>
        /// Paints the button in the color given as a method argument.
        /// </summary>
        /// <param name="color">Desired color of the button.</param>
        private void PaintButton(Color color)
        {
            _button.image.color = color;
        }
    }
}

