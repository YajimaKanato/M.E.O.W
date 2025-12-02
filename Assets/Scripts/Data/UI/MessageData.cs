using UnityEngine;

[CreateAssetMenu(fileName = "MessageData", menuName = "UIData/MessageData")]
public class MessageData : InitializeSO
{
    [SerializeField] Sprite[] _textFields;
    public Sprite[] TextFields => _textFields;
    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}
