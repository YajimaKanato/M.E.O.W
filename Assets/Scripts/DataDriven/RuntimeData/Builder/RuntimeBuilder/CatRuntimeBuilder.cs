using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "CatRuntimeBuilder", menuName = "RuntimeBuilder/CatRuntimeBuilder")]
    public class CatRuntimeBuilder : RuntimeBuilderBase
    {
        [SerializeField] CatDefaultData _cat;

        public override void CreateRuntime(RuntimeDataRepository repository, DataID id)
        {
            if (repository.TryGetData<CatRuntimeData>(id, out var dummy)) return;

            var data = new CatRuntimeData(_cat);
            repository.RegisterData(id, data);
        }
    }
}
