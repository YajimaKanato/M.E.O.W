using UnityEngine;

[CreateAssetMenu(fileName = "AnyScene", menuName = "Generator/AnyScene")]
public class ObjectGenerator : InitializeObject
{
    [System.Serializable]
    class GenerateObjectData
    {
        [SerializeField] GameObject _obj;
        [SerializeField] Vector3 _position;
        [SerializeField] Vector3 _rotation;

        /// <summary>
        /// オブジェクトを生成する関数
        /// </summary>
        public void Generate()
        {
            Instantiate(_obj, _position, Quaternion.Euler(_rotation));
        }
    }

    [SerializeField] GenerateObjectData[] _generateList;

    public override void Init(GameManager manager)
    {
        foreach (var obj in _generateList)
        {
            obj.Generate();
        }
    }
}
