using UnityEngine;

/// <summary>キャラクター（NPC）のベースクラス</summary>
[RequireComponent(typeof(Rigidbody2D))]
public abstract class CharacterNPC : MonoBehaviour
{
    protected Rigidbody2D _rb2d;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    /// <summary>
    /// 初期化関数
    /// </summary>
    protected virtual void Init()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.freezeRotation = true;
    }
}
