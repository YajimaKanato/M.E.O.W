using UnityEngine;

public class PlayerActionOnUI : MonoBehaviour
{
    [SerializeField] PlayerInfo _playerInfo;
    GameManager _initManager;

    private void Awake()
    {
        _initManager = _playerInfo.InitManager;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void ChangeItemSlot()
    {

    }
}
