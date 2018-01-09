using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour {

    Light flickerLight;
    public float minWaitTime;
    public float maxWaitTime;

    private void Start()
    {
        flickerLight = GetComponent<Light>();
        StartCoroutine(Flash());
    }

    IEnumerator Flash(){
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            flickerLight.enabled = !flickerLight.enabled;
        }
    }
}
