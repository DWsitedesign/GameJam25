using UnityEngine;

public class menuCamera : MonoBehaviour
{
    public float rotateSpeed = 2f;
    
    void Update()
    {
        transform.Rotate(Vector3.up*rotateSpeed*Time.deltaTime);
    }
}
