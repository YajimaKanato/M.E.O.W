using UnityEngine;

[CreateAssetMenu(fileName = "ConversationData", menuName = "UIData/ConversationData")]
public class ConversationData : InitializeSO
{
    public override bool Init(GameManager manager)
    {
        return _isInitialized;
    }
}
