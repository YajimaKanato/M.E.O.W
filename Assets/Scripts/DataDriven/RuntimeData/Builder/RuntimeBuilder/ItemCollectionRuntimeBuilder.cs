using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "ItemCollectionRuntimeBuilder", menuName = "RuntimeBuilder/ItemCollectionRuntimeBuilder")]
    public class ItemCollectionRuntimeBuilder : RuntimeBuilderBase
    {
        [SerializeField] ItemCollectionDefaultData _itemCollection;

        public override void CreateRuntime(RuntimeDataRepository repository, DataID id)
        {
            if (repository.TryGetData<ItemCollectionRuntimeData>(id, out var dummy)) return;

            var data = new ItemCollectionRuntimeData(_itemCollection);
            repository.RegisterData(id, data);
        }
    }
}
