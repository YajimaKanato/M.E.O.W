using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemDataList", menuName = "Scriptable Objects/ItemDataList")]
public class ItemDataList : ScriptableObject
{
    [SerializeField] List<ItemData> _itemList;
    public List<ItemData> ItemList => _itemList;
}

[System.Serializable]
public class ItemData
{
    [SerializeField, Tooltip("ƒAƒCƒeƒ€")] ItemBase _itemBase;
    [SerializeField, Tooltip("ŠŽãŒÀ")] int _possessLimit = 1;
    public ItemBase Itembase => _itemBase;
    public int PossessLimit => _possessLimit;
}
