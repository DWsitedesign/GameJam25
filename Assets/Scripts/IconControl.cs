using System;
using UnityEngine;

public class IconControl : MonoBehaviour
{
    public float RotateSpeed = 22f ;
    public float YMovement = .5f;
    private Vector3 yvalue;

    void Start()
    {
        yvalue = transform.position;
    }
    void Update()
    {
        transform.position = yvalue + new Vector3(0, (float)(Math.Sin(Time.time)*YMovement), 0);
        this.transform.Rotate(new Vector3(0, RotateSpeed*Time.deltaTime, 0));
    }
}
