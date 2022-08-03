using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DeltaReality.Quiz.QuizFunctions
{
    /// <summary>
    /// Class used for generating questions for a quiz.
    /// </summary>
    public class QuestionGenerator : MonoBehaviour
    {
        private static readonly System.Random _random = new System.Random();

        /// <summary>
        /// Used for creating a list of a desired size that contains QuestionScriptableObject objects in a random order.
        /// </summary>
        /// <param name="questions">List of QuestionScriptableObject objects.</param>
        /// <param name="numberOfQuestions">Number of questions the list will contain.</param>
        /// <returns>List of randomly ordered QuestionScriptableObject objects.</returns>
        public List<QuestionScriptableObject> GetRandomQuestions(List<QuestionScriptableObject> questions, int numberOfQuestions)
        {
            List<QuestionScriptableObject> _copyOfQuestions;
            _copyOfQuestions = new List<QuestionScriptableObject>();
            _copyOfQuestions = questions.OrderBy(a => _random.Next()).Take(numberOfQuestions).ToList();
            return _copyOfQuestions;
        }
    }
}

