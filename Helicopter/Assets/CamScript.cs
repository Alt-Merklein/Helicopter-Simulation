using UnityEngine;

public class CamScript : MonoBehaviour
{
    public Transform helicopter;
    private Vector3 offset;
    public float rotationSpeed;
    private float rotation = 0;
    public bool rotatecam;

    void Start(){
        offset = transform.position - helicopter.position;
    }
    void Update(){
        if (rotatecam){
            transform.position = helicopter.position + Quaternion.AngleAxis(rotation, Vector3.up)*offset;
            gameObject.GetComponent<Camera>().transform.LookAt(helicopter, Vector3.up);
            rotation += rotationSpeed * Time.deltaTime;
            if (rotation > 360){
                rotation = rotation - 360;
            }
        }
        else{
            transform.position = helicopter.position + offset;
        }
    }
}