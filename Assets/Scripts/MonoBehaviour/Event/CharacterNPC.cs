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
    public override void Init(GameManager manager)
    {
        if (tag != "Event")
        {
            tag = "Event";
        }

        if (gameObject.layer != LayerMask.NameToLayer("Event"))
        {
            gameObject.layer = LayerMask.NameToLayer("Event");
        }
        TargetSignInactive();
    }

    /// <summary>
    /// 任意のイベントの情報を返す関数
    /// </summary>
    /// <param name="player"></param>
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
