using UnityEngine;

namespace DataDriven
{
    /// <summary>ホットバーの初期データ</summary>
    [CreateAssetMenu(fileName = "HotbarDefaultData", menuName = "Player/HotbarDefaultData")]
    public class HotbarDefaultData : ScriptableObject
    {
        [SerializeField] UsableItemDefaultData[] _hotbar;

        public UsableItemDefaultData[] Hotbar => _hotbar;
    }
}
