using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour {
    public float speed;
    public float time;
    private float timeToChangeDirection;
    private float timeToDestroy;
    private float damage;
    public float damageLimit;
    public GameObject ExplosionPrefab;
    private int timeChange = 1;
    public GameObject player;
    public GameObject enemy;
    public float distance;
    public Light flashlight;

    void OnCollisionStay(Collision collision)
    {
        // An einer Wand, wird die Richtung, solange geändert, bis der Enemy sich davon weg bewegt.
        if (collision.gameObject.tag == "End")
        {
            ChangeDirection();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //Solange der Enemy von dem Licht getroffen wird, solange nimmt er schaden.
        if (other.gameObject.tag == "light")
        {
            if (flashlight.enabled == true)
            {
                damage = damage + (1 * Time.deltaTime);
                timeToDestroy += Time.deltaTime;
            }
        }
    }
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, enemy.transform.position);
        if (distance <= 100)
        {
            MoveToPlayer();
        }
        else
        {
            //Die Zeit bis die Richtunggeändert wird, wird verringert
            timeToChangeDirection -= Time.deltaTime;

            if (timeToChangeDirection <= 0)
            {
                ChangeDirection();
            }

            //Enemy nach vorne Bewegen.
            if (timeChange == 1)
            {
                GetComponent<Rigidbody>().velocity = transform.forward * speed;
            }
        }
        //Explosion muss ein bisschen früher einsetzen, da der Enemy im Rauch erst verschwinden soll.
        if (damage >= damageLimit)
        {
           DestroyGameObject();
        }
        if (timeToDestroy >= damageLimit + 1.2f)
        {
           Destroy(gameObject);
        }
        
    }

    private void ChangeDirection()
    {
        //Die Richtung wird geändert.
        Quaternion quat = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up);
        Vector3 newUp = quat * Vector3.forward;
        newUp.Normalize();
        transform.forward = newUp;
        timeToChangeDirection = time;
    }

    void DestroyGameObject()
    {
        //Explosion auf Position setzen und initialisieren.
        Vector3 pa = Vector3.forward;
        pa.Set(0, 13, 0);
        Instantiate(ExplosionPrefab, transform.position + pa, transform.rotation);
        timeChange = 0;

    }
    void MoveToPlayer()
    {
        transform.LookAt(player.transform);
        transform.Translate(player.transform.position * Time.deltaTime);
    }
}
