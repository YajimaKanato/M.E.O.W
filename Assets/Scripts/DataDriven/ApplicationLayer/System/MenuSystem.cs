using System;
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
            if (_repository.TryGetData<MenuRuntimeData>(DataID.Menu, out var menu))
            {
                _menuRuntime = menu;
                Debug.Log($"CurrentMenuCategory => {_menuRuntime.GetMenuCategory()}");
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
                Debug.Log($"CurrentMenuCategory => {_menuRuntime.GetMenuCategory()}");
            }
        }

        /// <summary>
        /// メニュー項目を選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void MenuSelectForGamePad(IndexMove dir)
        {
            if (_menuRuntime != null)
            {
                _menuRuntime.ChangeTypeForGamePad(dir);
                Debug.Log($"CurrentMenuCategory => {_menuRuntime.GetMenuCategory()}");
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

        /// <summary>
        /// メニュー項目内のカテゴリー選択を行う関数
        /// </summary>
        /// <param name="move">スロット選択の方向</param>
        public void SelectVertical(SlotMoveVertical move)
        {
            switch (_menuRuntime.GetMenuCategory())
            {
                case MenuType.Config:
                    SelectCategory<ConfigRuntimeData>(DataID.Config, move);
                    break;
                case MenuType.ItemList:
                    SelectCategory<ItemCollectionRuntimeData>(DataID.ItemCollection, move);
                    break;
                case MenuType.Log:
                    SelectCategory<LogRuntimeData>(DataID.Log, move);
                    break;
                case MenuType.Info:
                    SelectCategory<InfoRuntimeData>(DataID.Info, move);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// メニュー項目内のカテゴリー選択を行う関数
        /// </summary>
        /// <typeparam name="T">カテゴリー選択を行う型/typeparam>
        /// <param name="id">ID</param>
        /// <param name="move">スロット選択の方向</param>
        void SelectCategory<T>(DataID id, SlotMoveVertical move) where T : MenuCategoryRuntime, IRuntime
        {
            if (_repository.TryGetData<T>(id, out var data))
            {
                data.SelectCategory(move);
                Debug.Log($"{typeof(T)} : SelectCategory");
            }
        }

        public void SelectHorizontal(SlotMoveHorizontal move)
        {

        }

        /// <summary>
        /// エンター入力をした時に呼ばれる関数
        /// </summary>
        public void PushEnter()
        {
            switch (_menuRuntime.GetMenuCategory())
            {
                case MenuType.Config:
                    ConfigEnter();
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
                    case ConfigType.TextSpeed:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
