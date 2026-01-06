using UnityEngine;

namespace DataDriven
{
    public class MenuRuntimeFactory
    {
        public MenuRuntimeData MenuCreate(MenuDefaultData menu, ItemListRuntimeData itemList)
        {
            return new MenuRuntimeData(menu, itemList);
        }
    }
}
