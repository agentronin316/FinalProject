using UnityEngine;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

public class ScriptEnemyMovement : MonoBehaviour {

    GameObject[] players;
    ScriptEnemyData data;

    Vector3 newPos;
    bool alive = true;
    bool posUpdated = false;

    List<GameObject> nearbyEnemies = new List<GameObject>();
	// Use this for initialization
	void Start ()
    {
        newPos = transform.position;
        players = GameObject.FindGameObjectsWithTag("Player");
        data = gameObject.GetComponent<ScriptEnemyData>();
        InitiateAI();
        if (data.aiType != EnemyAIType.STATIONARY)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 20f);
            foreach (Collider col in colliders)
            {
                if (col.gameObject.transform.tag == "Enemy" && col.gameObject != this.gameObject)
                {
                    nearbyEnemies.Add(col.gameObject);
                }
                
            }
        }
	}
	
    void Update()
    {
        transform.position = newPos;
        posUpdated = true;
    }

	void InitiateAI ()
    {
        Thread thread;
        switch (data.aiType)
        {
            case EnemyAIType.STATIONARY:
                //Do nothing
                break;

            case EnemyAIType.CONCEALING:
                thread = new Thread(AIConceal);
                thread.Start();
                break;
                
            case EnemyAIType.HIDING:
                thread = new Thread(AIHide);
                thread.Start();
                break;
        }
	}

    void AIConceal()
    {
        Stopwatch timer = new Stopwatch();
        long oldTime = timer.ElapsedMilliseconds;
        while (alive)
        {
            bool awake = false;
            foreach (GameObject player in players)
            {
                if ((player.transform.position - transform.position).magnitude > data.awakeDistance)
                {
                    awake = true;
                }
            }
            if (posUpdated)
            {
                if (awake)
                {
                    float tempTime = (timer.ElapsedMilliseconds - oldTime) / 1000f;
                    //Find nearby higher value "enemy"
                    //Move in front of it
                    timer = new Stopwatch();
                }
                else
                {
                    timer = new Stopwatch();
                }
                posUpdated = false;
            }
            Thread.Sleep(30);
        }
    }

    void AIHide()
    {
        while (alive)
        {
            bool awake = false;
            foreach (GameObject player in players)
            {
                if ((player.transform.position - transform.position).magnitude > data.awakeDistance)
                {
                    awake = true;
                }
            }

            if (awake)
            {
                //Find nearby lower value "enemy"
                //Move behind it
            }
            Thread.Sleep(30);
        }
    }
}
