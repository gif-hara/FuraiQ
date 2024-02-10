using UnityEngine;

namespace FuraiQ
{
    /// <summary>
    /// <see cref="QuizBuilder"/>をまとめたパッケージ
    /// </summary>
    [CreateAssetMenu(fileName = "QuizBuilderPack", menuName = "FuraiQ/QuizBuilderPack")]
    public sealed class QuizBuilderPack : ScriptableObject
    {
        [SerializeField]
        private QuizBuilder[] quizBuilders;

        public QuizBuilder[] QuizBuilders => quizBuilders;
    }
}
