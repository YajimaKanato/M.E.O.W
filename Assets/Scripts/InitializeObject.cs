using Interface;
using UnityEngine;

public abstract class InitializeObject : ScriptableObject, IInitialize
{
    public abstract void Init(GameManager manager);
}
