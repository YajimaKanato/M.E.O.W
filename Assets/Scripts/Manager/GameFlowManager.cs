using System.Collections.Generic;
using Interface;
using Scene;
using UnityEngine.SceneManagement;

/// <summary>ゲームの流れに関する制御を行うクラス</summary>
public class GameFlowManager : InitializeBehaviour
{
    List<IPauseTime> _iPauseList;
    List<IInteractime> _iInteractList;

    public override bool Init(GameManager manager)
    {
        _isInitialized = InitializeManager.InitializationForVariable(out _gameManager, manager);
        _isInitialized = InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);
        _isInitialized = InitializeManager.InitializationForVariable(out _iPauseList, new List<IPauseTime>());
        _isInitialized = InitializeManager.InitializationForVariable(out _iInteractList, new List<IInteractime>());

        return _isInitialized;
    }

    /// <summary>
    /// 管理リストに登録する関数
    /// </summary>
    /// <typeparam name="T">任意のインターフェースを継承している型</typeparam>
    /// <param name="instance">自分自身</param>
    public void ListRegistering<T>(T instance) where T : IPauseTime, IInteractime
    {
        if (instance is IPauseTime) _iPauseList.Add(instance);
        if (instance is IInteractime) _iInteractList.Add(instance);
    }

    /// <summary>
    /// 管理リストから削除する関数
    /// </summary>
    /// <typeparam name="T">任意のインターフェースを継承している型</typeparam>
    /// <param name="instance">自分自身</param>
    public void ListDelete<T>(T instance) where T : IPauseTime, IInteractime
    {
        if (instance is IPauseTime) _iPauseList.Remove(instance);
        if (instance is IInteractime) _iInteractList.Remove(instance);
    }

    /// <summary>
    /// シーン遷移を行う関数
    /// </summary>
    /// <param name="sceneName">遷移するシーンの名前</param>
    public void SceneChange(string sceneName)
    {
        var scene = SceneManager.GetActiveScene().name;
        //インゲームに入るかインゲームから出る時にデータをリセットする
        if (scene == SceneName.Title.ToString() && sceneName.Contains(SceneName.Game.ToString())
            || scene.Contains(SceneName.Game.ToString()) && !sceneName.Contains(SceneName.Game.ToString()))
        {
            _runtimeDataManager.DataReset();
        }
        SceneManager.LoadScene(sceneName.ToString());
    }
}

namespace Scene
{
    public enum SceneName
    {
        Title,
        Option,
        Intro,
        Game,
        EndList,
        End,
        Bag
    }
}
