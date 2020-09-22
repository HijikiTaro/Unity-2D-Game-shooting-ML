using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    public GameObject explosion;

    GameController gameController;
    PlayerShip playerShip;

    float offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = Random.Range(0, 3f*Mathf.PI);
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        playerShip = GameObject.Find("PlayerShip").GetComponent<PlayerShip>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(
            Mathf.Cos(Time.frameCount*0.05f + offset) *0.01f, 
            Time.deltaTime, 
            0
            );
        if (transform.position.y < -3)
        {
            Destroy(gameObject);
            playerShip.PlayerAddReward(-0.5f);
        };

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        {
            Instantiate(explosion, collision.transform.position, transform.rotation);

            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            gameController.ResetScore();
            playerShip.PlayerAddReward(-1.0f);
            playerShip.EndEpisode();
            //            gameController.GameOver();
        }
        else if (collision.CompareTag("Bullet") == true)
        {
            gameController.AddScore();
            playerShip.PlayerAddReward(1f);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }


    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    */

}
