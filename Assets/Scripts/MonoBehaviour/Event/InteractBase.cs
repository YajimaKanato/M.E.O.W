using UnityEngine;
using System.Collections;

/// <summary>イベントのベースクラス</summary>
[RequireComponent(typeof(Rigidbody2D))]
public abstract class InteractBase : InitializeBehaviour
{
    [SerializeField, Tooltip("インタラクト対象になったときの表示オブジェクト")] GameObject _targetSign;

    public override bool Init(GameManager manager)
    {
        //Manager関連
        _isInitialized = InitializeManager.InitializationForVariable(out _gameManager, manager);
        _isInitialized = InitializeManager.InitializationForVariable(out _runtimeDataManager, _gameManager.RuntimeDataManager);
        _isInitialized = InitializeManager.InitializationForVariable(out _objectManager, _gameManager.ObjectManager);

        //シーン上の設定
        if (tag != "Event")
        {
            tag = "Event";
        }

        if (gameObject.layer != LayerMask.NameToLayer("Event"))
        {
            gameObject.layer = LayerMask.NameToLayer("Event");
        }

        if (!_targetSign)
        {
            InitializeManager.FailedInitialization();
        }
        else
        {
            TargetSignInactive();
        }
        GetComponent<Rigidbody2D>().gravityScale = 0;

        return _isInitialized;
    }

    /// <summary>
    /// 任意のイベントの情報を返す関数
    /// </summary>
    /// <returns>任意のイベントの情報</returns>
    public abstract IEnumerator Event();

    /// <summary>
    /// インタラクト対象表示をアクティブにする関数
    /// </summary>
    public void TargetSignActive()
    {
        _targetSign.SetActive(true);
    }

    /// <summary>
    /// インタラクト対象表示を非アクティブにする関数
    /// </summary>
    public void TargetSignInactive()
    {
        _targetSign.SetActive(false);
    }
}
