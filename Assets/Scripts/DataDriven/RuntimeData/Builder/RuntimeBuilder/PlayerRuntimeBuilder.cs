using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "PlayerRuntimeBuilder", menuName = "RuntimeBuilder/PlayerRuntimeBuilder")]
    public class PlayerRuntimeBuilder : RuntimeBuilderBase
    {
        [SerializeField] PlayerDefaultData _player;

        public override void CreateRuntime(RuntimeDataRepository repository, DataID id)
        {
            if (repository.TryGetData<PlayerRuntimeData>(id, out var dummy)) return;

            var data = new PlayerRuntimeData(_player);
            repository.RegisterData(id, data);
        }
    }
}
