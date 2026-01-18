using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "DogRuntimeBuilder", menuName = "RuntimeBuilder/DogRuntimeBuilder")]
    public class DogRuntimeBuilder : RuntimeBuilderBase
    {
        [SerializeField] DogDefaultData _dog;

        public override void CreateRuntime(RuntimeDataRepository repository, DataID id)
        {
            if (repository.TryGetData<DogRuntimeData>(id, out var dummy)) return;

            var data = new DogRuntimeData(_dog);
            repository.RegisterData(id, data);
        }
    }
}
