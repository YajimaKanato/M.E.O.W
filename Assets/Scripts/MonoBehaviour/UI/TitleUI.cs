using Interface;
using UnityEngine;

public class TitleUI : UIBehaviour, ISelectableUI, IEnterUI
{
    [SerializeField] TitleSelect[] _titleSelects;

    int _currentSelectIndex = 0;
    int _preSelectIndex = 0;
    public override bool Init(GameManager manager)
    {
        _gameManager = manager;
        if (!_gameManager) FailedInitialization();
        return _isInitialized;
    }

    public void PushEnter()
    {

    }

    /// <summary>
    /// 項目選択中を更新する関数
    /// </summary>
    public void SelectedSlot()
    {
        _preSelectIndex = _currentSelectIndex;
        _currentSelectIndex = _gameManager.DataManager.TitleRunTime.CurrentTitleIndex;
        _titleSelects[_preSelectIndex].SelectSign(false);
        _titleSelects[_currentSelectIndex].SelectSign(true);
    }
}
