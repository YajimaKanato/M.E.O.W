using Interface;
using UnityEngine;
using UnityEngine.UI;

public class GetItemUI : UIBehaviour, IEnterUI, IUIOpenAndClose
{
    [SerializeField] GetItemData _data;
    [SerializeField] Image _image;
    [SerializeField] Text _text;
    GetItemRunTime _getItemRunTime;

    public override bool Init(GameManager manager)
    {
        InitializeManager.InitializationForVariable(out _gameManager, manager);
        InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);
        InitializeManager.InitializationForVariable(out _uiManager, _gameManager.UIManager);
        _runtimeDataManager.RegisterData(_id, new GetItemRunTime(_data));
        InitializeManager.InitializationForVariable(out _getItemRunTime, _runtimeDataManager.GetData<GetItemRunTime>(_id));
        return _isInitialized;
    }


    public void Close()
    {

    }

    public void OpenSetting()
    {
        _image.sprite = _getItemRunTime.Sprite;
        _text.text = _getItemRunTime.Info;
    }

    public void PushEnter()
    {

    }
}
