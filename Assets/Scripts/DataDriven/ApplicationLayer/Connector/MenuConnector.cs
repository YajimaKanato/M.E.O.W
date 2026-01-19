using UnityEngine;
using System;

namespace DataDriven
{
    public class MenuConnector
    {
        public event Action OpenMenuAct;
        public event Action<int> ChangeCategoryAct;
        public event Action CloseMenuAct;
        //Config
        public event Action<ConfigRuntimeData, BGMRuntimeData, SERuntimeData, TextConfigRuntimeData> OpenConfigAct;
        public event Action<int> MoveConfigCategoryAct;
        public event Action<float> ChangeBGMVolumeAct;
        public event Action<float> ChangeSEVolumeAct;
        public event Action<TextSpeed> ChangeTextSpeedAct;
        //ItemList
        public event Action<ItemListRuntimeData> OpenItemListAct;
        public event Action<int> MoveItemListCategoryAct;
        public event Action<KeyItemState> OpenItemInfoAct;
        //ItemCollection
        public event Action<ItemCollectionRuntimeData> OpenItemCollectionAct;
        public event Action<int> MoveItemCollectionCategoryAct;
        public event Action<ItemObtainInfo> OpenItemObtainInfoAct;
        //Log
        public event Action<LogRuntimeData> OpenLogAct;
        public event Action<int> MoveLogCategoryAct;
        //Info
        public event Action<InfoRuntimeData> OpenInfoAct;

        #region Open
        /// <summary>
        /// メニューを開く時に呼ばれる関数
        /// </summary>
        public void OpenMenu()
        {
            OpenMenuAct?.Invoke();
        }

        /// <summary>
        /// メニュー項目を切り替える時に呼ばれる関数
        /// 1～4キー/LRボタンに対応
        /// </summary>
        /// <param name="index">切り替えた後のインデックス</param>
        public void ChangeCategory(int index)
        {
            ChangeCategoryAct?.Invoke(index);
        }

        /// <summary>
        /// 設定画面が開かれたときに呼ばれる関数
        /// </summary>
        /// <param name="config">設定画面の状態</param>
        /// <param name="bgm">BGMの設定状態</param>
        /// <param name="se">SEの設定状態</param>
        /// <param name="text">テキスト表示の設定状態</param>
        public void OpenConfig(ConfigRuntimeData config, BGMRuntimeData bgm, SERuntimeData se, TextConfigRuntimeData text)
        {
            OpenConfigAct?.Invoke(config, bgm, se, text);
        }

        /// <summary>
        /// タイトルのアイテムリストが開かれたときに呼ばれる関数
        /// </summary>
        /// <param name="itemCollection">アイテムリストの状態</param>
        public void OpenItemCollection(ItemCollectionRuntimeData itemCollection)
        {
            OpenItemCollectionAct?.Invoke(itemCollection);
        }

        /// <summary>
        /// インゲームのアイテムリストが開かれたときに呼ばれる関数
        /// </summary>
        /// <param name="itemCollection">アイテムリストの状態</param>
        public void OpenItemList(ItemListRuntimeData itemCollection)
        {
            OpenItemListAct?.Invoke(itemCollection);
        }

        /// <summary>
        /// 会話ログが開かれたときに呼ばれる関数
        /// </summary>
        /// <param name="log">会話ログの状態</param>
        public void OpenLog(LogRuntimeData log)
        {
            OpenLogAct?.Invoke(log);
        }

        /// <summary>
        /// 操作説明が開かれたときに呼ばれる関数
        /// </summary>
        /// <param name="info">操作説明の状態</param>
        public void OpenInfo(InfoRuntimeData info)
        {
            OpenInfoAct?.Invoke(info);
        }
        #endregion
        #region Config
        /// <summary>
        /// 設定画面の設定項目を切り替える時に呼ばれる関数
        /// </summary>
        /// <param name="index">現在選択中の項目</param>
        public void MoveConfigCategory(int index)
        {
            MoveConfigCategoryAct?.Invoke(index);
        }

        /// <summary>
        /// BGMの大きさを変更するときに呼ばれる関数
        /// </summary>
        /// <param name="volume">変更後の大きさ</param>
        public void ChangeBGMVolume(float volume)
        {
            ChangeBGMVolumeAct?.Invoke(volume);
        }

        /// <summary>
        /// SEの大きさを変更するときに呼ばれる関数
        /// </summary>
        /// <param name="volume">変更後の大きさ</param>
        public void ChangeSEVolume(float volume)
        {
            ChangeSEVolumeAct?.Invoke(volume);
        }

        /// <summary>
        /// テキスト表示速度を変更するときに呼ばれる関数
        /// </summary>
        /// <param name="speed">変更後の速度</param>
        public void ChangeTextSpeed(TextSpeed speed)
        {
            ChangeTextSpeedAct?.Invoke(speed);
        }
        #endregion
        #region ItemCollection
        /// <summary>
        /// タイトルのアイテムリストの選択アイテムを切り替える時に呼ばれる関数
        /// </summary>
        /// <param name="index">選択中のインデックス</param>
        public void MoveItemCollectionCategory(int index)
        {
            MoveItemCollectionCategoryAct?.Invoke(index);
        }

        /// <summary>
        /// アイテムの情報を開くときに呼ばれる関数
        /// </summary>
        /// <param name="item">アイテムの情報</param>
        public void OpenItemObtainInfo(ItemObtainInfo item)
        {
            OpenItemObtainInfoAct?.Invoke(item);
        }
        #endregion
        #region ItemList
        /// <summary>
        /// インゲームのアイテムリストの選択アイテムを切り替える時に呼ばれる関数
        /// </summary>
        /// <param name="index">選択中のインデックス</param>
        public void MoveItemListCategory(int index)
        {
            MoveItemListCategoryAct?.Invoke(index);
        }

        /// <summary>
        /// アイテムの情報を開くときに呼ばれる関数
        /// </summary>
        /// <param name="item">アイテムの情報</param>
        public void OpenItemInfo(KeyItemState item)
        {
            OpenItemInfoAct?.Invoke(item);
        }
        #endregion
        #region Log
        /// <summary>
        /// 選択中の会話ログを切り替える時に呼ばれる関数
        /// </summary>
        /// <param name="index">選択中のログのインデックス</param>
        public void MoveLogCategory(int index)
        {
            MoveLogCategoryAct?.Invoke(index);
        }
        #endregion
        /// <summary>
        /// メニューを閉じる時に呼ばれる関数
        /// </summary>
        public void CloseMenu()
        {
            CloseMenuAct?.Invoke();
        }
    }
}
