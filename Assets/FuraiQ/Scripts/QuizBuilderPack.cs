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
        private string packName;

        [SerializeField]
        private QuizBuilder[] quizBuilders;

        public string PackName => packName;

        public QuizBuilder[] QuizBuilders => quizBuilders;

        public QuizBuilder GetRandom()
        {
            return quizBuilders[Random.Range(0, quizBuilders.Length)];
        }
    }
}
