using UnityEngine;

namespace DataDriven
{
    public class OutGameFlow : FlowBase
    {
        TitleSystem _titleSystem;

        public override void Init(GameFlowManager gameFlowManager, RuntimeDataRepository repository, UnityConnector connector)
        {
            _gameFlowManager = gameFlowManager;
            _titleSystem = new TitleSystem(repository);
        }

        #region Title
        /// <summary>
        /// タイトルのカテゴリーを開く関数
        /// </summary>
        /// <returns>カテゴリーを開けたかどうか</returns>
        public void OpenCategory()
        {
            if (_titleSystem.OpenCategory()) _gameFlowManager.ChangeActionMap(ActionMapName.OutGameCategory);
        }

        /// <summary>
        /// カテゴリーを選択する関数
        /// </summary>
        /// <param name="move">選択するスロットをずらす方向</param>
        public void SelectCategory(IndexMove move)
        {
            _titleSystem.SelectCategory(move);
        }
        #endregion

        #region TitleCategory
        /// <summary>
        /// タイトルのカテゴリーを閉じる関数
        /// </summary>
        /// <returns>カテゴリーを閉じれたかどうか</returns>
        public void CloseCategory()
        {
            if (_titleSystem.CloseCategory()) _gameFlowManager.ChangeActionMap();
        }

        /// <summary>
        /// メニュー項目を選択する関数
        /// </summary>
        /// <param name="index">選択するスロット</param>
        public void TitleSelectForKeyboard(int index)
        {
            _titleSystem.MenuSelectForKeyboard(index);
        }

        /// <summary>
        /// メニュー項目を選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void TitleSelectForGamePad(IndexMove dir)
        {
            _titleSystem.MenuSelectForGamePad(dir);
        }

        /// <summary>
        /// メニュー項目内のカテゴリー選択を行う関数
        /// </summary>
        /// <param name="move">スロット選択の方向</param>
        public void TitleCategorySelect(IndexMove move)
        {
            _titleSystem.MenuCategorySelect(move);
        }

        /// <summary>
        /// 要素を変更する関数
        /// </summary>
        /// <param name="move">変更する方向</param>
        public void TitleCategoryElementSelect(IndexMove move)
        {
            _titleSystem.MenuCategoryElementSelect(move);
        }

        /// <summary>
        /// エンター入力で呼ばれる関数
        /// </summary>
        public void TitlePushEnter()
        {
            _titleSystem.PushEnter();
        }
        #endregion
    }
}
