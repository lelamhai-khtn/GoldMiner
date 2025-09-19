using System;
using System.Diagnostics;
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
    public float speedUp = 8f;

    private float angle = 0f;
    private int dir = 1;
    private float length;

    private enum State { Swing, Extending, Retracting }
    private State state = State.Swing;

    public event Action<GameObject> OnGrabbed;
    public bool IsSwinging => state == State.Swing;
    public bool IsExtending => state == State.Extending;
    public bool IsRetracting => state == State.Retracting;

    private void Awake()
    {
        claw.listener = this;

        line.positionCount = 2;
        line.useWorldSpace = false;
        length = minLength;
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
            case State.Swing:
                angle += dir * rotateSpeed * Time.deltaTime;
                if (angle >= maxAngle) { angle = maxAngle; dir = -1; }
                else if (angle <= minAngle) { angle = minAngle; dir = 1; }
                break;

            case State.Extending:
                length += speedDown * Time.deltaTime;
                if (length >= maxLength)
                {
                    length = maxLength;
                    state = State.Retracting;
                }
                break;

            case State.Retracting:
                length -= speedUp * Time.deltaTime;
                if (length <= minLength)
                {
                    length = minLength;
                    state = State.Swing;

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

    public void OnItemGrabbed(GameObject gold)
    {
        if (state == State.Extending)
        {
            var speed = gold.GetComponent<Gold>().data.data.Weight;
            state = State.Retracting;

            OnGrabbed?.Invoke(gold);
        }
    }

    public void Swing()
    {
        state = State.Swing;
    }

    public void Extending()
    {
        state = State.Extending;
    }

    public void Retracting()
    {
        state = State.Retracting;
    }
}
