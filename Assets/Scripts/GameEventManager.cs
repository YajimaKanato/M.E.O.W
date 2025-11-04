using Interface;
using UnityEngine;

/// <summary>ゲーム内のイベントに関する制御を行うスクリプト</summary>
public class GameEventManager : MonoBehaviour
{
    static GameEventManager _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// プレイヤーの体力を管理する関数
    /// </summary>
    /// <param name="health">IHealthを実装したスクリプトのインスタンス</param>
    /// <param name="player">プレイヤー</param>
    public static void HealthManagement(IHealth health, PlayerAction player)
    {
        player.HPUpdate(health.Health);
    }

    /// <summary>
    /// プレイヤーの空腹度を管理する関数
    /// </summary>
    /// <param name="saturate">ISaturateを実装したスクリプトのインスタンス</param>
    /// <param name="player">プレイヤー</param>
    public static void FullnessManagement(ISaturate saturate, PlayerAction player)
    {
        player.Saturation(saturate.Saturate);
    }
}
