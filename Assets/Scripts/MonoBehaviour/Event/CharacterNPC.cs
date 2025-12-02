using System.Collections;
using UnityEngine;

/// <summary>イベントのベースクラス</summary>
[RequireComponent(typeof(Rigidbody2D))]
public abstract class CharacterNPC : InitializeBehaviour
{
    [SerializeField, Tooltip("インタラクト対象になったときの表示オブジェクト")] GameObject _targetSign;

    /// <summary>
    /// 初期化関数
    /// </summary>
    public override bool Init(GameManager manager)
    {
        InitializationForVariable(out _gameManager, manager);

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
            FailedInitialization();
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
    /// <returns></returns>
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
