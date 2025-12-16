using Interface;
using UnityEngine;

/// <summary>タイトルのUIのデータ</summary>
[CreateAssetMenu(fileName = "TitleData", menuName = "UIData/TitleData")]
public class TitleData : InitializeSO
{
    [SerializeField, Tooltip("タイトルの選択項目の数")] int _titleCategoryCount = 5;
    [SerializeField, Tooltip("最初に選んでいるタイトルの項目の番号")] int _defaultSelectIndex = 0;
    public int TitleCategoryCount => _titleCategoryCount;
    public int DefaultSelectIndex => _defaultSelectIndex;

    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}

#region Title
/// <summary>タイトルのUIのランタイムデータ</summary>
public class TitleRunTime : IRunTime
{
    TitleData _titleData;
    int _titleIndex;
    int _currentTitleIndex = 0;
    public int TitleIndex => _titleIndex;
    public int CurrentTitleIndex => _currentTitleIndex;

    public TitleRunTime(TitleData info)
    {
        _titleData = info;
        _titleIndex = _titleData.TitleCategoryCount;
        _currentTitleIndex = _titleData.DefaultSelectIndex;
    }

    /// <summary>
    /// タイトルセレクトをする関数
    /// </summary>
    /// <param name="index"></param>
    public void SelectTitle(int index)
    {
        _currentTitleIndex += index;
        //行き止まり
        //if (_currentTitleIndex >= _playerInfo.TitleIndexCount)
        //{
        //    _currentTitleIndex = _playerInfo.TitleIndexCount - 1;
        //}
        //if (_currentTitleIndex <= 0)
        //{
        //    _currentTitleIndex = 0;
        //}

        //ループ
        if (_currentTitleIndex >= _titleData.TitleCategoryCount)
        {
            _currentTitleIndex = 0;
        }
        if (_currentTitleIndex < 0)
        {
            _currentTitleIndex = _titleData.TitleCategoryCount - 1;
        }

        Debug.Log($"Select : {_currentTitleIndex}");
    }
}
#endregion

namespace Title
{
    public enum TitleCategory
    {
        Start,
        EndingList,
        Option,
        Credit,
        Reset

    }
}
