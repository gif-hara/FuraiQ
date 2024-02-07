namespace FuraiQ
{
    /// <summary>
    /// クイズビルダー
    /// </summary>
    public interface IQuizBuilder
    {
        /// <summary>
        /// クイズを作成する
        /// </summary>
        IQuiz Build();
    }
}
