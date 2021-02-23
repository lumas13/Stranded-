using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    public GameObject instruct;

    // Start is called before the first frame update
    void Start()
    {
        instruct.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Instructions()
    {
        instruct.SetActive(true);
    }
}
