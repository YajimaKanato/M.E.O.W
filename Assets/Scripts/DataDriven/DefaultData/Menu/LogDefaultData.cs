using UnityEngine;

namespace DataDriven
{
    /// <summary>会話ログの初期データ</summary>
    [CreateAssetMenu(fileName = "Log", menuName = "Menu/MenuCategory/Log")]
    public class LogDefaultData : MenuCategory
    {
        [SerializeField] int _maxLogCount = 5;

        public int MaxLogCount => _maxLogCount;
    }
}
