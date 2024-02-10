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

        [SerializeField]
        private ResultData debugResultData;

        void Start()
        {
            var root = Instantiate(rootUIPrefab);
            var resultData = TinyServiceLocator.TryResolve<ResultData>();
            if (resultData == null)
            {
                resultData = debugResultData;
            }
            root.rootVisualElement.Q<Label>("CorrectNumberLabel").text = $"{resultData.CorrectNumber} / {resultData.TotalNumber}";
            root.rootVisualElement.Q<Label>("RatingLabel").text = resultData.GetRatingName();
            root.rootVisualElement.Q<Button>("TitleButton").OnClickedAsync()
                .Subscribe(_ =>
                {
                    SceneManager.LoadScene("Title");
                })
                .AddTo(this.destroyCancellationToken);
        }
    }
}
