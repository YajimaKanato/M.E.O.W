using Interface;
using UnityEngine;

public abstract class InitializeBehaviour : MonoBehaviour,IInitialize
{
    protected GameManager _gameManager;
    public abstract void Init(GameManager manager);
}
