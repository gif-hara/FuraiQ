using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        private QuizBuilderPack debugQuizBuilderPack;

        [SerializeField]
        private string headerFormat;

        private UIDocument rootUI;

        async void Start()
        {
            rootUI = Instantiate(rootUIPrefab);
            var quizBuilderPack = TinyServiceLocator.TryResolve<QuizBuilderPack>();
            if (quizBuilderPack == null)
            {
                quizBuilderPack = debugQuizBuilderPack;
            }
            var quizNumber = 0;
            var correctNumber = 0;
            while (quizNumber < quizBuilderPack.QuizNumberMax)
            {
                var quizBuilder = quizBuilderPack.GetRandom();
                var isCorrect = await ApplyQuizAsync(quizBuilder.Build(), quizNumber);
                correctNumber += isCorrect ? 1 : 0;
                await UniTask.Delay(TimeSpan.FromSeconds(1));
                quizNumber++;
            }
            var resultData = new ResultData(correctNumber, quizBuilderPack.QuizNumberMax);
            TinyServiceLocator.Remove<ResultData>();
            TinyServiceLocator.Register(resultData);
            SceneManager.LoadScene("Result");
        }

        private UniTask<bool> ApplyQuizAsync(IQuiz quiz, int quizNumber)
        {
            var source = new UniTaskCompletionSource<bool>();
            var visualElement = rootUI.rootVisualElement;
            visualElement
                .Q<Label>("HeaderLabel")
                .text = string.Format(headerFormat, quizNumber + 1);
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
            var scope = new CancellationTokenSource();
            foreach (var i in quiz.Options)
            {
                var option = quizOptionVisualTreeAsset.CloneTree();
                var button = option.Q<Button>("Button");
                button.text = i.message;
                button.OnClickedAsync()
                    .Subscribe(_ =>
                    {
                        var effectName = i.isCorrect ? "EffectCorrectArea" : "EffectIncorrectArea";
                        visualElement.Q<VisualElement>(effectName).visible = true;
                        scope.Cancel();
                        scope.Dispose();
                        source.TrySetResult(i.isCorrect);
                    })
                    .AddTo(scope.Token);
                optionArea.Add(option);
            }

            return source.Task;
        }
    }
}
