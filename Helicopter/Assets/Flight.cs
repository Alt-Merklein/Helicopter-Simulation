using UnityEngine;

public class Flight : MonoBehaviour
{
    public Rigidbody rb;
    public HelicesRot script;
    public Transform heliBody;

    [Header("Flight Parameters")]
    [Space]
    private float currentSpeed;
    public float proportionalMultiplier;
    public float maxUpperSpeed;
    public float maxSideSpeed;
    public float maxForwardSpeed;

    // Update is called once per frame
    void FixedUpdate()
    {
        //Subir na direção do mastro
        currentSpeed = proportionalMultiplier * script.currentSpeed * Time.fixedDeltaTime;
        Clamp(currentSpeed, 0, maxUpperSpeed * Time.fixedDeltaTime);
        rb.AddForce(heliBody.up * currentSpeed, ForceMode.Acceleration);

        //Clamp xz speed
        if (rb.velocity.z > maxForwardSpeed){
            rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y, maxForwardSpeed);
        }
        if (rb.velocity.x > maxSideSpeed){
            rb.velocity = new Vector3(maxSideSpeed,rb.velocity.y, rb.velocity.z);
        }
        if (rb.velocity.z < -maxForwardSpeed){
            rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y, -maxForwardSpeed);
        }
        if (rb.velocity.x < -maxSideSpeed){
            rb.velocity = new Vector3(-maxSideSpeed,rb.velocity.y, rb.velocity.z);
        }
    }

    private void Clamp(float value, float min, float max){
        if (value < min) value = min;
        if (value > max) value = max;
    }
}
