using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
using System;

public class StoryManager : MonoBehaviour
{
    static float _textSpeed = 0.1f;
    static bool _isEnter = false;
    static bool _isTyping = false;

    static StoryManager _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 初期化関数
    /// </summary>
    void Init()
    {

    }

    /// <summary>
    /// エンター入力時に行う関数
    /// </summary>
    /// <returns>テキスト表示中かどうか</returns>
    public static bool PushEnter()
    {
        //テキスト表示中の処理
        if (_isTyping)
        {
            _isEnter = true;
        }

        return _isTyping;
    }

    /// <summary>
    /// テキストを更新する関数
    /// </summary>
    /// <param name="textUI">テキストを表示するUI</param>
    /// <param name="text">表示するテキスト</param>
    public static void TextUpdate(Text textUI, string text)
    {
        _instance.StartCoroutine(TextCoroutine(textUI, text));
    }

    /// <summary>
    /// テキストを任意の速度で流す関数
    /// </summary>
    /// <param name="textUI">テキストを表示するUI</param>
    /// <param name="text">表示するテキスト</param>
    /// <returns></returns>
    static IEnumerator TextCoroutine(Text textUI, string text)
    {
        textUI.text = "";
        _isTyping = true;
        var wait = new WaitForSeconds(_textSpeed);

        foreach (var t in text)
        {
            //エンター入力が入ったら全文表示
            if (_isEnter)
            {
                textUI.text = text;
                yield return wait;
                break;
            }

            //一文字ずつ追加
            textUI.text += t;
            yield return wait;
        }
        yield return wait;

        _isEnter = false;
        _isTyping = false;
        yield break;
    }
}
