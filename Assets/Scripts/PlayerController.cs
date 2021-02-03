using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject BulleSpawn;

    public float playerSpeed;
    public float rotateSpeed;

    Animator animator;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }


    void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //Debug.Log("Test");
            transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
            animator.SetBool("isRunning", true);

        }

        if (Input.GetKey(KeyCode.S))
        {
            //Debug.Log("Test");
            transform.Translate(Vector3.back * playerSpeed * Time.deltaTime);
            animator.SetBool("isRunning", true);
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            //Debug.Log("Test");
            animator.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
        }
    }

    void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(BulletPrefab, BulleSpawn.transform.position, transform.rotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
        }
    }

}
