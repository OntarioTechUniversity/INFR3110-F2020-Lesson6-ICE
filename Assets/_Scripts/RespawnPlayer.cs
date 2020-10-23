using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public GameObject player;

    public Transform spawnPoint;
    public Transform enemySpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.gameObject.GetComponent<CharacterController>().enabled = false;
            player.transform.position = spawnPoint.position;
            player.transform.rotation = spawnPoint.rotation;
            player.gameObject.GetComponent<CharacterController>().enabled = true;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.transform.position = enemySpawnPoint.position;
            other.transform.rotation = enemySpawnPoint.rotation;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
