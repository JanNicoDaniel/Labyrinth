using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaschenlampeBehaviour : MonoBehaviour {
    public Light flashlight;
    public static float percent = 100;
    private static int oldpercent = 99;
    public static Text playerStats;
    public static int Batterielimit = 20;
    private static float timeToBlink = 14;

    private void Start()
    {
        playerStats = GameObject.Find("PlayerStats").GetComponent<Text>();
        UpdateStats();
    }


    // Update is called once per frame
    void Update() {
        //Wenn man Space drückt dann wird die Taschenlampe aktiviert
        if (Input.GetKeyDown("f") && flashlight.enabled == false && percent > 0)
        {
            flashlight.enabled = true;
        }
        else if (Input.GetKeyDown("f") && flashlight.enabled == true)
        {
            flashlight.enabled = false;
        }

        // Wenn die Taschenlampe an ist dann soll die Batterie langsam leer gehen.
        if (flashlight.enabled == true)
        {
            percent = percent - 1 * Time.deltaTime;
        }

        //Die Zeit bis es wieder Blinkt wird verringert
        if (percent > 10) { 
        timeToBlink -= Time.deltaTime;
        }
        if(timeToBlink <= 0)
        {
            StartCoroutine(Blinky());
        }
        //Wenn die Prozentanzeige bei 20 ist dann wird die Taschenlampe immer stärker flackern bis sie schließlich aus geht 
        if (percent > 0 && percent <= 10 && flashlight.enabled == true)
        {
            StartCoroutine(Blink());
        } else if (percent <= 0)
        {
            flashlight.enabled = false;
        }
        
        // Die Prozentanzahl wird aufgefrischt.
        UpdateStats();
    }
    //Veränderung des Textes auf dem Bildschirm
    public static void UpdateStats()
    {
        if (percent <= oldpercent)
        { 
            int i = oldpercent;
            oldpercent = (int)(percent);
            percent = i;
            playerStats.text = percent + " %";
        }
    }

    //Batterie wird wieder aufgefüllt um 25%
    public static void UpgradeBatterie()
    {
        if (percent < 75)
        {
            percent = percent + 25;
            oldpercent = oldpercent + 24;
        } else {
            percent = 100;
            oldpercent = 99;
        }
    }
    //Zwischendurch mal Blinken.
    IEnumerator Blinky()
    {
        flashlight.enabled = false;
        yield return new WaitForSeconds(0.05f);
        flashlight.enabled = true;
        yield return new WaitForSeconds(0.7f);
        flashlight.enabled = false;
        yield return new WaitForSeconds(0.2f);
        flashlight.enabled = true;
        timeToBlink = 10;
    }

    //Wenn die Taschenlampe beinah leer ist.
    IEnumerator Blink()
    {
       flashlight.enabled = false;
       yield return new WaitForSeconds(1/percent);
       flashlight.enabled = true;
       yield return new WaitForSeconds(1/percent);
       flashlight.enabled = false;
       yield return new WaitForSeconds(1/percent);
       flashlight.enabled = true;  
    }
}
