using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public Text ammoText;
    public Text moneyText;

    int ammo = 12;
    int money = 10;

    // Start is called before the first frame update
    void Start()
    {
        ammoText.GetComponent<Text>().text = "Ammo: " + ammo;
        moneyText.GetComponent<Text>().text = "Money " + money;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void buyAmmo()
    {
        if (ammo <= 21)
        {
            ammo += 1;
            ammoText.GetComponent<Text>().text = "Ammo: " + ammo;
            money -= 1;
            moneyText.GetComponent<Text>().text = "Money " + money;
        }
    }
}
