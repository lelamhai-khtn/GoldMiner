using UnityEngine;

public interface IGrabbedObject 
{
    event System.Action<GameObject> OnGrabbed;
}
