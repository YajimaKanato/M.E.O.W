using Interface;
using UnityEngine;

/// <summary>タイトルのUIに関する制御を行うクラス</summary>
public class TitleUI : UIBehaviour, ISelectableVerticalArrowUI, IEnterUI
{
    [SerializeField, Tooltip("タイトルのデータ")] TitleData _data;
    [SerializeField, Tooltip("タイトルの選択項目")] TitleSelect[] _titleSelects;
    OutGameUIManager _outGameUIManager;
    TitleRunTime _titleRunTime;
    int _currentSelectIndex = 0;
    int _preSelectIndex = 0;

    public override bool Init(GameManager manager)
    {
        //Manager関連
        _isInitialized = InitializeManager.InitializationForVariable(out _gameManager, manager);
        _isInitialized = InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);
        _isInitialized = InitializeManager.InitializationForVariable(out _outGameUIManager, _gameManager.OutGameUIManager);
        //対応するデータの作成と登録
        _runtimeDataManager.RegisterData(_id, new TitleRunTime(_data));
        _isInitialized = InitializeManager.InitializationForVariable(out _titleRunTime, _runtimeDataManager.GetData<TitleRunTime>(_id));

        if (_titleSelects == null) _isInitialized = InitializeManager.FailedInitialization();
        //アイテムスロットの初期化
        var length = _titleRunTime.TitleIndex;
        for (int i = 0; i < length; i++)
        {
            if (!_titleSelects[i])
            {
                _isInitialized = InitializeManager.FailedInitialization();
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
