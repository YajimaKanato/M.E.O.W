using Interface;
using UnityEngine;

/// <summary>意思決定のUIのデータ</summary>
[CreateAssetMenu(fileName = "DecideData", menuName = "UIData/DecideData")]
public class DecideData : InitializeSO
{
    [SerializeField] int _decideValue = 2;
    [SerializeField] int _defaultIndex = 0;
    public int DecideValue => _decideValue;
    public int DefaultIndex => _defaultIndex;

    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}

/// <summary>意思決定のUIのランタイムデータ</summary>
public class DecideRuntime : IRunTime
{
    DecideData _decideData;
    ItemInfo _item;
    int _currentIndex;
    int _preIndex;
    bool _decideType;

    public ItemInfo Item => _item;
    public int CurrentIndex => _currentIndex;
    public int PreIndex => _preIndex;
    public bool DecideType => _decideType;

    public DecideRuntime(DecideData data)
    {
        _decideData = data;
        _preIndex = _currentIndex;
        _currentIndex = _decideData.DefaultIndex;
    }

    /// <summary>
    /// 意思決定イベントの種類を決定する関数
    /// </summary>
    /// <param name="type">特定のアイテムを欲するイベントかどうか</param>
    /// <param name="item">アイテムの最適解</param>
    public void DecideTypeSetting(bool type, ItemInfo item)
    {
        _decideType = type;
        _item = item;
    }

    /// <summary>
    /// 意思決定をする関数
    /// </summary>
    /// <param name="index"></param>
    public void SelectDecide(int index)
    {
        _currentIndex += index;
        //行き止まり
        //if (_currentIndex >= _decideData.DecideValue)
        //{
        //    _currentIndex = _decideData.DecideValue - 1;
        //}
        //if (_currentIndex <= 0)
        //{
        //    _currentIndex = 0;
        //}

        //ループ
        if (_currentIndex >= _decideData.DecideValue)
        {
            _currentIndex = 0;
        }
        if (_currentIndex < 0)
        {
            _currentIndex = _decideData.DecideValue - 1;
        }

        Debug.Log($"Select : {_currentIndex}");
    }
}
