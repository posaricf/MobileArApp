using System;
using I2.Loc;

namespace DeltaReality.Quiz.QuizFunctions
{
    /// <summary>
    /// Class which represents an answer in a quiz.
    /// </summary>
    [Serializable]
    public class Answer
    {
        public bool Correct;
        [TermsPopup] public string AnswerText;
    }
}
