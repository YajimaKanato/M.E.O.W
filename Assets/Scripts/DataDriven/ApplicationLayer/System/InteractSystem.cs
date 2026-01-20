using System.Collections.Generic;
using UnityEngine;

namespace DataDriven
{
    /// <summary>インタラクトの処理を司るクラス</summary>
    public class InteractSystem
    {
        GameFlowManager _gameFlowManager;
        RuntimeDataRepository _repository;
        CharacterRuntimeData _target;
        Queue<EventParts> _event;
        ConditionalNextEvent _conditionalEvent;
        EnterType _enterType;
        bool _decide;

        public InteractSystem(GameFlowManager gameFlowManager, RuntimeDataRepository repository)
        {
            _gameFlowManager = gameFlowManager;
            _repository = repository;
            _enterType = EnterType.Interact;
            _decide = true;
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
        /// 意思決定をする関数
        /// </summary>
        /// <param name="decide"></param>
        public void Decide(bool decide)
        {
            if (_enterType != EnterType.AnyItem && _enterType != EnterType.Decide) return;
            _decide = decide;
            Debug.Log(_decide ? "はい" : "いいえ");
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="index">選択するスロット</param>
        public void HotbarSelectForKetboard(int index)
        {
            if (_enterType != EnterType.AnyItem) return;
            if (_repository.TryGetData<HotbarRuntimeData>(DataID.Hotbar, out var hotbar))
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
            if (_repository.TryGetData<HotbarRuntimeData>(DataID.Hotbar, out var hotbar))
            {
                hotbar.SelectItemOnConversationForGamePad(dir);
            }
        }

        /// <summary>
        /// アイテムをあげる関数
        /// </summary>
        void GiveItem()
        {
            //初期値として条件に一致しなかった時のイベントを設定
            _event = _conditionalEvent.FailedEvent.Events;
            if (_enterType == EnterType.AnyItem)
            {
                if (_repository.TryGetData<HotbarRuntimeData>(DataID.Hotbar, out var hotbar))
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
            else if (_enterType == EnterType.Decide && _decide)
            {
                var newEvent = _conditionalEvent.NextEvent[0];
                //アイテムをあげるイベントの処理
                if (_conditionalEvent.EventType == EventType.SpecificItem)
                {
                    //条件に一致した時のイベントを取得
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
                        if (_repository.TryGetData<HotbarRuntimeData>(DataID.Hotbar, out var hotbar))
                        {
                            //アイテムを持っていたらイベント更新
                            if (hotbar.GiveSpecificItem((UsableItemDefaultData)item)) _event = newEvent.Event.Events;
                        }
                    }
                }
                else if (_conditionalEvent.EventType == EventType.Decide)
                {
                    _event = newEvent.Event.Events;
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
                case EventType.SpecificItem or EventType.AnyItem or EventType.Decide:
                    ConditionalNextEvent((ConditionalNextEvent)parts);
                    return false;
                case EventType.Next:
                    NextEvent();
                    return true;
                case EventType.Loop:
                    LoopEvent();
                    return true;
                case EventType.SceneTransition:
                    SceneTransition((SceneTransitionEvent)parts);
                    return true;
                default:
                    return true;
            }
        }

        #region Interact
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
                    if (!itemList.GetKeyItem((KeyItemDefaultData)item)) return;
                }
                if (_repository.TryGetData<ItemCollectionRuntimeData>(DataID.ItemCollection, out var itemCollection))
                {
                    if (!itemCollection.GetKeyItem((KeyItemDefaultData)item)) return;
                }
            }
            else
            {
                if (_repository.TryGetData<HotbarRuntimeData>(DataID.Hotbar, out var hotbar))
                {
                    if (!hotbar.GetItem((UsableItemDefaultData)item)) return;
                }
            }
            Debug.Log($"Get => {item.Name}");
        }

        /// <summary>
        /// 条件によってイベントが分岐する時に呼ばれる関数
        /// </summary>
        /// <param name="conditional">イベント</param>
        void ConditionalNextEvent(ConditionalNextEvent conditional)
        {
            Debug.Log("ConditionalEvent");
            _conditionalEvent = conditional;
            if (_conditionalEvent.EventType == EventType.AnyItem)
            {
                _enterType = EnterType.AnyItem;
            }
            else
            {
                _enterType = EnterType.Decide;
                _decide = true;
                Debug.Log(_conditionalEvent.Question);
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

        /// <summary>
        /// シーン切り替えを行う関数
        /// </summary>
        /// <param name="sceneTransition">イベント</param>
        void SceneTransition(SceneTransitionEvent sceneTransition)
        {
            NextEvent();
            _gameFlowManager.SceneTransition(sceneTransition.SceneName);
        }
        #endregion
    }
}
