using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketEnergySystem : MonoBehaviour
{

    public Image fuelBar;
    Rocket rocketState;

    void Start()
    {
        
        rocketState = GetComponent<Rocket>();

    }

    // Update is called once per frame
    void Update()
    {

        fuelFilling();


    }

    public void fuelFilling()
    {

         float fuelAmount = rocketState.fuel;

        fuelAmount += 0.1f;  
        fuelAmount = Mathf.Clamp(fuelAmount, 0, 100);

        fuelBar.fillAmount = fuelAmount / 100f;

        rocketState.fuel = fuelAmount;     


    }


}
