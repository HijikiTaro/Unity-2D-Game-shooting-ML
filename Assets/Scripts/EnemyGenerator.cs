using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    public GameObject enemyPefab;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 2f, 0.5f);
    }

    void Spawn()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-2.5f, 2.5f),
            transform.position.y,
            transform.position.z
            );

        Instantiate(enemyPefab, spawnPosition, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
