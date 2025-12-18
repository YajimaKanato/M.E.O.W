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
    int _currentIndex;
    int _preIndex;

    public int CurrentIndex => _currentIndex;
    public int PreIndex => _preIndex;

    public DecideRuntime(DecideData data)
    {
        _decideData = data;
        _preIndex = _currentIndex;
        _currentIndex = _decideData.DefaultIndex;
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
