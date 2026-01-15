using UnityEngine;

namespace DataDriven
{
    public class TitleObjectFactory : SceneObjectFactory
    {
        [Header("SceneObject")]
        [SerializeField] TitleMono _title;

        public override void CreateSceneObject(RuntimeDataRepository repository, UnityConnector connector)
        {
            _repository = repository;
            _connector = connector;

            if (_title) ObjectCreate<TitleMono, TitleRuntimeData>(DataID.Title, _title);
        }
    }
}
