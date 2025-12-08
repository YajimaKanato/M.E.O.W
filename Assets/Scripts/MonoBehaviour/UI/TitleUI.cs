using Interface;
using UnityEngine;

public class TitleUI : UIBehaviour, ISelectableVerticalArrowUI, IEnterUI
{
    [SerializeField] TitleData _data;
    [SerializeField] TitleSelect[] _titleSelects;
    OutGameUIManager _outGameUIManager;
    TitleRunTime _titleRunTime;
    int _currentSelectIndex = 0;
    int _preSelectIndex = 0;

    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);
        InitializeManager.InitializationForVariable(out _outGameUIManager, _gameManager.OutGameUIManager);
        //対応するデータの作成と登録
        _runtimeDataManager.RegisterData(_id, new TitleRunTime(_data));
        InitializeManager.InitializationForVariable(out _titleRunTime, _runtimeDataManager.GetData<TitleRunTime>(_id));

        if (_titleSelects == null) InitializeManager.FailedInitialization();
        //アイテムスロットの初期化
        var length = _titleRunTime.TitleIndex;
        for (int i = 0; i < length; i++)
        {
            if (!_titleSelects[i])
            {
                InitializeManager.FailedInitialization();
                break;
            }
            _titleSelects[i].SelectSign(i == 0);
        }
        return _isInitialized;
    }

    public void PushEnter()
    {
        _outGameUIManager.TitleEnter();
    }

    void ISelectableVerticalArrowUI.SelectedCategory(int index)
    {
        _titleRunTime.SelectTitle(index);
        _preSelectIndex = _currentSelectIndex;
        _currentSelectIndex = _titleRunTime.CurrentTitleIndex;
        _titleSelects[_preSelectIndex].SelectSign(false);
        _titleSelects[_currentSelectIndex].SelectSign(true);
    }
}
