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
            Instantiate(rootUIPrefab);
        }
    }
}
