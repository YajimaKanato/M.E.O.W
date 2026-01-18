using UnityEngine;

namespace DataDriven
{
    /// <summary>アンドロイドのランタイムデータ</summary>
    public class AndroidRuntimeData : CharacterRuntimeData
    {
        public AndroidRuntimeData(AndroidDefaultData android)
        {
            _event = android.EventData.Events;
        }
    }
}
