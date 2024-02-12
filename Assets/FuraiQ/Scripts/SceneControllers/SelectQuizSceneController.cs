using System;
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
        private VisualTreeAsset headerVisualTreeAsset;

        [SerializeField]
        private VisualTreeAsset quizButtonVisualTreeAsset;

        [SerializeField]
        private List<QuizBuilderPackData> quizDatabase;

        void Start()
        {
            var ui = Instantiate(rootUIPrefab);
            var listArea = ui.rootVisualElement.Q<ListView>("ListArea");
            var UIElements = new List<VisualElement>();
            foreach (var i in quizDatabase)
            {
                var header = headerVisualTreeAsset.CloneTree();
                header.Q<Label>("HeaderLabel").text = i.header;
                UIElements.Add(header);
                foreach (var pack in i.packs)
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
                    UIElements.Add(uiElement);
                }
            }
            listArea.makeItem = () => new VisualElement();
            listArea.itemsSource = UIElements;
            listArea.bindItem = (element, i) => element.Add(UIElements[i]);
        }

        [Serializable]
        private class QuizBuilderPackData
        {
            public string header;

            public List<QuizBuilderPack> packs;
        }
    }
}
