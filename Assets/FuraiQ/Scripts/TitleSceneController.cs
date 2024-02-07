using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace FuraiQ
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class TitleSceneController : MonoBehaviour
    {
        [SerializeField]
        private UIDocument rootUIPrefab;

        void Start()
        {
            var root = Instantiate(rootUIPrefab);
            root.rootVisualElement.Q<Button>("StartButton").OnClickedAsync()
                .Subscribe(_ =>
                {
                    Debug.Log("StartButton Clicked");
                })
                .AddTo(this.destroyCancellationToken);
        }
    }
}
