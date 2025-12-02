using Interface;
using UnityEngine;

public class TitleUI : UIBehaviour, ISelectableVerticalArrowUI, IEnterUI
{
    [SerializeField] TitleSelect[] _titleSelects;
    TitleRunTime _titleRunTime;
    int _currentSelectIndex = 0;
    int _preSelectIndex = 0;
    public override bool Init(GameManager manager)
    {
        InitializationForVariable(out _gameManager, manager);
        InitializationForVariable(out _titleRunTime, _gameManager.DataManager.TitleRunTime);

        if (_titleSelects == null) FailedInitialization();
        //アイテムスロットの初期化
        var length = _titleRunTime.TitleIndex;
        for (int i = 0; i < length; i++)
        {
            if (!_titleSelects[i])
            {
                FailedInitialization();
                break;
            }
            _titleSelects[i].SelectSign(i == 0);
        }
        return _isInitialized;
    }

    public void PushEnter()
    {

    }

    /// <summary>
    /// 項目選択中を更新する関数
    /// </summary>
    public void SelectedCategory()
    {
        _preSelectIndex = _currentSelectIndex;
        _currentSelectIndex = _titleRunTime.CurrentTitleIndex;
        _titleSelects[_preSelectIndex].SelectSign(false);
        _titleSelects[_currentSelectIndex].SelectSign(true);
    }
}
