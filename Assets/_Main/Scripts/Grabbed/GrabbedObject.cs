using UnityEngine;

public class GrabbedObject : MonoBehaviour
{
    public IGrabbedObject grabbedObject;

    private void Awake()
    {
        grabbedObject = this.transform.Find("Claw").GetComponent<IGrabbedObject>();
    }
    void OnEnable()
    {
        if (grabbedObject != null) grabbedObject.OnGrabbed += HandleGrabbed;
    }
   
    void OnDisable()
    {
        if (grabbedObject != null) grabbedObject.OnGrabbed -= HandleGrabbed;
    }

    private void HandleGrabbed(GameObject item)
    {
        if (item == null) return;
        var hook = this.GetComponent<Hook>();
        hook.Retracting();

        var weight = item.GetComponent<ItemObject>().data.data.Weight;
        hook.WeightObject(weight);
    }
}
