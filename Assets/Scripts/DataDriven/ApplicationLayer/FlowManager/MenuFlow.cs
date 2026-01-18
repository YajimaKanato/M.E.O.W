using UnityEngine;

namespace DataDriven
{
    public class MenuFlow : FlowBase
    {
        MenuSystem _menuSystem;

        public override void Init(GameFlowManager gameFlowManager, RuntimeDataRepository repository, UnityConnector connector)
        {
            _gameFlowManager = gameFlowManager;
            _menuSystem = new MenuSystem(repository);
        }

        #region Menu
        /// <summary>
        /// メニューを開く関数
        /// </summary>
        public void MenuOpen()
        {
            if (_menuSystem.MenuOpen()) _gameFlowManager.ChangeActionMap(ActionMapName.Menu);
        }

        /// <summary>
        /// メニュー項目を選択する関数
        /// </summary>
        /// <param name="index">選択するスロット</param>
        public void MenuSelectForKeyboard(int index)
        {
            _menuSystem.MenuSelectForKeyboard(index);
        }

        /// <summary>
        /// メニュー項目を選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void MenuSelectForGamePad(IndexMove dir)
        {
            _menuSystem.MenuSelectForGamePad(dir);
        }

        /// <summary>
        /// メニュー項目内のカテゴリー選択を行う関数
        /// </summary>
        /// <param name="move">スロット選択の方向</param>
        public void MenuCategorySelect(IndexMove move)
        {
            _menuSystem.MenuCategorySelect(move);
        }

        /// <summary>
        /// 要素を変更する関数
        /// </summary>
        /// <param name="move">変更する方向</param>
        public void MenuCategoryElementSelect(IndexMove move)
        {
            _menuSystem.MenuCategoryElementSelect(move);
        }

        /// <summary>
        /// エンター入力で呼ばれる関数
        /// </summary>
        public void MenuPushEnter()
        {
            _menuSystem.PushEnter();
        }

        /// <summary>
        /// メニューを閉じる関数
        /// </summary>
        public void MenuClose()
        {
            if (_menuSystem.MenuClose()) _gameFlowManager.ChangeActionMap();
        }
        #endregion
    }
}
