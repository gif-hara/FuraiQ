using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine.UIElements;

namespace FuraiQ
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extensions
    {
        public static IUniTaskAsyncEnumerable<AsyncUnit> OnClickedAsync(this Button self)
        {
            return UniTaskAsyncEnumerable.Create<AsyncUnit>(async (writer, token) =>
            {
                void OnClicked()
                {
                    writer.YieldAsync(AsyncUnit.Default);
                }
                self.clicked += OnClicked;
                await UniTask.WaitUntilCanceled(token);
                self.clicked -= OnClicked;
            });
        }

    }
}
