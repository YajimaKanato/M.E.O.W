using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "SERuntimeBuilder", menuName = "RuntimeBuilder/SERuntimeBuilder")]
    public class SERuntimeBuilder : RuntimeBuilderBase
    {
        [SerializeField] SEDefaultData _se;

        public override bool CreateRuntime(RuntimeDataRepository repository, DataID id)
        {
            if (repository.TryGetData<SERuntimeData>(id, out var dummy)) return　false;

            var data = new SERuntimeData(_se);
            repository.RegisterData(id, data);
            return true;
        }
    }
}
