using System.Collections.Generic;
using UnityEngine;
using I2.Loc;

namespace DeltaReality.Quiz.QuizFunctions
{
    /// <summary>
    /// Scriptable object which represents a question in a quiz.
    /// </summary>
    [CreateAssetMenu(fileName = "QuestionData", menuName = "ScriptableObjects/QuestionScriptableObject", order = 1)]
    public class QuestionScriptableObject : ScriptableObject
    {
        [TermsPopup] public string QuestionText;
        public List<Answer> Answers;
    } 
}
