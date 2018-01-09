using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeAi : MonoBehaviour {

    public Transform[] waypoints;
    int cur = 0;

    public float speed;
    public Transform spawnPoint;

    void Start()
    {
        transform.Rotate(0f, 90f, 0f);
    }


    void FixedUpdate()
    {
        if(transform.position != waypoints[cur].position)
        {
            Vector3 p = Vector3.MoveTowards(transform.position, waypoints[cur].position, speed);
            GetComponent<Rigidbody>().MovePosition(p);
        }
        else
        {
            cur = (cur + 1) % waypoints.Length;
        }
    }

    private void Awake()
    {
        Spawn();
    }

    public void Spawn()
    {
        transform.position = spawnPoint.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ReSpawn")
        {
            Spawn();
            transform.Rotate(0f, 180f, 0f);
            cur = 0;
        }

        if(other.gameObject.tag == "Left"){
            transform.Rotate(0f, -90f, 0f);
        }

        if(other.gameObject.tag == "Right"){
            transform.Rotate(0f, 90f, 0f);
        }

        if(other.gameObject.tag == "Spin"){
            transform.Rotate(0f, 180f, 0f);
        }
    }

}
