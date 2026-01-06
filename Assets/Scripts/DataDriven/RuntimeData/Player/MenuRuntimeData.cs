using UnityEngine;

namespace DataDriven
{
    /// <summary>メニューのランタイムデータ</summary>
    public class MenuRuntimeData : IRuntime
    {
        MenuDefaultData _menu;
        ItemListRuntimeData _itemList;
        int _currentIndex;

        public MenuRuntimeData(MenuDefaultData menu, ItemListRuntimeData itemList)
        {
            _menu = menu;
            _itemList = itemList;
            _currentIndex = menu.DefaultSelectIndex;
        }


    }
}
