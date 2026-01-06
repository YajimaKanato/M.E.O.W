using UnityEngine;

namespace DataDriven
{
    /// <summary>プレイヤーに悪い効果も与えるアイテムの初期データ</summary>
    [CreateAssetMenu(fileName = "BadFood", menuName = "Item/BadFood")]
    public class BadFoodDefaultData : UsableItemDefaultData
    {
        [SerializeField] float _damage = 10;

        public float Damage => _damage;
    }
}
