using UnityEngine;

namespace DataDriven
{
    /// <summary>メニューのランタイムデータ</summary>
    public class MenuRuntimeData : IRuntime
    {
        MenuDefaultData _menu;
        int _currentType;
        MenuType[] _menuTypes;

        public int CurrentType => _currentType;

        public MenuRuntimeData(MenuDefaultData menu)
        {
            _menu = menu;
            _currentType = (int)menu.DefaultSelectIndex;
            _menuTypes = _menu.Categories;
        }

        /// <summary>
        /// 現在選択中のメニュー項目を返す関数
        /// </summary>
        /// <returns>現在選択中のメニュー</returns>
        public MenuType GetMenuCategory()
        {
            return _menuTypes[_currentType];
        }

        /// <summary>
        /// メニュー項目を選択する関数
        /// </summary>
        /// <param name="index">選択するスロット</param>
        public void ChangeTypeForKeyboard(int index)
        {
            if (index < 0 || _menuTypes.Length - 1 < index) return;
            _currentType = index;
            Debug.Log($"Select => {_currentType} : {_menuTypes[_currentType]}");
        }

        /// <summary>
        /// メニュー項目を選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void ChangeTypeForGamePad(IndexMove dir)
        {
            _currentType += (int)dir;
            if (_currentType > _menuTypes.Length - 1)
            {
                _currentType = 0;
            }
            else if (_currentType < 0)
            {
                _currentType = _menuTypes.Length - 1;
            }
            Debug.Log($"Select => {_currentType} : {_menuTypes[_currentType]}");
        }
    }
}
