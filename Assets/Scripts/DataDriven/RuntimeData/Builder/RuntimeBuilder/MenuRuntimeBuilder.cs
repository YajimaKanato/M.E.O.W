using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "MenuRuntimeBuilder", menuName = "RuntimeBuilder/MenuRuntimeBuilder")]
    public class MenuRuntimeBuilder : RuntimeBuilderBase
    {
        [SerializeField] MenuDefaultData _menu;

        public override bool CreateRuntime(RuntimeDataRepository repository, DataID id)
        {
            if (repository.TryGetData<MenuRuntimeData>(id, out var dummy)) return false;

            var data = new MenuRuntimeData(_menu);
            repository.RegisterData(id, data);
            return true;
        }
    }
}
