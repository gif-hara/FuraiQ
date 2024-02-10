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
    public sealed class ResultSceneController : MonoBehaviour
    {
        [SerializeField]
        private UIDocument rootUIPrefab;

        void Start()
        {
            var root = Instantiate(rootUIPrefab);
            root.rootVisualElement.Q<Button>("TitleButton").OnClickedAsync()
                .Subscribe(_ =>
                {
                    SceneManager.LoadScene("Title");
                })
                .AddTo(this.destroyCancellationToken);
        }
    }
}
