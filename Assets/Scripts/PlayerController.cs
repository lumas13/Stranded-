using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public int maxAmmo = 22;
    public int currentAmmo;
    public int money;
    public int maxHealth = 100;
    public int currentHealth;
    public int flareGunLeft = 1;

    public float playerSpeed;
    public float rotateSpeed;

    private bool canShoot = true;
    private bool shopOpened = false;

    public GameObject bulletPrefab;
    public GameObject bulletSpawn;
    public GameObject shop;
    public Text healthText;
    public Text ammoShopText;
    public Text ammoText;
    public Text moneyText;
    public Text moneyShopText;
    public Text flareGunText;

    public AmmoBarController ammoBar;
    public HealthBarController healthBar;
    private Animator animator;
    private Rigidbody rigidBody;
    private AudioSource audioSource;
    public AudioClip[] audioClip;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentAmmo = maxAmmo;

        healthBar.setMaxHealth(maxHealth);
        ammoBar.setMaxAmmo(maxAmmo);

        instance = this;

        shop.SetActive(false);

        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        healthText.GetComponent<Text>().text = "Health: " + maxHealth;
        moneyText.GetComponent<Text>().text = "Money: " + money;
        ammoText.GetComponent<Text>().text = "Ammo: " + maxAmmo;
        flareGunText.GetComponent<Text>().text = "Flare Gun Left: " + flareGunLeft;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
        GameOver();
        Shop();
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
        if (UIController.gameIsPaused == false && shopOpened == false)
        {
            if (currentAmmo > 0)
            {
                canShoot = true;

                if (Input.GetMouseButtonDown(0) && canShoot == true) //Left click on mouse to shoot
                {
                    Instantiate(bulletPrefab, bulletSpawn.transform.position, transform.rotation);
                    currentAmmo--;
                    ammoText.GetComponent<Text>().text = "Ammo: " + currentAmmo;
                    ammoBar.setAmmo(currentAmmo);
                    audioSource.PlayOneShot(audioClip[0]);
                }
            }

            if (currentAmmo <= 0)
            {
                canShoot = false;
                ammoBar.setAmmo(currentAmmo);
            }
        }
    }

    void GameOver()
    {
        if (flareGunLeft == 0)
        {
            SceneManager.LoadScene("WinScene");
        }

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    public void Shop()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            shop.SetActive(true);
            shopOpened = true;

            ammoShopText.GetComponent<Text>().text = "Ammo: " + currentAmmo;
            moneyShopText.GetComponent<Text>().text = "Money " + money;
        }
        else
        {
            shop.SetActive(false);
            shopOpened = false;
        }
    }

    public void BuyAmmo()
    {
        if (currentAmmo < 60 && money > 0)
        {
            currentAmmo += 2;
            money--;
            ammoText.GetComponent<Text>().text = "Ammo: " + currentAmmo;
            moneyText.GetComponent<Text>().text = "Money: " + money;
            ammoBar.setAmmo(currentAmmo);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {    
        if (collision.gameObject.CompareTag("Key")) //Destroys and collects the key when player collides with it
        {
            Destroy(collision.gameObject);
            money += 2;
            moneyText.GetComponent<Text>().text = "Money: " + money;
            audioSource.PlayOneShot(audioClip[2]);
        }

        if (collision.gameObject.CompareTag("Shop"))
        {
            shop.SetActive(true);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentHealth -= 10;

            healthBar.setHealth(currentHealth);
            healthText.GetComponent<Text>().text = "Health: " + currentHealth;
            audioSource.PlayOneShot(audioClip[1]);
        }

        if (collision.gameObject.CompareTag("FlareGun"))
        {
            Destroy(collision.gameObject);
            flareGunLeft--;
            flareGunText.GetComponent<Text>().text = "Flare Gun Left: " + flareGunLeft;
            audioSource.PlayOneShot(audioClip[3]);
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
            if (currentHealth < 100) //Heals player when player is on the pad and health is less than 100
            {
                currentHealth++;
                healthText.GetComponent<Text>().text = "Health: " + currentHealth;
                healthBar.setHealth(currentHealth);
            }
        }

        /*if (collision.gameObject.CompareTag("Test"))
        {
                maxHealth--;
                healthText.GetComponent<Text>().text = "Health: " + currentHealth; 
        }
        */
    }
}
