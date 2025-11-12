using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
using System;

public class StoryManager : MonoBehaviour
{
    InputAction _enterAct;

    float _textSpeed = 0.1f;
    bool _isEnter = false;
    bool _isTyping = false;

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

    private void OnEnable()
    {
        _enterAct.started += PushEnter;
    }

    private void OnDisable()
    {
        _enterAct.started -= PushEnter;
    }

    /// <summary>
    /// 初期化関数
    /// </summary>
    void Init()
    {
        _enterAct = InputSystem.actions.FindAction("Enter");
    }

    /// <summary>
    /// エンター入力時に行う関数
    /// </summary>
    /// <param name="context"></param>
    void PushEnter(InputAction.CallbackContext context)
    {
        if (_isTyping)
        {
            _isEnter = true;
        }
        else
        {
            //次のログを流す
        }
    }

    /// <summary>
    /// テキストを更新する関数
    /// </summary>
    /// <param name="textUI">テキストを表示するUI</param>
    /// <param name="text">表示するテキスト</param>
    public void TextUpdate(Text textUI, string text)
    {
        StartCoroutine(TextCoroutine(textUI, text));
    }

    /// <summary>
    /// テキストを任意の速度で流す関数
    /// </summary>
    /// <param name="textUI">テキストを表示するUI</param>
    /// <param name="text">表示するテキスト</param>
    /// <returns></returns>
    IEnumerator TextCoroutine(Text textUI, string text)
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
                break;
            }

            //一文字ずつ追加
            textUI.text += t;
            yield return wait;
        }

        _isEnter = false;
        _isTyping = false;
        yield break;
    }
}
