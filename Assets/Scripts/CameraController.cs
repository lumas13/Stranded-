using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    public Vector3 posOff; //0,2,-4
    //public Vector3 topView;

    public float lerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //player = gameObject.FindObjectingWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        transform.position = Vector3.Lerp(transform.position, player.transform.position + posOff, 0.1f);

        if (Input.GetKey(KeyCode.Tab)) //Changes view of camera to top down
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + topView, 1f);
            transform.rotation = Quaternion.Euler(90, 0, 0); 
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + posOff, 0.1f);
            transform.rotation = Quaternion.Euler(13, 0, 0);
        }
        */

        Vector3 finalCameraPosition = player.transform.position + (player.transform.forward * posOff.z) + (player.transform.up * posOff.y);

        transform.position = Vector3.Lerp(transform.position, finalCameraPosition, lerpSpeed);

        transform.LookAt(player.transform);

    }
}
