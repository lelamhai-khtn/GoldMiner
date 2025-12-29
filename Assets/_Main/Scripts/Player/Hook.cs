using System;
using UnityEngine;

public class Hook : MonoBehaviour, IHook, IGrabbedObject, IClawListener
{

    public LineRenderer line;
    public Claw claw;

    [Header("Swing")]
    public float rotateSpeed = 80f;
    public float minAngle = -70f;
    public float maxAngle = 70f;

    [Header("Rope")]
    public float minLength = 1f;
    public float maxLength = 7f;
    public float speedDown = 6f;
    public float defaultForce = 8f;
    public float pullForce;

    private float angle = 0f;
    private int dir = 1;
    private float length;


    public event Action<GameObject> OnGrabbed;
    public HookState state { get; set; }

    private void Awake()
    {
        claw.listener = this;

        line.positionCount = 2;
        line.useWorldSpace = false;
        length = minLength;
        pullForce = defaultForce;
    }

    private void Update()
    {
        UpdateState();
        UpdateLineAndClaw();
    }

    void UpdateState()
    {
        switch (state)
        {
            case HookState.Swing:
                angle += dir * rotateSpeed * Time.deltaTime;
                if (angle >= maxAngle) { angle = maxAngle; dir = -1; }
                else if (angle <= minAngle) { angle = minAngle; dir = 1; }
                break;

            case HookState.Extending:
                length += speedDown * Time.deltaTime;
                if (length >= maxLength)
                {
                    length = maxLength;
                    state = HookState.Retracting;
                }
                break;

            case HookState.Retracting:
                length -= pullForce * Time.deltaTime;
                if (length <= minLength)
                {
                    length = minLength;
                    state = HookState.Swing;
                    pullForce = defaultForce;
                    if (claw.transform.childCount > 0)
                    {
                        Destroy(claw.transform.GetChild(0).gameObject);
                    }
                }
                break;
        }
    }

    void UpdateLineAndClaw()
    {
        transform.rotation = Quaternion.Euler(0, 0, angle); 

        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.down * (length - 0.1f));

        if (claw) claw.transform.localPosition = Vector3.down * length;
    }

    public void OnItemGrabbed(GameObject go)
    {
        if (state == HookState.Extending)
        {
            float weightFactor = go.GetComponent<HookableItem>().config.weightFactor;
            pullForce /= weightFactor;
            state = HookState.Retracting;
            OnGrabbed?.Invoke(go);
        }
    }
}
