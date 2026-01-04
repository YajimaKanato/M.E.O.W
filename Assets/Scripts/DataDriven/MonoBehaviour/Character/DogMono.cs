using UnityEngine;

namespace DataDriven
{
    public class DogMono : Character
    {
        public override void Init(CharacterRuntime character)
        {
            _characterRuntime = character;
            tag = "Character";
        }
    }
}
