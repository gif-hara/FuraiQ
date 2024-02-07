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
        private QuizBuilder quizBuilder;

        private UIDocument rootUI;

        async void Start()
        {
            rootUI = Instantiate(rootUIPrefab);
            while (true)
            {
                await ApplyQuizAsync(quizBuilder.Build());
                await UniTask.Delay(TimeSpan.FromSeconds(1));
            }
        }

        private UniTask<bool> ApplyQuizAsync(IQuiz quiz)
        {
            var source = new UniTaskCompletionSource<bool>();
            var visualElement = rootUI.rootVisualElement;
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
