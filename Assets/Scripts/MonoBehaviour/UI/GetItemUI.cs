using Interface;
using UnityEngine;
using UnityEngine.UI;

/// <summary>アイテム獲得時のUIに関する制御を行うクラス</summary>
public class GetItemUI : UIBehaviour, IEnterUI, IUIOpenAndClose
{
    [SerializeField, Tooltip("アイテム獲得に関するデータ")] GetItemData _data;
    [SerializeField, Tooltip("獲得したアイテムを表示するイメージ")] Image _image;
    [SerializeField, Tooltip("獲得したアイテムの情報を表示するテキスト")] Text _text;
    GetItemRunTime _getItemRunTime;

    public override bool Init(GameManager manager)
    {
        //Manager関連
        _isInitialized = InitializeManager.InitializationForVariable(out _gameManager, manager);
        _isInitialized = InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);
        //ランタイムデータ
        _runtimeDataManager.RegisterData(_id, new GetItemRunTime(_data));
        _isInitialized = InitializeManager.InitializationForVariable(out _getItemRunTime, _runtimeDataManager.GetData<GetItemRunTime>(_id));
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
