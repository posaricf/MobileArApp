using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DeltaReality.Quiz.QuizFunctions
{
    /// <summary>
    /// Class used for question manipulation
    /// </summary>
    public class AnswerManipulator : MonoBehaviour
    {
        private static readonly System.Random _random = new System.Random();

        /// <summary>
        /// Shuffles order of answer buttons.
        /// </summary>
        /// <param name="answerButtons">Array of Button elements to shuffle.</param>
        /// <returns>Array of shuffled Button elements.</returns>
        public Button[] ShuffleButtonOrder(Button[] answerButtons)
        {
            Button[] copyOfAnswerButtons = new Button[answerButtons.Length];
            copyOfAnswerButtons = answerButtons.OrderBy(x => _random.Next()).ToArray();
            return copyOfAnswerButtons;
        }
    }
}
