using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D), typeof(CatEvent))]
public class Cat : MonoBehaviour
{
    Rigidbody2D _rb2d;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
