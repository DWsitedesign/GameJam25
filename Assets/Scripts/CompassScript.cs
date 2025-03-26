using UnityEngine;

public class CompassScript : MonoBehaviour
{
    public Transform targetTrans;
    public float distanceDisappear = 18f;
    private bool isActive = true;
    private GameObject compassObj;

    void Start()
    {
        compassObj= transform.Find("compass").gameObject;
    }
    void Update()
    {
        Vector3 lookingDirection = targetTrans.position - transform.position;
        lookingDirection.y = 0;
        if (lookingDirection.sqrMagnitude > distanceDisappear)
        {
            if (!isActive)
            {
                compassObj.SetActive(true);
                isActive = true;
            }
            transform.rotation = Quaternion.LookRotation(lookingDirection);
        }
        else if (isActive)
        {
            isActive = false;
            compassObj.SetActive(false);
        }

    }
}
