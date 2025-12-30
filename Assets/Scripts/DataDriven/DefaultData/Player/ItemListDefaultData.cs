using UnityEngine;

namespace DataDriven
{
    /// <summary>アイテムリストの初期データ</summary>
    [CreateAssetMenu(fileName = "ItemList", menuName = "Item/ItemList")]
    public class ItemListDefaultData : ScriptableObject
    {
        /// <summary>アイテムリストが保持するキーアイテムの配列</summary>
        [SerializeField] KeyItemDefaultData[] _items;

        public KeyItemDefaultData[] Items => _items;
    }

    /// <summary>キーアイテムの所持情報に関するクラス</summary>
    [System.Serializable]
    public class KeyItemState
    {
        [SerializeField] KeyItemDefaultData _keyItem;
        [SerializeField] bool _haveIngame;
        [SerializeField] bool _haveOutgame;

        public KeyItemDefaultData KeyItem => _keyItem;
        public bool HaveIngame => _haveIngame;
        public bool HaveOutgame => _haveOutgame;

        public KeyItemState(KeyItemDefaultData keyItem, bool haveIngame, bool haveOutgame)
        {
            _keyItem = keyItem;
            _haveIngame = haveIngame;
            _haveOutgame = haveOutgame;
        }

        /// <summary>
        /// このアイテムを取得した時に呼び出す関数
        /// </summary>
        public void GetItem()
        {
            _haveOutgame = true;
            _haveIngame = true;
        }

        /// <summary>
        /// このアイテムを与えた時に呼び出す関数
        /// </summary>
        public void GiveItem()
        {
            _haveIngame = false;
        }
    }
}
