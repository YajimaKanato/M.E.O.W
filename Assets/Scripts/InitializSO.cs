using Interface;
using UnityEngine;

public abstract class InitializSO : ScriptableObject, IInitialize
{
    protected GameManager _gameManager;
    public abstract bool Init(GameManager manager);
}
