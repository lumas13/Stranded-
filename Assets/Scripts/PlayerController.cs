using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public int ammo;
    public int money;
    public int playerHealth;
    public float playerSpeed;
    public float rotateSpeed;

    private bool canShoot = true;

    public GameObject bulletPrefab;
    public GameObject bulletSpawn;
    public GameObject shop;
    public Text healthText;
    public Text ammoText;
    public Text moneyText;

    private Animator animator;
    private Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        shop.SetActive(false);

        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();

        healthText.GetComponent<Text>().text = "Health: " + playerHealth;
        moneyText.GetComponent<Text>().text = "Money: " + money;
        ammoText.GetComponent<Text>().text = "Ammo: " + ammo;
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
        if (ammo > 0)
        {
            canShoot = true;

            if (Input.GetMouseButtonDown(0) && canShoot == true) //Left click on mouse to shoot
            {
                Instantiate(bulletPrefab, bulletSpawn.transform.position, transform.rotation);
                ammo--;
                ammoText.GetComponent<Text>().text = "Ammo: " + ammo;
            }
        }

        if (ammo <= 0)
        {
            canShoot = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {    
        if (collision.gameObject.CompareTag("Key")) //Destroys and collects the key when player collides with it
        {
            Destroy(collision.gameObject);
            money++;
            moneyText.GetComponent<Text>().text = "Money: " + money;
        }

        if (collision.gameObject.CompareTag("Shop"))
        {
            shop.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shop"))
        {
            shop.SetActive(false);
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
