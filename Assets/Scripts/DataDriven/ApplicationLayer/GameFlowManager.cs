using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DataDriven
{
    /// <summary>ゲームの流れを司るクラス</summary>
    public class GameFlowManager : MonoBehaviour
    {
        [SerializeField] ActionMapData _actionMapData;
        InputManager _input;
        SceneDataCreateFlow[] _dataFlow;
        SceneObjectFactory _objectFactory;
        RuntimeDataRepository _repository;
        InteractSystem _interactSystem;
        PlaySceneSystem _playSceneSystem;
        UnityConnector _unityConnector;
        MenuSystem _menuSystem;
        TitleSystem _titleSystem;
        Dictionary<string, ActionMapName> _actionMapNames;
        Stack<ActionMapName> _actionMapStack;
        List<InteractMono> _targetList;
        InteractMono _target;
        static GameFlowManager _instance;

        private void Awake()
        {
            GameStart();
        }

        /// <summary>
        /// ゲーム開始時に一回だけ呼ぶ想定の関数
        /// </summary>
        void Initialization()
        {
            //処理実行
            _repository = new RuntimeDataRepository();
            _unityConnector = new UnityConnector();
            _unityConnector.Init();
            _actionMapNames = new Dictionary<string, ActionMapName>();
            foreach (var pair in _actionMapData.Pair)
            {
                _actionMapNames[pair.SceneName] = pair.ActionMapName;
            }
            SceneManager.sceneLoaded += OnSceneLoaded;
            //あとでまとめてクラス作る
            //インスタンス生成
            _interactSystem = new InteractSystem(_repository);
            _playSceneSystem = new PlaySceneSystem(_repository, _unityConnector.ActionConnector);
            _menuSystem = new MenuSystem(_repository);
            _titleSystem = new TitleSystem(_repository);
            _targetList = new List<InteractMono>();
            _input = FindFirstObjectByType<InputManager>();
            _input?.Init();

        }

        /// <summary>
        /// シーンが切り替わるたびに呼ばれる関数
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"></param>
        void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            //インスタンス生成
            _objectFactory = GameObject.FindWithTag("ObjectFactory").GetComponent<SceneObjectFactory>();
            _dataFlow = _objectFactory.DataFlow;
            //処理実行
            foreach (var dataFlow in _dataFlow)
            {
                dataFlow?.CreateSceneData(_repository);
            }
            _objectFactory?.CreateSceneObject(_repository, _unityConnector);
            //アクションマップの設定
            _actionMapStack = new Stack<ActionMapName>();
            ChangeActionMap(_actionMapNames[scene.name]);
            Debug.Log("SceneLoaded");
        }

        /// <summary>
        /// ゲームが開始した時に呼ばれる関数
        /// </summary>
        public void GameStart()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                Initialization();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// アクションマップを切り替える関数
        /// </summary>
        /// <param name="mapName">切り替えるアクションマップの名前</param>
        void ChangeActionMap(ActionMapName mapName = ActionMapName.Unknown)
        {
            //引数に何も指定しなかったらスタックからポップ
            if (mapName == ActionMapName.Unknown)
            {
                _actionMapStack.Pop();
            }
            else
            {
                _actionMapStack.Push(mapName);
            }
            _input.ChangeActionMap(_actionMapStack.Peek());
        }

        #region Title
        /// <summary>
        /// タイトルのカテゴリーを開く関数
        /// </summary>
        /// <returns>カテゴリーを開けたかどうか</returns>
        public void OpenCategory()
        {
            if (_titleSystem.OpenCategory()) ChangeActionMap(ActionMapName.OutGameCategory);
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
            if (_titleSystem.CloseCategory()) ChangeActionMap();
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

        #region PlayScene
        /// <summary>
        /// 移動時の処理を行う関数
        /// </summary>
        /// <param name="move">移動する方向</param>
        /// <param name="position">現在位置</param>
        public void Move(Vector2 move, Vector3 position)
        {
            _playSceneSystem.Move(move);
            if (move != Vector2.zero)
            {
                GetTarget(position);
            }
        }

        /// <summary>
        /// 足場から降りる時の処理を行う関数
        /// </summary>
        /// <param name="down">足場から降りたかどうか</param>
        public void Down(bool down)
        {
            _playSceneSystem.Down(down);
        }

        /// <summary>
        /// 走るときの処理を行う関数
        /// </summary>
        /// <param name="run">走るかどうか</param>
        public void Run(bool run)
        {
            _playSceneSystem.Run(run);
        }

        /// <summary>
        /// ジャンプするときの処理を行う関数
        /// </summary>
        public void Jump()
        {
            _playSceneSystem.Jump();
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="index">選択するスロット</param>
        public void HotbarSelectForKeyboard(int index)
        {
            _playSceneSystem.HotbarSelectForKetboard(index);
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void HotbarselectForGamePad(IndexMove dir)
        {
            _playSceneSystem.HotbarSelectForGamePad(dir);
        }

        public void UseItem()
        {
            _playSceneSystem.UseItem();
        }
        #endregion

        #region Interact
        /// <summary>
        /// インタラクトが行われたときに呼ばれる関数
        /// </summary>
        /// <param name="character">インタラクトを行う対象のキャラクター</param>
        public void Interact()
        {
            if (!_target) return;
            if (_interactSystem.StartInteract(_target.ID)) ChangeActionMap(ActionMapName.UI);
        }

        /// <summary>
        /// インタラクトを進める時に呼ばれる関数
        /// </summary>
        public void Confirm()
        {
            if (_interactSystem.PushInteract()) ChangeActionMap();
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="index">選択するスロット</param>
        public void HotbarSelectOnConversationForKeyboard(int index)
        {
            _interactSystem.HotbarSelectForKetboard(index);
        }

        /// <summary>
        /// アイテムを選択する関数
        /// </summary>
        /// <param name="dir">選択するスロットをずらす方向</param>
        public void HotbarselectOnConversationForGamePad(IndexMove dir)
        {
            _interactSystem.HotbarSelectForGamePad(dir);
        }
        #endregion

        #region Menu
        /// <summary>
        /// メニューを開く関数
        /// </summary>
        public void MenuOpen()
        {
            if (_menuSystem.MenuOpen()) ChangeActionMap(ActionMapName.Menu);
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
            if (_menuSystem.MenuClose()) ChangeActionMap();
        }
        #endregion

        /// <summary>
        /// ターゲットのリストに登録する関数
        /// </summary>
        /// <param name="target">登録するターゲット</param>
        public void AddTargetList(InteractMono target)
        {
            _targetList.Add(target);
        }

        /// <summary>
        /// ターゲットのリストから削除する関数
        /// </summary>
        /// <param name="target">削除するターゲット</param>
        public void RemoveTargetList(InteractMono target)
        {
            _targetList.Remove(target);
        }

        /// <summary>
        /// 一番近いターゲットを返す関数
        /// </summary>
        /// <param name="position">現在位置</param>
        void GetTarget(Vector3 position)
        {
            _target = null;
            foreach (InteractMono target in _targetList)
            {
                if (_target)
                {
                    var dir = Vector3.SqrMagnitude(_target.transform.position - position);
                    var compareDir = Vector3.SqrMagnitude(target.transform.position - position);
                    if (dir > compareDir) _target = target;
                }
                else
                {
                    _target = target;
                }
            }
        }
    }

    /// <summary>タグの名前</summary>
    struct TagName
    {
        public const string PLAYER = "Player";
        public const string CHARACTER = "Character";
    }
}
