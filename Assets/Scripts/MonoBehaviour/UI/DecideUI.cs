using Interface;
using UnityEngine;

/// <summary>意思決定をするUIに関する制御を行うクラス</summary>
public class DecideUI : UIBehaviour, ISelectableHorizontalArrowUI, IUIOpenAndClose, IEnterUI
{
    [SerializeField] DecideData _decide;
    [SerializeField, Tooltip("YESの意思決定UI")] DecideSelect _yes;
    [SerializeField, Tooltip("NOの意思決定UI")] DecideSelect _no;
    DecideRuntime _decideRuntime;

    public override bool Init(GameManager manager)
    {
        //Manager関連
        _isInitialized = InitializeManager.InitializationForVariable(out _gameManager, manager);
        _isInitialized = InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);

        //ランタイムデータ関連
        _runtimeDataManager.RegisterData(_id, new DecideRuntime(_decide));
        _isInitialized = InitializeManager.InitializationForVariable(out _decideRuntime, _runtimeDataManager.GetData<DecideRuntime>(_id));
        return _isInitialized;
    }

    public void Close()
    {

    }

    public void OpenSetting()
    {
        SlotUpdate();
    }

    public void PushEnter()
    {

    }

    void ISelectableHorizontalArrowUI.SelectedCategory(int index)
    {
        _decideRuntime.SelectDecide(index);
        SlotUpdate();
    }

    void SlotUpdate()
    {
        _yes.SelectSign(_decideRuntime.CurrentIndex == 0, _decideRuntime.DecideType ? _decideRuntime.Item.Sprite : null);
        _no.SelectSign(_decideRuntime.CurrentIndex == 1);
    }
}
