using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;

    public bool canWalk = false;
    bool isWalking = false;

    public GameObject chairsit;

    public bool isHiding = false;

    void Update()
    {
        if(canWalk)
        {
            if (Vector3.Distance(transform.position, chairsit.transform.position) <= 4f && Input.GetKeyDown(KeyCode.Space))
            {
                if (isWalking)
                {
                    GetComponent<NavMeshAgent>().enabled = false;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                    transform.parent = chairsit.transform;
                    transform.localPosition = new Vector3(0, 0, 0);
                }
                else
                {
                    GetComponent<NavMeshAgent>().enabled = true;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    transform.parent = null;
                    transform.localPosition = new Vector3(-22.1f, 7.05f, -16.62f);
                }
                isWalking = !isWalking;
            }

            if(!isWalking)
            {
            }
            else if (Input.anyKey)
            {
                Vector3 direction = new Vector3(Input.GetAxis("HorizontalKey"), 0, Input.GetAxis("VerticalKey"));
                Vector3 rightMovement = Vector3.right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
                Vector3 upMovement = Vector3.forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");

                Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

                if(heading != Vector3.zero) transform.forward = heading;
                transform.position += rightMovement;
                transform.position += upMovement;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hiding")) isHiding = true; 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Hiding")) isHiding = false; 
    }
}
