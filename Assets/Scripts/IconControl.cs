
using UnityEngine;

public class IconControl : MonoBehaviour
{
    public float RotateSpeed = 22f ;
    public float YMovement = .5f;
    private Vector3 yvalue;
    private float timeOffset;

    void Start()
    {
        yvalue = transform.position;
        timeOffset = Random.Range(0, 2f*Mathf.PI);
    }
    void Update()
    {
        transform.position = yvalue + new Vector3(0, (float)(Mathf.Sin(timeOffset+Time.time)*YMovement), 0);
        this.transform.Rotate(new Vector3(0, RotateSpeed*Time.deltaTime, 0));
    }
}
