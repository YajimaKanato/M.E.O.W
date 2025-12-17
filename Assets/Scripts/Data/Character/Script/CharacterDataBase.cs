using UnityEngine;

/// <summary>キャラクターのベースクラス</summary>
public abstract class CharacterDataBase : InitializeSO
{
    [SerializeField] protected string _characterName;
    [SerializeField] protected Sprite _characterImage;
}
