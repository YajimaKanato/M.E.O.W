using UnityEngine;

namespace DataDriven
{
    /// <summary>メニュー操作の処理を司るクラス</summary>
    public class MenuSystem
    {
        RuntimeDataRepository _repository;

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
            if (!_repository.TryGetData<MenuRuntimeData>(DataID.Menu, out var menu)) return false;
            Debug.Log("MenuOpen");
            CategoryOpenSetting();
            return true;
        }

        /// <summary>
        /// メニュー項目を選択する関数
        /// </summary>
        /// <param name="index">選択するスロット</param>
        public void MenuSelectForKeyboard(int index)
        {
            if (!_repository.TryGetData<MenuRuntimeData>(DataID.Menu, out var menu)) return;
            menu.ChangeTypeForKeyboard(index);
            CategoryOpenSetting();
        }

        /// <summary>
        /// メニュー項目を選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void MenuSelectForGamePad(IndexMove dir)
        {
            if (!_repository.TryGetData<MenuRuntimeData>(DataID.Menu, out var menu)) return;
            menu.ChangeTypeForGamePad(dir);
            CategoryOpenSetting();
        }

        void CategoryOpenSetting()
        {
            if (!_repository.TryGetData<MenuRuntimeData>(DataID.Menu, out var menu)) return;
            switch (menu.GetMenuCategory())
            {
                case MenuCategory.Config:
                    break;
                case MenuCategory.ItemCollection:
                    break;
                case MenuCategory.ItemList:
                    break;
                case MenuCategory.Log:
                    break;
                case MenuCategory.Info:
                    break;
                default:
                    break;
            }
            Debug.Log($"CurrentMenuCategory => {menu.GetMenuCategory()}");
        }

        /// <summary>
        /// メニューを閉じる関数
        /// </summary>
        /// <returns>メニューを閉じれたかどうか</returns>
        public bool MenuClose()
        {
            if (!_repository.TryGetData<MenuRuntimeData>(DataID.Menu, out var menu)) return false;
            Debug.Log("MenuClose");
            return true;
        }

        /// <summary>
        /// メニュー項目内のカテゴリー選択を行う関数
        /// </summary>
        /// <param name="move">スロット選択の方向</param>
        public void MenuCategorySelect(IndexMove move)
        {
            if (!_repository.TryGetData<MenuRuntimeData>(DataID.Menu, out var menu)) return;
            switch (menu.GetMenuCategory())
            {
                case MenuCategory.Config:
                    ConfigSelect(move);
                    break;
                case MenuCategory.ItemCollection:
                    ItemCollectionSelect(move);
                    break;
                case MenuCategory.ItemList:
                    ItemListSelect(move);
                    break;
                case MenuCategory.Log:
                    LogSelect(move);
                    break;
                case MenuCategory.Info:
                    InfoSelect(move);
                    break;
                default:
                    break;
            }
        }

        #region MenuCategory
        void ConfigSelect(IndexMove move)
        {
            if (_repository.TryGetData<ConfigRuntimeData>(DataID.Config, out var data))
            {
                data.SelectCategory(move);
                Debug.Log($"SelectCategory => {data.Category}");
            }
        }

        void ItemCollectionSelect(IndexMove move)
        {
            if (_repository.TryGetData<ItemCollectionRuntimeData>(DataID.ItemCollection, out var data))
            {
                ((IVerticalArrowInput)data).SelectCategory(move);
                Debug.Log($"SelectCategory => {data.CurrentIndex + 1}");
            }
        }

        void ItemListSelect(IndexMove move)
        {
            if (_repository.TryGetData<ItemListRuntimeData>(DataID.ItemList, out var data))
            {
                ((IVerticalArrowInput)data).SelectCategory(move);
                Debug.Log($"SelectCategory => {data.CurrentIndex + 1}");
            }
        }

        void LogSelect(IndexMove move)
        {
            if (_repository.TryGetData<LogRuntimeData>(DataID.Log, out var data))
            {
                data.SelectCategory(move);
                Debug.Log($"Log : {data.CurrentIndex + 1} => {data.GetLog()}");
            }
        }

        void InfoSelect(IndexMove move)
        {
            if (_repository.TryGetData<InfoRuntimeData>(DataID.Info, out var data))
            {
                data.SelectCategory(move);
                Debug.Log($"SelectCategory => {typeof(InfoRuntimeData)}");
            }
        }
        #endregion

        /// <summary>
        /// 要素を変更する関数
        /// </summary>
        /// <param name="move">変更する方向</param>
        public void MenuCategoryElementSelect(IndexMove move)
        {
            if (!_repository.TryGetData<MenuRuntimeData>(DataID.Menu, out var menu)) return;
            switch (menu.GetMenuCategory())
            {
                case MenuCategory.Config:
                    ConfigElementSelect(move);
                    break;
                case MenuCategory.ItemCollection:
                    ItemCollectionElementSelect(move);
                    break;
                case MenuCategory.ItemList:
                    ItemListElementSelect(move);
                    break;
                case MenuCategory.Log:
                    break;
                case MenuCategory.Info:
                    break;
                default:
                    break;
            }
        }

        #region MenuElement
        /// <summary>
        /// 設定画面で要素を変更する関数
        /// </summary>
        /// <param name="move">変更する方向</param>
        void ConfigElementSelect(IndexMove move)
        {
            if (!_repository.TryGetData<ConfigRuntimeData>(DataID.Config, out var data)) return;
            switch (data.Category)
            {
                case ConfigType.BGM:
                    BGMChange(move);
                    break;
                case ConfigType.SE:
                    SEChange(move);
                    break;
                case ConfigType.TextConfig:
                    TextSpeedChange(move);
                    break;
                default:
                    break;
            }
        }

        #region Config
        void BGMChange(IndexMove move)
        {
            if (_repository.TryGetData<BGMRuntimeData>(DataID.BGM, out var bgm))
            {
                bgm.ChangeElement(move);
                Debug.Log($"BGMVolume => {bgm.CurrentVolume}");
            }
        }

        void SEChange(IndexMove move)
        {
            if (_repository.TryGetData<SERuntimeData>(DataID.SE, out var se))
            {
                se.ChangeElement(move);
                Debug.Log($"SEVolume => {se.CurrentVolume}");
            }
        }

        void TextSpeedChange(IndexMove move)
        {
            if (_repository.TryGetData<TextConfigRuntimeData>(DataID.Text, out var text))
            {
                text.ChangeElement(move);
                Debug.Log($"TextSpeed => {text.CurrentTextSpeed}");
            }
        }
        #endregion
        #region ItemCollection
        /// <summary>
        /// コレクト画面の横移動
        /// </summary>
        /// <param name="move">変更する方向</param>
        void ItemCollectionElementSelect(IndexMove move)
        {
            if (_repository.TryGetData<ItemCollectionRuntimeData>(DataID.ItemCollection, out var data))
            {
                ((IHorizontalArrowInput)data).SelectCategory(move);
                Debug.Log($"SelectCategory => {data.CurrentIndex + 1}");
            }
        }

        void ItemListElementSelect(IndexMove move)
        {
            if (_repository.TryGetData<ItemListRuntimeData>(DataID.ItemList, out var data))
            {
                ((IHorizontalArrowInput)data).SelectCategory(move);
                Debug.Log($"SelectCategory => {data.CurrentIndex + 1}");
            }
        }
        #endregion
        #endregion

        /// <summary>
        /// エンター入力をした時に呼ばれる関数
        /// </summary>
        public void PushEnter()
        {
            if (!_repository.TryGetData<MenuRuntimeData>(DataID.Menu, out var menu)) return;
            switch (menu.GetMenuCategory())
            {
                case MenuCategory.Config:
                    ConfigEnter();
                    break;
                case MenuCategory.ItemCollection:
                    ItemCollectionEnter();
                    break;
                case MenuCategory.ItemList:
                    ItemListEnter();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 設定画面の時のエンター処理を行う関数
        /// </summary>
        void ConfigEnter()
        {
            if (_repository.TryGetData<ConfigRuntimeData>(DataID.Config, out var config))
            {
                switch (config.Category)
                {
                    case ConfigType.TextConfig:
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// コレクト画面のエンター処理を行う関数
        /// </summary>
        void ItemCollectionEnter()
        {
            if (_repository.TryGetData<ItemCollectionRuntimeData>(DataID.ItemCollection, out var itemCollection))
            {
                var item = itemCollection.GetItemInfo();
                if (item == null) return;
                var itemInfo = item.ItemInfo;
                if (itemInfo)
                {
                    Debug.Log($"{itemInfo.Name} : {(item.IsObtained ? "獲得済み" : "未獲得")}\n{itemInfo.ItemInfo}");
                }
                else
                {
                    Debug.Log("null");
                }
            }
        }

        void ItemListEnter()
        {
            if (_repository.TryGetData<ItemListRuntimeData>(DataID.ItemList, out var itemCollection))
            {
                var item = itemCollection.GetItemInfo();
                if (item == null) return;
                var itemInfo = item.ItemInfo;
                if (itemInfo)
                {
                    Debug.Log($"{itemInfo.Name} : {(item.IsObtained ? "獲得済み" : "未獲得")}\n{itemInfo.ItemInfo}");
                }
                else
                {
                    Debug.Log("null");
                }
            }
        }
    }
}
