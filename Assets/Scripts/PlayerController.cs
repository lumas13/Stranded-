using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController inst;

    public int ammo;    
    public int playerHealth;
    public float playerSpeed;
    public float rotateSpeed;

    public GameObject bulletPrefab;
    public GameObject bulletSpawn;
    public Text healthText;
    public Text ammoText;

    private Animator animator;
    private Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }


    void Movement()
    {
        if (Input.GetKey(KeyCode.W)) //Move foward and start animation
        {
            //Debug.Log("Test");
            transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
            animator.SetBool("isRunning", true);
        }
        else if (Input.GetKey(KeyCode.S)) //Move backward and start animation
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

        if (Input.GetKey(KeyCode.A)) //Rotate Left
        {
            transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0));
        }
        else if (Input.GetKey(KeyCode.D)) //Rotate Right
        {
            transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
        }
    }

    void Shooting()
    {
        if (Input.GetMouseButtonDown(0)) //Left click on mouse to shoot
        {
            Instantiate(bulletPrefab, bulletSpawn.transform.position, transform.rotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Key")) //Destroys the key when player collides with it
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("HealingPad"))
        {
            if (playerHealth < 100) //Heals player when player is on the pad and health is less than 100
            {
                playerHealth++;
                healthText.GetComponent<Text>().text = "Health: " + playerHealth;
            }
        }

        if (collision.gameObject.CompareTag("Test"))
        {
                playerHealth--;
                healthText.GetComponent<Text>().text = "Health: " + playerHealth; 
        }
    }

}
