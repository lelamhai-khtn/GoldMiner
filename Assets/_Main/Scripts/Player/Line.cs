using UnityEngine;

public class Line : MonoBehaviour
{
    public float rotateSpeed = 80f;
    public float minAngle = -70f;
    public float maxAngle = 70f;

    public float minLength = 1f; 
    public float maxLength = 7f; 
    public float speedDown = 6f; 
    public float speedUp = 8f; 

    public Transform hook; 

    private LineRenderer lr;
    private float angle = 0f;
    private int dir = 1;
    private float length;

    private enum State { Swing, Extending, Retracting }
    private State state = State.Swing;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.useWorldSpace = false;
        length = minLength;
    }

    void Update()
    {
        UpdateState();
        UpdateLineAndHook();
    }

    public void HandleInput()
    {
        state = State.Extending;
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
                }
                break;
        }
    }

    void UpdateLineAndHook()
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);

        lr.SetPosition(0, Vector3.zero);
        lr.SetPosition(1, Vector3.down * (length - 0.2f));

        if (hook) hook.localPosition = Vector3.down * length;
    }
}
