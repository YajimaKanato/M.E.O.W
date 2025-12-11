using UnityEngine;

[CreateAssetMenu(fileName = "UIDataList", menuName = "UIData/UIDataList")]
public class UIDataList : InitializeSO
{
    [SerializeField] UIDataBase[] _uiDataBaseList;
    public UIDataBase[] UIDataBaseList => _uiDataBaseList;
    public override bool Init(GameManager manager)
    {
        if (_uiDataBaseList == null)
        {
            InitializeManager.FailedInitialization();
        }
        else
        {
            foreach (var uiData in _uiDataBaseList)
            {
                if (!uiData) InitializeManager.FailedInitialization();
                uiData?.Init(manager);
            }
        }
        return _isInitialized;
    }
}
