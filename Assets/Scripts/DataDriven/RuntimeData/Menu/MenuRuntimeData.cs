using UnityEngine;

namespace DataDriven
{
    /// <summary>メニューのランタイムデータ</summary>
    public class MenuRuntimeData : IRuntime
    {
        MenuDefaultData _menu;
        int _currentIndex;

        public MenuRuntimeData(MenuDefaultData menu)
        {
            _menu = menu;
            _currentIndex = menu.DefaultSelectIndex;
        }


    }
}
