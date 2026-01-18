using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DataDriven
{
    /// <summary>ゲームの流れを司るクラス</summary>
    public class GameFlowManager : MonoBehaviour
    {
        [SerializeField] ActionMapData _actionMapData;
        //アクションマップ系
        Dictionary<ActionMapName, InputBase> _actionMapDict;
        Dictionary<string, ActionMapName> _actionMapNames;
        Stack<ActionMapName> _actionMapStack;
        //シーンオブジェクト
        FlowBase[] _flows;
        InputBase[] _inputs;
        SceneObjectFactory[] _objectFactory;
        //注入必須系
        RuntimeDataRepository _repository;
        UnityConnector _unityConnector;
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
            //注入必須系生成
            _repository = new RuntimeDataRepository();
            _unityConnector = new UnityConnector();
            _unityConnector.Init();
            //アクションマップ初期化
            _actionMapNames = new Dictionary<string, ActionMapName>();
            foreach (var pair in _actionMapData.Pair)
            {
                _actionMapNames[pair.SceneName] = pair.ActionMapName;
            }
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        /// <summary>
        /// シーンが切り替わるたびに呼ばれる関数
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"></param>
        void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            //アクションマップ系設定
            _actionMapDict = new Dictionary<ActionMapName, InputBase>();
            _actionMapStack = new Stack<ActionMapName>();
            _inputs = FindObjectsByType<InputBase>(FindObjectsSortMode.None);
            foreach (var input in _inputs)
            {
                input.ActionMapSetting();
                _actionMapDict[input.ActionMapName] = input;
            }
            ChangeActionMap(_actionMapNames[scene.name]);

            //フロー設定
            _flows = FindObjectsByType<FlowBase>(FindObjectsSortMode.None);
            foreach (var flow in _flows)
            {
                flow.Init(_instance, _repository, _unityConnector);
            }

            //ファクトリー設定
            _objectFactory = FindObjectsByType<SceneObjectFactory>(FindObjectsSortMode.None);
            foreach (var objectFactory in _objectFactory)
            {
                objectFactory.CreateSceneObject(_repository, _unityConnector);
            }

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
        public void ChangeActionMap(ActionMapName mapName = ActionMapName.Unknown)
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
            //_input.ChangeActionMap(_actionMapStack.Peek());
            foreach (var actionMap in _actionMapDict)
            {
                if (actionMap.Key == _actionMapStack.Peek())
                {
                    actionMap.Value.ActionMapEnable();
                }
                else
                {
                    actionMap.Value.ActionMapDisable();
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
