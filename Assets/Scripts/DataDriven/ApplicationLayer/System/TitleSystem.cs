using UnityEngine;

namespace DataDriven
{
    /// <summary>タイトル操作の処理を司るクラス</summary>
    public class TitleSystem
    {
        RuntimeDataRepository _repository;

        public TitleSystem(RuntimeDataRepository repository)
        {
            _repository = repository;
            if (!_repository.TryGetData<TitleRuntimeData>(DataID.Title, out var title)) return;
            if (!_repository.TryGetData<MenuRuntimeData>(DataID.Menu, out var menuRuntime)) return;
        }

        /// <summary>
        /// タイトルのカテゴリーを開く関数
        /// </summary>
        /// <returns>カテゴリーを開けたかどうか</returns>
        public bool OpenCategory()
        {
            if (!_repository.TryGetData<TitleRuntimeData>(DataID.Title, out var title)) return false;
            Debug.Log($"OpenCategory => {title.GetTitleCategory()}");
            switch (title.GetTitleCategory())
            {
                case TitleCategory.GameStart:
                    //シーン遷移
                    break;
                case TitleCategory.Ending:
                    OpenEnding();
                    break;
                case TitleCategory.Menu:
                    OpenMenu();
                    break;
                case TitleCategory.Credit:
                    OpenCredit();
                    break;
                case TitleCategory.Reset:
                    ResetSelect();
                    break;
                default:
                    return false;
            }
            return true;
        }

        void OpenEnding()
        {
            Debug.Log("OpenEndingList");
        }

        void OpenMenu()
        {
            Debug.Log("OpenMenu");
        }

        void OpenCredit()
        {
            Debug.Log("OpenCredit");
        }

        void ResetSelect()
        {
            Debug.Log("Do you want to clear Data?");
        }

        /// <summary>
        /// カテゴリーを選択する関数
        /// </summary>
        /// <param name="move">選択するスロットをずらす方向</param>
        public void SelectCategory(IndexMove move)
        {
            if (!_repository.TryGetData<TitleRuntimeData>(DataID.Title, out var title)) return;
            title.ChangeType(move);
            Debug.Log($"CurrentTitleCategory => {title.GetTitleCategory()}");
        }

        /// <summary>
        /// タイトルのカテゴリーを閉じる関数
        /// </summary>
        /// <returns>カテゴリーを閉じれたかどうか</returns>
        public bool CloseCategory()
        {
            if (!_repository.TryGetData<TitleRuntimeData>(DataID.Title, out var title)) return false;
            Debug.Log("TitleCategoryClose");
            return true;
        }

        /// <summary>
        /// メニュー項目を選択する関数
        /// </summary>
        /// <param name="index">選択するスロット</param>
        public void MenuSelectForKeyboard(int index)
        {
            if (!_repository.TryGetData<TitleRuntimeData>(DataID.Title, out var title)) return;
            if (!_repository.TryGetData<MenuRuntimeData>(DataID.Menu, out var menuRuntime)) return;
            if (title.GetTitleCategory() != TitleCategory.Menu) return;
            if (menuRuntime != null)
            {
                menuRuntime.ChangeTypeForKeyboard(index);
                Debug.Log($"CurrentMenuCategory => {menuRuntime.GetMenuCategory()}");
            }
        }

        /// <summary>
        /// メニュー項目を選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void MenuSelectForGamePad(IndexMove dir)
        {
            if (!_repository.TryGetData<TitleRuntimeData>(DataID.Title, out var title)) return;
            if (!_repository.TryGetData<MenuRuntimeData>(DataID.Menu, out var menuRuntime)) return;
            if (title.GetTitleCategory() != TitleCategory.Menu) return;
            if (menuRuntime != null)
            {
                menuRuntime.ChangeTypeForGamePad(dir);
                Debug.Log($"CurrentMenuCategory => {menuRuntime.GetMenuCategory()}");
            }
        }

        /// <summary>
        /// メニュー項目内のカテゴリー選択を行う関数
        /// </summary>
        /// <param name="move">スロット選択の方向</param>
        public void MenuCategorySelect(IndexMove move)
        {
            if (!_repository.TryGetData<TitleRuntimeData>(DataID.Title, out var title)) return;
            if (!_repository.TryGetData<MenuRuntimeData>(DataID.Menu, out var menuRuntime)) return;
            if (title.GetTitleCategory() != TitleCategory.Menu) return;
            switch (menuRuntime.GetMenuCategory())
            {
                case MenuCategory.Config:
                    ConfigSelect(move);
                    break;
                case MenuCategory.ItemCollection:
                    ItemCollectionSelect(move);
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
            if (!_repository.TryGetData<TitleRuntimeData>(DataID.Title, out var title)) return;
            if (!_repository.TryGetData<MenuRuntimeData>(DataID.Menu, out var menuRuntime)) return;
            if (title.GetTitleCategory() != TitleCategory.Menu) return;
            switch (menuRuntime.GetMenuCategory())
            {
                case MenuCategory.Config:
                    ConfigElementSelect(move);
                    break;
                case MenuCategory.ItemCollection:
                    ItemCollectionElementSelect(move);
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
        #endregion
        #endregion

        /// <summary>
        /// エンター入力をした時に呼ばれる関数
        /// </summary>
        public void PushEnter()
        {
            if (!_repository.TryGetData<TitleRuntimeData>(DataID.Title, out var title)) return;
            if (!_repository.TryGetData<MenuRuntimeData>(DataID.Menu, out var menuRuntime)) return;
            if (title.GetTitleCategory() == TitleCategory.Menu)
            {
                switch (menuRuntime.GetMenuCategory())
                {
                    case MenuCategory.Config:
                        ConfigEnter();
                        break;
                    case MenuCategory.ItemCollection:
                        ItemCollectionEnter();
                        break;
                    default:
                        break;
                }
            }
            else if (title.GetTitleCategory() == TitleCategory.Reset)
            {

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
    }
}
