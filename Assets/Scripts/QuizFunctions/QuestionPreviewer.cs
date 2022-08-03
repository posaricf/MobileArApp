using I2.Loc;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DeltaReality.Quiz.QuizFunctions
{
    /// <summary>
    /// Class used for prepairing the questions and answers to be displayed.
    /// </summary>
    public class QuestionPreviewer : MonoBehaviour
    {
        [Header("Local References")]
        [SerializeField] private Text _questionText;
        [SerializeField] private AnswerManipulator _answerManipulator;
        [SerializeField] private GameObject _answerButtonsParent;
        [SerializeField] private Button[] _answerButtons;

        public static Action OnQuizEnd;
        public static Action<bool> OnToggleNext;
        public static Action<bool> OnToggleOtherButtons;

        private const string TOTAL_SCORE = "Total score";
        private List<QuestionScriptableObject> _questions;
        private int _score;

        private void OnEnable()
        {
            AnswerButton.OnNextQuestion += EnableNextQuestion;
        }

        private void OnDisable()
        {
            AnswerButton.OnNextQuestion -= EnableNextQuestion;
        }

        public void SetQuestions(List<QuestionScriptableObject> questions)
        {
            _questions = questions;
        }

        /// <summary>
        /// Sets the value of the Text element to the question text.
        /// </summary>
        /// <param name="question">Question to be displayed.</param>
        public void SetQuestionText(QuestionScriptableObject question)
        {
            Localize localization = _questionText.GetComponent<Localize>();

            if (localization.TermSuffix != "")
            {
                localization.TermSuffix = "";
            }

            localization.Term = question.QuestionText;
        }

        /// <summary>
        /// Toggles interactability of answer buttons.
        /// </summary>
        /// <param name="toggleValue">True if interactable, false otherwise.</param>
        public void ToggleAnswerButtonsInteractability(bool toggleValue)
        {
            foreach (Button button in _answerButtons)
            {
                AnswerButton answerButton = button.GetComponent(typeof(AnswerButton)) as AnswerButton;
                answerButton.ToggleButtonInteractability(toggleValue);
            }
        }

        /// <summary>
        /// Toggles answer buttons visibility.
        /// </summary>
        /// <param name="toggleValue">True to activate them, false otherwise.</param>
        public void ToggleAnswerButtonsVisibility(bool toggleValue)
        {
            _answerButtonsParent.gameObject.SetActive(toggleValue);
        }

        /// <summary>
        /// If there are unused questions, invokes methods to show them and deactivates "Next"
        /// button, otherwise shows total score, "Main menu" and "Restart" buttons.
        /// </summary>
        public void NextQuestion()
        {
            OnToggleNext?.Invoke(false);
            if (_questions.Count != 0)
            {
                QuestionScriptableObject question = GetFirstListElement(_questions);
                SetQuestionText(question);
                SetAnswerButtons(question);
            }
            else
            {
                ToggleAnswerButtonsVisibility(false);
                OnQuizEnd?.Invoke();
                ShowTotalScore(_score);
                OnToggleOtherButtons?.Invoke(true);
            }
        }

        /// <summary>
        /// Toggles button visibility.
        /// </summary>
        /// <param name="button">Button for which the visibility will be changed.</param>
        /// <param name="toggleValue">True/false value.</param>
        public void ToggleVisibility(Button button, bool toggleValue)
        {
            button.gameObject.SetActive(toggleValue);
        }

        /// <summary>
        /// Sets quiz score.
        /// </summary>
        /// <param name="score">Quiz score.</param>
        public void SetScore(int score)
        {
            _score = score;
        }

        /// <summary>
        /// Sets answer button values for a question given as a method argument.
        /// </summary>
        /// <param name="question">Scriptable object which contains answers for the question.</param>
        private void SetAnswerButtons(QuestionScriptableObject question)
        {
            Button[] answerButtons = _answerManipulator.ShuffleButtonOrder(_answerButtons);
            for (int i = 0; i < _answerButtons.Length; i++)
            {
                AnswerButton answerButton = answerButtons[i].GetComponent<AnswerButton>();
                answerButton.SetButton(question.Answers[i]);
            }
        }

        /// <summary>
        /// Shows total score as text of a Text element.
        /// </summary>
        /// <param name="totalScore">Total score acquired by player.</param>
        private void ShowTotalScore(int totalScore)
        {
            ToggleAnswerButtonsVisibility(false);
            Localize localization = _questionText.GetComponent<Localize>();
            localization.TermSuffix = ": " + totalScore;
            localization.Term = TOTAL_SCORE;
        }

        /// <summary>
        /// Returns the first element of a list given as a method argument.
        /// </summary>
        /// <param name="list">List of QuestionScriptableObject elements.</param>
        /// <returns>First element of the list.</returns>
        private QuestionScriptableObject GetFirstListElement(List<QuestionScriptableObject> list)
        {
            return list[0];
        }

        /// <summary>
        /// Enables "Next" button, removes interactability from answer buttons.
        /// </summary>
        /// <param name="answer"></param>
        private void EnableNextQuestion()
        {
            QuestionScriptableObject question = GetFirstListElement(_questions);
            _questions.Remove(question);
            ToggleAnswerButtonsInteractability(false);
            OnToggleNext?.Invoke(true);
        }
    }
}
