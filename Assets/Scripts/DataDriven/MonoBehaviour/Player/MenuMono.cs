using UnityEngine;

namespace DataDriven
{
    /// <summary>メニューの入力処理を司るクラス</summary>
    public class MenuMono : MonoBehaviour
    {
        InputManager _inputManager;
        GameFlowManager _gameFlowManager;
        static MenuMono _instance;

        public void Awake()
        {
            //Init();
        }

        public void Init()
        {
            if (!_instance)
            {
                _instance = this;
                _gameFlowManager = FindFirstObjectByType<GameFlowManager>();
                _inputManager = FindFirstObjectByType<InputManager>();
                if (_inputManager) ActionRegister();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// アクションを登録する関数
        /// </summary>
        void ActionRegister()
        {

        }
    }
}
