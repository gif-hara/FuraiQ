using System;

namespace FuraiQ
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class ItemAttribute
    {
        public string key;

        public string value;

        public int ParseToInt()
        {
            return int.Parse(value);
        }
    }
}
