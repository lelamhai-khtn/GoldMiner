using UnityEngine;

public class Hook : MonoBehaviour
{
    public LineRenderer line;
    public Claw claw;

    private float rotateSpeed = 80f;
    private float minAngle = -70f;
    private float maxAngle = 70f;

    [Header("Rope")]
    public float minLength = 1f;
    public float maxLength = 7f;
    public float speedDown = 6f;

    [Header("Retract Speed vs Weight")]
    public float baseSpeedUp = 8f;  
    public float minSpeedUp = 1f;
    public float heavyFactor = 0.25f;

    private float angle = 0f;
    private int dir = 1;
    private float length;
    private float currentWeight = 0f;
    
    public enum State 
    { 
        Swing, Extending, Retracting 
    }

    private State state = State.Swing;

    private void Awake()
    {
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
                float speedUpNow = GetRetractSpeed();
                length -= speedUpNow * Time.deltaTime;
                if (length <= minLength)
                {
                    length = minLength;
                    state = State.Swing;

                    if (claw && claw.transform.childCount > 0)
                    {
                        Destroy(claw.transform.GetChild(0).gameObject);
                    }
                    currentWeight = 0f;
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

    float GetRetractSpeed()
    {
        float denom = 1f + heavyFactor * Mathf.Max(0f, currentWeight);
        return baseSpeedUp / denom;
    }

    public void WeightObject(float weight)
    {
        currentWeight = weight;
    }    

    public void Retracting()
    {
        if (state == State.Extending)
        {
            state = State.Retracting;
        }
    }

    public void StartExtending()
    {
        if (state == State.Swing)
        {
            state = State.Extending;
        }
    }
}