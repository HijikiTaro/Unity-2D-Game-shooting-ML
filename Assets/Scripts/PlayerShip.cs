using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;

using UnityEngine.SceneManagement;


public class PlayerShip : Agent
{
    public Transform firepoint;
    public GameObject bulletPrefab;

    AudioSource audioSource;
    public AudioClip shotSE;

    GameController gameController;

    public float moveSpeed = 2;


    Rigidbody2D ship;
    void Start()
    {
        ship = GetComponent<Rigidbody2D>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition);

        //objectというタグ名のゲームオブジェクトを複数取得したい時
        GameObject[] objects = GameObject.FindGameObjectsWithTag("enemy");
        //配列の要素一つ一つに対して処理を行う
        foreach (GameObject o in objects)
        {
            sensor.AddObservation(o.transform.localPosition);
        }
    }

    public Transform Target;
    public override void OnEpisodeBegin()
    {
            
        this.transform.localPosition = new Vector3(0, -1.3f, 0);

        //objectというタグ名のゲームオブジェクトを複数取得したい時
        GameObject[] objects = GameObject.FindGameObjectsWithTag("enemy");
        //配列の要素一つ一つに対して処理を行う
        foreach (Object o in objects)
        {
            Destroy(o);
        }


    }
    public override void OnActionReceived(float[] vectorAction)
    {
        var dirToGo = Vector3.zero;

        var shootCommand = false;
        var rightAxis = (int)vectorAction[0];
        var forwardAxis = (int)vectorAction[1];
        var shootAxis = (int)vectorAction[2];

        switch (rightAxis)
        {
            case 1:
                dirToGo = transform.right;
                break;
            case 2:
                dirToGo = -transform.right;
                break;
        }

        switch (forwardAxis)
        {
            case 1:
                dirToGo = transform.up;
                break;
            case 2:
                dirToGo = -transform.up;
                break;
        }

        switch (shootAxis)
        {
            case 1:
                shootCommand = true;
                break;
        }
        if (shootCommand)
        {
            Instantiate(bulletPrefab, firepoint.position, transform.rotation);
        }

        dirToGo *= 0.5f;

        Vector3 nextPostion = transform.position + dirToGo * Time.deltaTime * 4f;

        nextPostion = new Vector3(
            Mathf.Clamp(nextPostion.x, -2.9f, 2.9f),
            Mathf.Clamp(nextPostion.y, -2f, 2f),
            nextPostion.z
            );

        gameObject.transform.position = nextPostion;

        AddReward(-0.1f);

    }

    public override void Heuristic(float[] actionsOut)
    {

        actionsOut[0] = 0f;
        actionsOut[1] = 0f;
        actionsOut[2] = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            actionsOut[0] = 2f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            actionsOut[0] = 1f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            actionsOut[1] = 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            actionsOut[1] = 2f;
        }
        actionsOut[2] = Input.GetKey(KeyCode.Space) ? 1.0f : 0.0f;

        Debug.Log("[" + actionsOut[0] + ", " + actionsOut[1] + ", " + actionsOut[2] + "]");

    }

    public void PlayerAddReward(float reward)
    {
        AddReward(reward);
    }


    // Update is called once per frame
 /*   void Update()
    {
        Move();
        Shot();

    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 nextPostion = transform.position + new Vector3(x, y, 0) * Time.deltaTime * 4f;

        nextPostion = new Vector3(
            Mathf.Clamp(nextPostion.x, -2.9f, 2.9f),
            Mathf.Clamp(nextPostion.y, -2f, 2f),
            nextPostion.z
            );

        transform.position = nextPostion;

    }
    void Shot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, firepoint.position, transform.rotation);
            audioSource.PlayOneShot(shotSE);

        }

    }*/
}
