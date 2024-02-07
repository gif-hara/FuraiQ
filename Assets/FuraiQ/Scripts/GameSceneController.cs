using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
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
        private VisualTreeAsset quizOptionVisualTreeAsset;

        [SerializeField]
        private QuizBuilder[] quizBuilders;

        [SerializeField]
        private string headerFormat;

        private UIDocument rootUI;

        async void Start()
        {
            rootUI = Instantiate(rootUIPrefab);
            var quizNumber = 1;
            while (true)
            {
                var quizBuilder = quizBuilders[UnityEngine.Random.Range(0, quizBuilders.Length)];
                await ApplyQuizAsync(quizBuilder.Build(), quizNumber);
                await UniTask.Delay(TimeSpan.FromSeconds(1));
                quizNumber++;
            }
        }

        private UniTask<bool> ApplyQuizAsync(IQuiz quiz, int quizNumber)
        {
            var source = new UniTaskCompletionSource<bool>();
            var visualElement = rootUI.rootVisualElement;
            visualElement
                .Q<Label>("HeaderLabel")
                .text = string.Format(headerFormat, quizNumber);
            visualElement
                .Q<Label>("QuestionLabel")
                .text = quiz.Question;
            visualElement
                .Q<VisualElement>("EffectCorrectArea")
                .visible = false;
            visualElement
                .Q<VisualElement>("EffectIncorrectArea")
                .visible = false;

            var optionArea = visualElement.Q<VisualElement>("OptionsArea");
            optionArea.Clear();
            foreach (var i in quiz.Options)
            {
                var option = quizOptionVisualTreeAsset.CloneTree();
                option.style.flexGrow = 1;
                var button = option.Q<Button>("Button");
                button.text = i.message;
                button.OnClickedAsync()
                    .Subscribe(_ =>
                    {
                        var effectName = i.isCorrect ? "EffectCorrectArea" : "EffectIncorrectArea";
                        visualElement.Q<VisualElement>(effectName).visible = true;
                        source.TrySetResult(i.isCorrect);
                    })
                    .AddTo(destroyCancellationToken);
                optionArea.Add(option);
            }

            return source.Task;
        }
    }
}
