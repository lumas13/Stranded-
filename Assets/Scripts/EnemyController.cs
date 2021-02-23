using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private bool keyDropped = false;

    public int enemyHP;

    public GameObject player;
    public GameObject keyPrefab;
    public GameObject keyDropPos;
    public AudioClip[] audioClip;
   
    private Animator animator;
    private NavMeshAgent navMesh;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        chasePlayer();
        enemyDied();
        keyDropper();
    }

    private void chasePlayer() //Chases the player 
    {
        navMesh.SetDestination(player.transform.position);

        //Debug.Log(NavMesh.velocity.magnitude);
        /*
        if (NavMesh.velocity.magnitude > 1)
        {
            Animator.SetBool("isRunning", true);
        }
        else
        {
            Animator.SetBool("isRunning", false);
        }
        */
    }

    private void enemyDied() //Stop enemy, start the death animation and destory the enemy
    {
        if (enemyHP <= 0)
        {
            animator.SetTrigger("hasDied");
            navMesh.speed = 0;
            Destroy(gameObject, 1.8f);
        }
    }

    private void keyDropper() //Drops a key upon the death of enemy
    {
        if (enemyHP <= 0 && keyDropped == false)
        {
            Instantiate(keyPrefab, keyDropPos.transform.position, Quaternion.identity);
            keyDropped = true;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            enemyHP -= 1;
            print(enemyHP);
            audioSource.PlayOneShot(audioClip[0]);
            Destroy(collision.gameObject);
        }
    }
}
