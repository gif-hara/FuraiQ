using UnityEngine;
using UnityEngine.UIElements;

namespace FuraiQ
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GameSceneController : MonoBehaviour
    {
        [SerializeField]
        private UIDocument rootUIPrefab;

        [SerializeField]
        private VisualElement quizOptionVisualElement;

        [SerializeField]
        private QuizBuilder quizBuilder;

        private UIDocument rootUI;

        void Start()
        {
            rootUI = Instantiate(rootUIPrefab);
            var quiz = quizBuilder.Build();
            ApplyQuiz(quiz);
        }

        private void ApplyQuiz(IQuiz quiz)
        {
            var questionLabel = rootUI.rootVisualElement.Q<Label>("QuestionLabel");
            questionLabel.text = quiz.Question;
        }
    }
}
