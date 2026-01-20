using UnityEngine;

namespace DataDriven
{
    /// <summary>犬のランタイムデータ</summary>
    public class DogRuntimeData : CharacterRuntimeData
    {
        public DogRuntimeData(DogDefaultData dog)
        {
            _event = dog.EventData.Events;
        }
    }
}
