using UnityEngine;
using System;

namespace DataDriven
{
    public class MenuConnector
    {
        public event Action OpenMenuAct;
        public event Action<int> ChangeCategoryAct;
        public event Action<ConfigRuntimeData> OpenConfigAct;
        public event Action<ItemCollectionRuntimeData> OpenItemCollectionAct;
        public event Action<ItemListRuntimeData> OpenItemListAct;
        public event Action<LogRuntimeData> OpenLogAct;
        public event Action<InfoRuntimeData> OpenInfoAct;
        //Config
        public event Action<IndexMove> MoveConfigCategoryAct;
        public event Action<float> ChangeBGMVolumeAct;
        public event Action<float> ChangeSEVolumeAct;
        public event Action<TextSpeed> ChangeTextSpeedAct;
        //ItemList
        public event Action<int> MoveItemListCategoryAct;
        public event Action<KeyItemDefaultData> OpenItemInfoAct;
        public event Action CloseMenuAct;

        #region Open
        public void OpenMenu()
        {
            OpenMenuAct?.Invoke();
        }

        /// <summary>
        /// 1～4キー/LRボタンに対応
        /// </summary>
        /// <param name="index"></param>
        public void ChangeCategory(int index)
        {
            ChangeCategoryAct?.Invoke(index);
        }

        public void OpenConfig(ConfigRuntimeData config)
        {
            OpenConfigAct?.Invoke(config);
        }

        public void OpenItemCollection(ItemCollectionRuntimeData itemCollection)
        {
            OpenItemCollectionAct?.Invoke(itemCollection);
        }

        public void OpenItemList(ItemListRuntimeData itemCollection)
        {
            OpenItemListAct?.Invoke(itemCollection);
        }

        public void OpenLog(LogRuntimeData log)
        {
            OpenLogAct?.Invoke(log);
        }

        public void OpenInfo(InfoRuntimeData info)
        {
            OpenInfoAct?.Invoke(info);
        }
        #endregion
        #region Config
        public void MoveConfigCategory(IndexMove move)
        {
            MoveConfigCategoryAct?.Invoke(move);
        }

        public void ChangeBGMVolume(float volume)
        {
            ChangeBGMVolumeAct?.Invoke(volume);
        }

        public void ChangeSEVolume(float volume)
        {
            ChangeSEVolumeAct?.Invoke(volume);
        }

        public void ChangeTextSpeed(TextSpeed speed)
        {
            ChangeTextSpeedAct?.Invoke(speed);
        }
        #endregion

        #region ItemList
        public void MoveItemListCategory(int index)
        {
            MoveItemListCategoryAct?.Invoke(index);
        }

        public void OpenItemInfo(KeyItemDefaultData item)
        {
            OpenItemInfoAct?.Invoke(item);
        }
        #endregion

        public void CloseMenu()
        {
            CloseMenuAct?.Invoke();
        }
    }
}
