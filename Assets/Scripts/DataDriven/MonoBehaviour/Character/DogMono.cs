using UnityEngine;

namespace DataDriven
{
    public class DogMono : Character, IMono<DogRuntimeData>
    {
        public void Init(DogRuntimeData character)
        {
            _characterRuntime = character;
            tag = "Character";
        }
    }
}
