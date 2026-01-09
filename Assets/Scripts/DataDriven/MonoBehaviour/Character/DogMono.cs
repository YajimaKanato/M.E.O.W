using UnityEngine;

namespace DataDriven
{
    public class DogMono : SceneEntity
    {
        public void Init(DogRuntimeData character)
        {
            tag = "Character";
        }

        public override void Init()
        {
            tag = "Character";
        }
    }
}
