using UnityEngine;
using Interface;

public class MenuUI : UIBehaviour, ISelectableNumberUIForKeyboard, ISelectableNumberUIForGamepad, ISelectableHorizontalArrowUI, ISelectableVerticalArrowUI, IClosableUI, IUIOpenAndClose
{
    [SerializeField, Tooltip("メニューのデータ")] MenuData _menu;
    [SerializeField, Tooltip("メニューの項目のUI")] MenuBase[] _menuSelects;
    MenuRunTime _menuRunTime;
    int _currentIndex = 0;
    int _preIndex = 0;

    public override bool Init(GameManager manager)
    {
        //Manager関連
        _isInitialized = InitializeManager.InitializationForVariable(out _gameManager, manager);
        _isInitialized = InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);
        _isInitialized = InitializeManager.InitializationForVariable(out _uiManager, _gameManager.UIManager);

        //ランタイムデータ関連
        _runtimeDataManager.RegisterData(_id, new MenuRunTime(_menu));
        _isInitialized = InitializeManager.InitializationForVariable(out _menuRunTime, _runtimeDataManager.GetData<MenuRunTime>(_id));

        return _isInitialized;
    }

    public void Close()
    {

    }

    public void OpenSetting()
    {
        //UIを開くときに最後に選択した項目を表示する
        var menuIndex = _menuRunTime.MenuIndex;
        for (int i = 0; i < menuIndex; i++)
        {
            _menuSelects[i].gameObject.SetActive(i == _currentIndex);
        }
    }

    void ISelectableNumberUIForKeyboard.SelectedCategory(int index)
    {
        _menuRunTime.SelectMenuForKeyboard(index);
        SelectUpdate();
    }

    void ISelectableNumberUIForGamepad.SelectedCategory(int index)
    {
        _menuRunTime.SelectMenuForGamepad(index);
        SelectUpdate();
    }
    void ISelectableHorizontalArrowUI.SelectedCategory(int index)
    {
        //現在選択中のメニューの項目が矢印選択の入力を受け付けるか
        if (_menuSelects[_currentIndex] is ISelectableHorizontalArrowUI) ((ISelectableHorizontalArrowUI)_menuSelects[_currentIndex]).SelectedCategory(index);
    }

    void ISelectableVerticalArrowUI.SelectedCategory(int index)
    {
        //現在選択中のメニューの項目が矢印選択の入力を受け付けるか
        if (_menuSelects[_currentIndex] is ISelectableVerticalArrowUI) ((ISelectableVerticalArrowUI)_menuSelects[_currentIndex]).SelectedCategory(index);
    }

    /// <summary>
    /// スロット選択中を更新する関数
    /// </summary>
    void SelectUpdate()
    {
        _preIndex = _currentIndex;
        _currentIndex = _menuRunTime.CurrentMenuIndex;
        _menuSelects[_preIndex].gameObject.SetActive(false);
        _menuSelects[_currentIndex].gameObject.SetActive(true);
    }
}
