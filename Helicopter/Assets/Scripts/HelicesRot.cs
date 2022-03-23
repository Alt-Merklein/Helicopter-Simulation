using UnityEngine;

public class HelicesRot : MonoBehaviour
{
    public Transform helicopter;
    [HideInInspector]
    public float currentSpeed;
    public float Heliceacceleration;
    public float Helicedeacceleration;
    public float maxRotSpeed;

    [Space]
    [Header("Helices")]
    [Space]
    public Transform helice1;
    public Transform helice2;
    public Transform contrapeso1;
    public Transform contrapeso2;

    [Space]
    [Header("X rotation")]
    [Space]
    [HideInInspector] public float XRotAccum = 0;
    public float XrotSpeed;
    public float maxXAngleOffset;

    [Space]
    [Header("Y rotation")]
    [Space]
    [HideInInspector] public float yRotAccum = 0;
    public float yRotSpeed;
    public float maxYAngleOffset;
    void Start()
    {
        currentSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)){
            Accel(Heliceacceleration * Time.deltaTime);
        }
        else{
            Accel(-Helicedeacceleration * Time.deltaTime);
        }

        //Rotating the things
        helice1.localRotation *= Quaternion.Euler(0,0,currentSpeed * Time.deltaTime);
        helice2.localRotation *= Quaternion.Euler(0,0,-currentSpeed * Time.deltaTime);
        contrapeso1.localRotation *= Quaternion.Euler(0,0,currentSpeed * Time.deltaTime);
        contrapeso2.localRotation *= Quaternion.Euler(0,0,-currentSpeed * Time.deltaTime);

        //Rotating on X and Y axes
        XRotAccum += Input.GetAxis("Vertical") * Time.deltaTime * XrotSpeed;
        yRotAccum += Input.GetAxis("Horizontal") * Time.deltaTime * yRotSpeed;

        //REPAROS NECESSARIOS ABAIXO
        if (XRotAccum > maxXAngleOffset){
            if (yRotAccum > maxYAngleOffset){
                helicopter.rotation = Quaternion.Euler(maxXAngleOffset,maxYAngleOffset ,0);
            }
            if (yRotAccum < -maxYAngleOffset){
                helicopter.rotation = Quaternion.Euler(maxXAngleOffset, -maxYAngleOffset, 0);
            }
            helicopter.rotation = Quaternion.Euler(maxXAngleOffset, yRotAccum,0);
        }

        if (XRotAccum < -maxXAngleOffset){
            if (yRotAccum > maxYAngleOffset){
                helicopter.rotation = Quaternion.Euler(-maxXAngleOffset,maxYAngleOffset ,0);
            }
            if (yRotAccum < -maxYAngleOffset){
                helicopter.rotation = Quaternion.Euler(-maxXAngleOffset, -maxYAngleOffset, 0);
            }
            helicopter.rotation = Quaternion.Euler(-maxXAngleOffset,yRotAccum ,0);
        }
        helicopter.rotation = Quaternion.Euler(XRotAccum,yRotAccum ,0);
        //REPAROS NECESSARIOS
        
    
    }

    private void Accel(float value){
        currentSpeed += value;
        if (currentSpeed > maxRotSpeed){
            currentSpeed = maxRotSpeed;
        }
        if (currentSpeed < 0){
            currentSpeed = 0; 
        }
    }
}
