using UnityEngine;

public class ItemInstance : InitializeBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }

    /// <summary>
    /// アイテムの画像をセットする関数
    /// </summary>
    /// <param name="item">アイテムの画像</param>
    public void ItemImageSetting(Sprite item)
    {
        _spriteRenderer.sprite = item;
    }
}
