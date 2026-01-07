using UnityEngine;

namespace DataDriven
{
    public class DogMono : Character
    {
        public override void Init(CharacterRuntimeData character)
        {
            _characterRuntime = character;
            tag = "Character";
        }
    }
}
