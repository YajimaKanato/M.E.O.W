using UnityEngine;

/// <summary>イベントのベースクラス</summary>
[RequireComponent(typeof(Rigidbody2D))]
public class EventBase : MonoBehaviour
{
    [SerializeField, Tooltip("インタラクト対象になったときの表示オブジェクト")] GameObject _targetSign;
    [SerializeField] EventBaseData _eventBaseData;
    public EventBaseData EventBaseData => _eventBaseData;

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// 初期化関数
    /// </summary>
    protected virtual void Init()
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
