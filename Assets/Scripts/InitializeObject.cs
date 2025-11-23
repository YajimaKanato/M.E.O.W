using Interface;
using UnityEngine;

public abstract class InitializeObject : ScriptableObject, IInitialize
{
    protected GameManager _gameManager;
    public abstract void Init(GameManager manager);
}
