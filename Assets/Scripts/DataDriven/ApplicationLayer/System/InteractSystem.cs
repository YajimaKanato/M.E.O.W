using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataDriven
{
    /// <summary>インタラクトの処理を司るクラス</summary>
    public class InteractSystem
    {
        RuntimeDataRepository _repository;
        CharacterRuntimeData _target;
        Queue<EventParts> _event;
        ConditionalNextEvent _conditionalEvent;
        EnterType _enterType;

        public InteractSystem(RuntimeDataRepository repository)
        {
            _repository = repository;
            _enterType = EnterType.Interact;
        }

        /// <summary>
        /// インタラクトを開始する時に呼び出す関数
        /// </summary>
        /// <param name="character">開始するインタラクトの対象キャラクター</param>
        /// <returns>インタラクトを開始できたか</returns>
        public bool StartInteract(DataID character)
        {
            //対象がいなかったりイベントがすでに起きていたりする場合はreturn
            if (_event != null) return false;
            if (!TargetSetting(character)) return false;
            Debug.Log("Start Interact");
            //イベントを受け取る
            _event = _target.EventExecute().Events;
            //イベントの実行
            PushInteract();
            return true;
        }

        /// <summary>
        /// ターゲットとなるエンティティのランタイムデータを設定する関数
        /// </summary>
        /// <param name="character">ID</param>
        /// <returns>ランタイムデータが取得できたかどうか</returns>
        bool TargetSetting(DataID character)
        {
            switch (character)
            {
                case DataID.Dog:
                    if (!_repository.TryGetData<DogRuntimeData>(character, out var dog)) return false;
                    _target = dog;
                    return true;
                case DataID.Cat:
                    //if (!_repository.TryGetData<CatRuntimeData>(character, out var cat)) return false;
                    //_target = cat;
                    return true;
                case DataID.Mouse:
                    //if (!_repository.TryGetData<MouseRuntimeData>(character, out var mouse)) return false;
                    //_target = mouse;
                    return true;
                case DataID.Android:
                    //if (!_repository.TryGetData<AndroidRuntimeData>(character, out var android)) return false;
                    //_target = android;
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// インタラクトを進める関数
        /// </summary>
        /// <returns>最後のイベントだったかどうか</returns>
        public bool PushInteract()
        {
            if (_enterType != EnterType.Interact) GiveItem();

            //イベントを受け取っていなかったり空だったりする場合はreturn
            if (_event == null || _event.Count == 0) return false;
            //イベントを実行
            return InteractOutput(_event.Dequeue());
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="index">選択するスロット</param>
        public void HotbarSelectForKetboard(int index)
        {
            if (_enterType != EnterType.AnyItem) return;
            if (_repository.TryGetData<HotbarRuntimeData>((int)DataID.Hotbar, out var hotbar))
            {
                hotbar.SelectItemOnConversationForKeyboard(index);
            }
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void HotbarSelectForGamePad(IndexMove dir)
        {
            if (_enterType != EnterType.AnyItem) return;
            if (_repository.TryGetData<HotbarRuntimeData>((int)DataID.Hotbar, out var hotbar))
            {
                hotbar.SelectItemOnConversationForGamePad(dir);
            }
        }

        /// <summary>
        /// アイテムをあげる関数
        /// </summary>
        void GiveItem()
        {
            Debug.Log("GiveItem");
            //初期値として条件に一致しなかった時のイベントを設定
            _event = _conditionalEvent.FailedEvent.Events;
            if (_enterType == EnterType.AnyItem)
            {
                if (_repository.TryGetData<HotbarRuntimeData>((int)DataID.Hotbar, out var hotbar))
                {
                    var item = hotbar.GiveItem();
                    if (item)
                    {
                        //プレイヤーがあげるアイテムが条件に一致するか調べる
                        foreach (var condition in _conditionalEvent.NextEvent)
                        {
                            if (item.ItemName == condition.ConditionalItem.ItemName)
                            {
                                //一致したらそれに応じたイベントを返す
                                _event = condition.Event.Events;
                                break;
                            }
                        }
                    }
                }
            }
            else if (_enterType == EnterType.SpecificItem)
            {
                //条件に一致した時のイベントを取得
                var newEvent = _conditionalEvent.NextEvent[0];
                //条件のアイテムを取得
                var item = newEvent.ConditionalItem;
                if (item.ItemType == ItemRole.KeyItem)
                {
                    //キーアイテムの時はアイテムリストに対して処理
                    if (_repository.TryGetData<ItemListRuntimeData>(DataID.ItemList, out var itemList))
                    {
                        //アイテムを持っていたらイベント更新
                        if (itemList.GiveItem((KeyItemDefaultData)item)) _event = newEvent.Event.Events;
                    }
                }
                else
                {
                    //食べ物の時はホットバーに対して処理
                    if (_repository.TryGetData<HotbarRuntimeData>((int)DataID.Hotbar, out var hotbar))
                    {
                        //アイテムを持っていたらイベント更新
                        if (hotbar.GiveSpecificItem((UsableItemDefaultData)item)) _event = newEvent.Event.Events;
                    }
                }
            }
            _enterType = EnterType.Interact;
        }

        /// <summary>
        /// インタラクトを出力する関数
        /// </summary>
        /// <param name="parts">出力するインタラクト</param>
        bool InteractOutput(EventParts parts)
        {
            switch (parts.EventType)
            {
                case EventType.Talk:
                    TalkEvent((TalkEvent)parts);
                    return false;
                case EventType.GiveItem:
                    GiveItemEvent((GiveItemEvent)parts);
                    return false;
                case EventType.SpecificItem or EventType.AnyItem:
                    ConditionalNextEvent((ConditionalNextEvent)parts);
                    return false;
                case EventType.Next:
                    NextEvent();
                    return true;
                case EventType.Loop:
                    LoopEvent();
                    return true;
                default:
                    return true;
            }
        }

        /// <summary>
        /// 会話イベントを行う関数
        /// </summary>
        /// <param name="talk">イベント</param>
        void TalkEvent(TalkEvent talk)
        {
            var text = $"{talk.TalkerName.ToString()} : {talk.Text}";
            if (_repository.TryGetData<LogRuntimeData>(DataID.Log, out var log)) log.MemorizeLog(text);
            Debug.Log(text);
        }

        /// <summary>
        /// アイテムをもらうイベントを行う関数
        /// </summary>
        /// <param name="give">イベント</param>
        void GiveItemEvent(GiveItemEvent give)
        {
            var item = give.Item;
            if (item.ItemType == ItemRole.KeyItem)
            {
                if (_repository.TryGetData<ItemListRuntimeData>(DataID.ItemList, out var itemList))
                {
                    itemList.GetKeyItem((KeyItemDefaultData)item);
                }
            }
            else
            {
                if (_repository.TryGetData<HotbarRuntimeData>((int)DataID.Hotbar, out var hotbar))
                {
                    hotbar.GetItem((UsableItemDefaultData)item);
                }
            }
        }

        /// <summary>
        /// 条件によってイベントが分岐する時に呼ばれる関数
        /// </summary>
        /// <param name="conditional">イベント</param>
        void ConditionalNextEvent(ConditionalNextEvent conditional)
        {
            Debug.Log("ConditionalEvent");
            _conditionalEvent = conditional;
            if (_conditionalEvent.EventType == EventType.SpecificItem)
            {
                _enterType = EnterType.SpecificItem;
            }
            else
            {
                _enterType = EnterType.AnyItem;
            }
        }

        /// <summary>
        /// イベントを次に進める関数
        /// </summary>
        void NextEvent()
        {
            Debug.Log("NextEvent");
            _target.NextEvent();
            _target = null;
            _event = null;
        }

        /// <summary>
        /// イベントをループさせる関数
        /// </summary>
        void LoopEvent()
        {
            Debug.Log("LoopEvent");
            _target = null;
            _event = null;
        }
    }
}
