using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public IHook hook;
    public IGrabbedObject clawGrabbed;
    public Animator animator;

    private void Awake()
    {
        hook = this.transform.Find("Hook").GetComponent<IHook>();
        clawGrabbed = this.transform.Find("Hook").GetComponent<IGrabbedObject>();
        animator = this.transform.Find("Model").GetComponent<Animator>();
    }

    private void Reset()
    {
        animator = this.transform.Find("Model").GetComponent<Animator>();
    }

    void OnEnable()
    {
        if (clawGrabbed != null) clawGrabbed.OnGrabbed += HandleGrabbed;
    }

    void OnDisable()
    {
        if (clawGrabbed != null) clawGrabbed.OnGrabbed -= HandleGrabbed;
    }

    private void Update()
    {
        switch(hook.state)
        {
            case HookState.Swing:
                animator.SetBool("Rope", false);
                break;

            case HookState.Extending:
                break;

            case HookState.Retracting:
                break;
        }
    }

    private void HandleGrabbed(GameObject go)
    {
        Debug.Log("GrabbedObject: " + go.name);
    }

    public void StartExtending()
    {
        if (hook.state == HookState.Swing)
        {
            hook.state = HookState.Extending;
            animator.SetBool("Rope", true);
        }
    }
}