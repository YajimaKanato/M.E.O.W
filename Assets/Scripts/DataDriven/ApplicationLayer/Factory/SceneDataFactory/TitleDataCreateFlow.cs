using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu(fileName = "TitleFlow", menuName = "DataFlow/TitleFlow")]
    public class TitleDataCreateFlow : SceneDataCreateFlow
    {
        [Header("DefaultData")]
        [SerializeField] TitleDefaultData _title;

        public override void CreateSceneData(RuntimeDataRepository repository)
        {
            _repository = repository;
            DataCreate(DataID.Title, _title, data => new TitleRuntimeData(data));
        }
    }
}
