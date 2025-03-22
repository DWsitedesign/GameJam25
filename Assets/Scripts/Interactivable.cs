using UnityEngine;

public class Interactivable : MonoBehaviour
{

    public Renderer icon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !icon.enabled){
             icon.enabled = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && icon.enabled){
             icon.enabled = false;
        }
    }
}
