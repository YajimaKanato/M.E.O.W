using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    protected Rigidbody2D _rb2d;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.freezeRotation = true;
    }
}
