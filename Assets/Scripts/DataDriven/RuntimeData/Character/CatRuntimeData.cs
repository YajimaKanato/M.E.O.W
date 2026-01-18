using UnityEngine;

namespace DataDriven
{
    /// <summary>野良猫のランタイムデータ</summary>
    public class CatRuntimeData : CharacterRuntimeData
    {
        public CatRuntimeData(CatDefaultData cat)
        {
            _event = cat.EventData.Events;
        }
    }
}
