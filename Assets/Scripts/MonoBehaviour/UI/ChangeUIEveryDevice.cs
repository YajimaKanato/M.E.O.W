using UnityEngine;

public class ChangeUIEveryDevice : InitializeBehaviour
{
    [SerializeField, Tooltip("最後の入力がキーボードの時に表示するUI")] Sprite _keyboardImage;
    [SerializeField, Tooltip("最後の入力がゲームパッドの時に表示するUI")] Sprite _gamepadImage;
    SpriteRenderer _spriteRenderer;

    /// <summary>
    /// キーボード入力用のUIを表示する関数
    /// </summary>
    public void UIChangeForKeyboard()
    {
        if (_keyboardImage != null)
        {
            _spriteRenderer.sprite = _keyboardImage;
        }
    }

    /// <summary>
    /// ゲームパッド入力用のUIを表示する関数
    /// </summary>
    public void UIChangeForGamepad()
    {
        if (_gamepadImage != null)
        {
            _spriteRenderer.sprite = _gamepadImage;
        }
    }

    public override bool Init(GameManager manager)
    {
        if (TryGetComponent<SpriteRenderer>(out var renderer))
        {
            _spriteRenderer = renderer;
        }
        else
        {
            return false;
        }

        return true;
    }
}
