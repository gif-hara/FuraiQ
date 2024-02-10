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
        private List<QuizBuilderPack> quizBuilderPacks;

        void Start()
        {
            var root = Instantiate(rootUIPrefab);
        }
    }
}
