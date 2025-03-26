using System;
using UnityEngine;

public class sunControl : MonoBehaviour
{
    public float DaySpeed = 5f;
    public float NightSpeed = 15f;
    private float speed;

    public Transform clock;
    // start day at 180
    public bool dayLightcycle = false;


    // Update is called once per frame
    void Update()
    {

        float curTime = this.transform.eulerAngles.x;
        if (curTime > 0 && curTime < 180)
        {
            speed = DaySpeed;
            // todo remove light when it is night speed
        }
        else
        {
            speed = NightSpeed;
        }
        clock.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
        if (dayLightcycle)
        {
            this.transform.Rotate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        // Debug.Log(curTime + "/n" + speed);
    }
}
