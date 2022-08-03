using DeltaReality.Quiz.Managers;
using DeltaReality.Quiz.QuizFunctions;
using I2.Loc;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DeltaReality.Quiz.Controllers
{
    /// <summary>
    /// Controller for quiz screen.
    /// </summary>
    public class QuizController : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonNext;
        [SerializeField] private Button _buttonMainMenu;
        [SerializeField] private Button _buttonRestart;
        
        [Header("Question Handlers")]
        [SerializeField] private QuestionGenerator _questionGenerator;
        [SerializeField] private QuestionPreviewer _questionPreviewer;
        [SerializeField] private QuestionResult _questionResult;

        [Header("Local References")]
        [SerializeField] private List<QuestionScriptableObject> _questions;
        [SerializeField] private int _numberOfQuestions;

        private List<QuestionScriptableObject> _randomizedQuestions;

        private void Start()
        {
            SetQuiz();
        }

        private void OnEnable()
        {
            _buttonSettings.onClick.AddListener(OnClickSettings);
            _buttonNext.onClick.AddListener(OnClickNext);
            _buttonMainMenu.onClick.AddListener(OnClickMainMenu);
            _buttonRestart.onClick.AddListener(OnClickRestartQuiz);
            QuestionPreviewer.OnQuizEnd += PropagateScore;
            QuestionPreviewer.OnToggleNext += ToggleNextButton;
            QuestionPreviewer.OnToggleOtherButtons += ToggleOtherButtons;
        }

        private void OnDisable()
        {
            _buttonSettings.onClick.RemoveListener(OnClickSettings);
            _buttonNext.onClick.RemoveListener(OnClickNext);
            _buttonMainMenu.onClick.RemoveListener(OnClickMainMenu);
            _buttonRestart.onClick.RemoveListener(OnClickRestartQuiz);
            QuestionPreviewer.OnQuizEnd -= PropagateScore;
            QuestionPreviewer.OnToggleNext -= ToggleNextButton;
            QuestionPreviewer.OnToggleOtherButtons -= ToggleOtherButtons;
        }

        /// <summary>
        /// Restarts the quiz when "Restart" button is clicked.
        /// </summary>
        private void OnClickRestartQuiz()
        {
            SetQuiz();
        }

        /// <summary>
        /// Changes the state of the application into MainMenu when "Main menu" button is clicked and resets the quiz.
        /// </summary>
        private void OnClickMainMenu()
        {
            SetQuiz();
            ScreenManager.Instance.ChangeState(AppState.MainMenu);
        }

        /// <summary>
        /// Changes the state of the application into Settings when "Settings" button is clicked.
        /// </summary>
        private void OnClickSettings()
        {
            ScreenManager.Instance.ChangeState(AppState.Settings);
        }

        /// <summary>
        /// Changes the question when "Next" button is clicked.
        /// </summary>
        private void OnClickNext()
        {
            _questionPreviewer.NextQuestion();
        }

        /// <summary>
        /// Sets the quiz for a new game; randomizes questions, invokes methods for showing text and
        /// buttons, resets score.
        /// </summary>
        private void SetQuiz()
        {
            _randomizedQuestions = _questionGenerator.GetRandomQuestions(_questions, _numberOfQuestions);

            _questionPreviewer.SetQuestions(_randomizedQuestions);
            _questionPreviewer.NextQuestion();

            _questionResult.ResetScore();
            _questionPreviewer.ToggleAnswerButtonsVisibility(true);

            _questionPreviewer.ToggleVisibility(_buttonMainMenu, false);
            _questionPreviewer.ToggleVisibility(_buttonRestart, false);
        }

        /// <summary>
        /// Propagates score to question previewer.
        /// </summary>
        private void PropagateScore()
        {
            int score = _questionResult.GetScore();
            _questionPreviewer.SetScore(score);
        }

        /// <summary>
        /// Toggles "Next" button depending on method argument.
        /// </summary>
        /// <param name="toggleValue">True to activate it, false otherwise.</param>
        private void ToggleNextButton(bool toggleValue)
        {
            _questionPreviewer.ToggleVisibility(_buttonNext, toggleValue);
        }

        /// <summary>
        /// Toggles "Main menu" and "Restart" buttons depending on method argument.
        /// </summary>
        /// <param name="toggleValue">True to activate them, false otherwise.</param>
        private void ToggleOtherButtons(bool toggleValue)
        {
            _questionPreviewer.ToggleVisibility(_buttonMainMenu, toggleValue);
            _questionPreviewer.ToggleVisibility(_buttonRestart, toggleValue);
        }
    }
}
