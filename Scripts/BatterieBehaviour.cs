using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterieBehaviour : MonoBehaviour {
    public float speed;
    public GameObject DustPrefab;

    // Use this for initialization
    void Start () {
        //Instantiate(DustPrefab, transform.position, transform.rotation);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TaschenlampeBehaviour.UpgradeBatterie();
            Destroy(DustPrefab);   
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
