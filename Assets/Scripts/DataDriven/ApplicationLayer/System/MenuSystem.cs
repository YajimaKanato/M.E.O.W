using UnityEngine;

namespace DataDriven
{
    /// <summary>メニュー操作の処理を司るクラス</summary>
    public class MenuSystem
    {
        RuntimeDataRepository _repository;
        MenuRuntimeData _menuRuntime;

        public MenuSystem(RuntimeDataRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// メニューを開く関数
        /// </summary>
        /// <returns>メニューを開けたかどうか</returns>
        public bool MenuOpen()
        {
            if (_menuRuntime != null) return false;
            Debug.Log("MenuOpen");
            if (_repository.TryGetData<MenuRuntimeData>((int)EntityID.Menu, out var menu))
            {
                _menuRuntime = menu;
                Debug.Log($"CurrentMenuCategory => {_menuRuntime.GetMenuCategory().MenuType}");
                return true;
            }
            return false;
        }

        /// <summary>
        /// メニュー項目を選択する関数
        /// </summary>
        /// <param name="index">選択するスロット</param>
        public void MenuSelectForKeyboard(int index)
        {
            if (_menuRuntime != null)
            {
                _menuRuntime.ChangeTypeForKeyboard(index);
                Debug.Log($"CurrentMenuCategory => {_menuRuntime.GetMenuCategory().MenuType}");
            }
        }

        /// <summary>
        /// メニュー項目を選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void MenuSelectForGamePad(int dir)
        {
            if (_menuRuntime != null)
            {
                _menuRuntime.ChangeTypeForGamePad(dir);
                Debug.Log($"CurrentMenuCategory => {_menuRuntime.GetMenuCategory().MenuType}");
            }
        }

        /// <summary>
        /// メニューを閉じる関数
        /// </summary>
        /// <returns>メニューを閉じれたかどうか</returns>
        public bool MenuClose()
        {
            if (_menuRuntime == null) return false;
            if (_menuRuntime != null)
            {
                Debug.Log("MenuClose");
                _menuRuntime = null;
                return true;
            }
            return false;
        }
    }
}
