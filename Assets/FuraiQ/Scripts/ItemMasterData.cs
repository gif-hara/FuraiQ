using UnityEngine;

namespace FuraiQ
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "ItemMasterData", menuName = "FuraiQ/ItemMasterData")]
    public sealed class ItemMasterData : ScriptableObject
    {
        [SerializeField]
        private ItemData[] items;

        public ItemData[] Items => items;
    }
}
