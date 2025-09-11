using UnityEngine;

public class Claw : MonoBehaviour
{
    public IClawListener listener;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        listener?.OnItemGrabbed(collision.gameObject);

        collision.transform.parent = this.transform;
        collision.transform.localPosition = Vector2.zero;
        collision.transform.localScale = Vector2.one;
    }
}