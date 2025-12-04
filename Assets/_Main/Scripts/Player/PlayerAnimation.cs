using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Extending()
    {
        animator.SetBool("Rope", true);
    }

    public void Retracting()
    {
        animator.SetBool("Rope", false);
    }
}
