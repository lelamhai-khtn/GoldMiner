using UnityEngine;

public class GrabbedObject : MonoBehaviour
{
    public IGrabbedObject clawGrabbed;

    private void Awake()
    {
        clawGrabbed = this.transform.Find("Hook").GetComponent<IGrabbedObject>();
    }
    void OnEnable()
    {
        if (clawGrabbed != null) clawGrabbed.OnGrabbed += HandleGrabbed;
    }
   
    void OnDisable()
    {
        if (clawGrabbed != null) clawGrabbed.OnGrabbed -= HandleGrabbed;
    }

    private void HandleGrabbed(GameObject gold)
    {
        Debug.Log("GrabbedObject: " + gold.name);
    }
}
