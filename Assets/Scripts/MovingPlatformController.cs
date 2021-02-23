using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    float xLimit = 8.5f;
    private int speed = 5;

    private bool isFoward = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < xLimit && isFoward) // == true
        {
            Foward();
        }
        else if (transform.position.x > -xLimit && !isFoward) // == false
        {
            Backward();
        }
        else
        {
            isFoward = !isFoward;
        }

        /*
        if (onMoveCube)
        {
            // 'this'reference to MoveCube
            player.transform.SetParent(this.transform);
        }
        else
        {
            // Disown the child(player)
            player.transform.SetParent(null);
        }
        */
    }

    private void Foward()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    private void Backward()
    {
        transform.Translate(Vector3.right * Time.deltaTime * -speed);
    }
}
