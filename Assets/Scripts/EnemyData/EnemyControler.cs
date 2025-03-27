using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyControler : MonoBehaviour
{
    public Transform player;
    public Transform truck;
    public float moveSpeed = 2;
    public int attackDistance = 10;
    public int attackDamage = 20;
    public float attackFrequency = 2f;
    private float lastAttack;
    public int health = 20;
    private CharacterController characterController;
    internal bool canMove = false;

    void Start()
    {
        lastAttack = Time.timeSinceLevelLoad;
        characterController = GetComponent<CharacterController>();
        StartCoroutine(AllowMove());
    }

    IEnumerator AllowMove(){
        yield return new WaitForSeconds(0.2f);
        canMove=true;
    }

    void Update()
    {   
        Vector3 playerPos = player.position;
        playerPos.y = 0;
        Vector3 truckPos = truck.position;
        truckPos.y = 0;
        Vector3 currentPos = transform.position;
        currentPos.y = 0;
        float truckDistance = Vector3.SqrMagnitude(truckPos - currentPos);
        float playerDistance = Vector3.SqrMagnitude(playerPos - currentPos);
        if (truckDistance < playerDistance)
        {
            if (truckDistance <= attackDistance && Time.timeSinceLevelLoad - attackFrequency > lastAttack)
            {
                truck.gameObject.GetComponentInChildren<TruckInfo>().takeDamage(attackDamage);
                lastAttack = Time.timeSinceLevelLoad;
            }
            else if (truckDistance > attackDistance && canMove)
            {
                Vector3 moveDirection = (truckPos - currentPos).normalized;
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                characterController.transform.rotation = Quaternion.RotateTowards(characterController.transform.rotation, toRotation, 200 * Time.deltaTime);

                characterController.Move((truckPos - currentPos).normalized * Time.deltaTime * moveSpeed + Vector3.down * 9.8f);
                // transform.position += (truckPos - currentPos).normalized * Time.deltaTime * moveSpeed;

            }
        }
        else
        {
            if (playerDistance <= attackDistance && Time.timeSinceLevelLoad - attackFrequency > lastAttack)
            {
                player.gameObject.GetComponent<PlayerData>().takeDamage(attackDamage);
                lastAttack = Time.timeSinceLevelLoad;
            }
            else if (playerDistance > attackDistance && canMove)
            {
                Vector3 moveDirection = (playerPos - currentPos).normalized;
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                characterController.transform.rotation = Quaternion.RotateTowards(characterController.transform.rotation, toRotation, 200 * Time.deltaTime);
                characterController.Move(moveDirection * Time.deltaTime * moveSpeed + Vector3.down * 9.8f);
                // transform.position += (playerPos - currentPos).normalized * Time.deltaTime * moveSpeed;

            }

        }
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log(health);
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Vector3 playerPos = player.position;
            playerPos.y = 0;
            Vector3 currentPos = transform.position;
            currentPos.y = 0;
            characterController.Move((currentPos - playerPos).normalized * 2 + Vector3.down * (-9.8f));
        }
    }
}
