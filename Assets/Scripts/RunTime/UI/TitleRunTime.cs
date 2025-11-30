using Interface;
using UnityEngine;

public class TitleRunTime : IRunTime
{
    TitleData _titleData;
    int _currentTitleIndex = 0;
    public int CurrentTitleIndex => _currentTitleIndex;
    public TitleRunTime(TitleData info)
    {
        _titleData = info;
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
