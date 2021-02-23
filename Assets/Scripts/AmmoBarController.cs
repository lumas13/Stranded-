using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBarController : MonoBehaviour
{
    public Slider slider;

    public void setAmmo(int ammo)
    {
        slider.value = ammo;
    }

    public void setMaxAmmo(int ammo)
    {
        slider.maxValue = ammo;
        slider.value = ammo;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
