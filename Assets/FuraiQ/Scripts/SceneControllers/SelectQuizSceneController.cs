using System.Collections.Generic;
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
    public sealed class SelectQuizSceneController : MonoBehaviour
    {
        [SerializeField]
        private UIDocument rootUIPrefab;

        [SerializeField]
        private VisualTreeAsset quizButtonVisualTreeAsset;

        [SerializeField]
        private List<QuizBuilderPack> quizBuilderPacks;

        void Start()
        {
            var root = Instantiate(rootUIPrefab);
            var listArea = root.rootVisualElement.Q<VisualElement>("ListArea");
            listArea.Clear();
            foreach (var pack in quizBuilderPacks)
            {
                var uiElement = quizButtonVisualTreeAsset.CloneTree();
                var button = uiElement.Q<Button>("Button");
                button.text = pack.PackName;
                button.OnClickedAsync()
                    .Subscribe(_ =>
                    {
                        TinyServiceLocator.Remove<QuizBuilderPack>();
                        TinyServiceLocator.Register(pack);
                        SceneManager.LoadScene("Game");
                    })
                    .AddTo(this.destroyCancellationToken);
                listArea.Add(uiElement);
            }
        }
    }
}
