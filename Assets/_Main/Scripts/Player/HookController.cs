using UnityEngine;

public class HookController : MonoBehaviour
{
    public IHook hook;

    private void Start()
    {
        hook = this.transform.Find("Hook").GetComponent<IHook>();
    }

    public void StartExtending()
    {
        if (hook.IsSwinging)
            hook.Extending();
    }
}
