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

        private UIDocument ui;

        async void Start()
        {
            ui = Instantiate(rootUIPrefab);
            var quizBuilderPack = TinyServiceLocator.TryResolve<QuizBuilderPack>();
            if (quizBuilderPack == null)
            {
                quizBuilderPack = debugQuizBuilderPack;
            }
            var quizNumber = 0;
            var correctNumber = 0;
            // ゲーム開始
            ui.rootVisualElement
                .Q<VisualElement>("HeaderArea")
                .visible = false;
            ui.rootVisualElement
                .Q<VisualElement>("QuestionArea")
                .visible = false;
            ui.rootVisualElement
                .Q<VisualElement>("OptionsArea")
                .visible = false;
            ui.rootVisualElement
                .Q<VisualElement>("Game_EffectCorrectArea")
                .visible = false;
            ui.rootVisualElement
                .Q<VisualElement>("Game_EffectIncorrectArea")
                .visible = false;
            ui.rootVisualElement
                .Q<VisualElement>("GameStartEffectArea")
                .visible = true;
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            ui.rootVisualElement
                .Q<VisualElement>("GameStartEffectArea")
                .visible = false;
            ui.rootVisualElement
                .Q<VisualElement>("HeaderArea")
                .visible = true;
            ui.rootVisualElement
                .Q<VisualElement>("QuestionArea")
                .visible = true;
            ui.rootVisualElement
                .Q<VisualElement>("OptionsArea")
                .visible = true;

            // クイズ部分
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
            var visualElement = ui.rootVisualElement;
            visualElement
                .Q<Label>("HeaderLabel")
                .text = string.Format(headerFormat, quizNumber + 1);
            visualElement
                .Q<Label>("QuestionLabel")
                .text = quiz.Question;
            visualElement
                .Q<VisualElement>("Game_EffectCorrectArea")
                .visible = false;
            visualElement
                .Q<VisualElement>("Game_EffectIncorrectArea")
                .visible = false;

            var optionArea = visualElement.Q<VisualElement>("OptionsArea");
            optionArea.Clear();
            var clickedAction = new Action(() =>
            {
            });
            var scope = new CancellationTokenSource();
            foreach (var i in quiz.Options)
            {
                var option = quizOptionVisualTreeAsset.CloneTree();
                var button = option.Q<Button>("Button");
                option.Q<VisualElement>("Option_EffectCorrectArea").visible = false;
                option.Q<VisualElement>("Option_EffectIncorrectArea").visible = false;
                button.text = i.message;
                button.style.backgroundColor = i.buttonColor;
                button.OnClickedAsync()
                    .Subscribe(_ =>
                    {
                        var effectName = i.isCorrect ? "Game_EffectCorrectArea" : "Game_EffectIncorrectArea";
                        visualElement.Q<VisualElement>(effectName).visible = true;
                        scope.Cancel();
                        scope.Dispose();
                        clickedAction();
                        source.TrySetResult(i.isCorrect);
                    })
                    .AddTo(scope.Token);
                clickedAction += () =>
                {
                    var effectName = i.isCorrect ? "Option_EffectCorrectArea" : "Option_EffectIncorrectArea";
                    option.Q<VisualElement>(effectName).visible = true;
                };
                optionArea.Add(option);
            }

            return source.Task;
        }
    }
}
