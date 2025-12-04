using System;
using UnityEngine;

public class Claw : MonoBehaviour, IGrabbedObject
{
    public event Action<GameObject> OnGrabbed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnGrabbed?.Invoke(collision.gameObject);

        collision.transform.parent = this.transform;
        collision.transform.localPosition = Vector2.zero;
        collision.transform.localScale = Vector2.one;
    }
}