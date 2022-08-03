using System;
using UnityEngine;

namespace DeltaReality.Quiz.QuizFunctions
{
    /// <summary>
    /// Class that contains methods for handling question results.
    /// </summary>
    public class QuestionResult : MonoBehaviour
    {
        private int _score;

        private void OnEnable()
        {
            AnswerButton.OnAnswerClicked += CheckAnswer;
        }

        private void OnDisable()
        {
            AnswerButton.OnAnswerClicked -= CheckAnswer;
        }

        /// <summary>
        /// Checks if a given answer is correct, and if so, increases the score.
        /// </summary>
        /// <param name="answer">Answer to check.</param>
        public void CheckAnswer(Answer answer)
        {
            _score = answer.Correct ? ++_score : _score;
        }

        /// <summary>
        /// Resets the quiz score.
        /// </summary>
        public void ResetScore()
        {
            _score = 0;
        }

        /// <summary>
        /// Used to obtain current quiz score.
        /// </summary>
        /// <returns>Current quiz score.</returns>
        public int GetScore()
        {
            return _score;
        }
    }
}

