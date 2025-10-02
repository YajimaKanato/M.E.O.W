using UnityEngine;

public abstract class EventBase : MonoBehaviour
{
    private void Start()
    {
        if (tag != "Event")
        {
            tag = "Event";
        }

        if (gameObject.layer != LayerMask.NameToLayer("Event"))
        {
            gameObject.layer = LayerMask.NameToLayer("Event");
        }
    }

    public virtual void Event()
    {
        Debug.Log("EventBase");
    }
}
