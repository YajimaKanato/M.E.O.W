using UnityEngine;

namespace DataDriven
{
    /// <summary>ネズミのランタイムデータ</summary>
    public class MouseRuntimeData : CharacterRuntimeData
    {
        public MouseRuntimeData(MouseDefaultData mouse)
        {
            _event = mouse.EventData.Events;
        }
    }
}
