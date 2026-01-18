using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "BGMRuntimeBuilder", menuName = "RuntimeBuilder/BGMRuntimeBuilder")]
    public class BGMRuntimeBuilder : RuntimeBuilderBase
    {
        [SerializeField] BGMDefaultData _bgm;

        public override void CreateRuntime(RuntimeDataRepository repository, DataID id)
        {
            if (repository.TryGetData<BGMRuntimeData>(id, out var dummy)) return;

            var data = new BGMRuntimeData(_bgm);
            repository.RegisterData(id, data);
        }
    }
}
