using UnityEngine;
using System.Collections;

public class ScriptEnemyMovement : MonoBehaviour {

    GameObject[] players;
    ScriptEnemyData data;

	// Use this for initialization
	void Start ()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        data = gameObject.GetComponent<ScriptEnemyData>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        bool awake = false;

        switch (data.aiType)
        {
            case EnemyAIType.STATIONARY:
                //Do nothing
                break;

            case EnemyAIType.CONCEALING:
                foreach (GameObject player in players)
                {
                    if ((player.transform.position - transform.position).magnitude > data.awakeDistance)
                    {
                        awake = true;
                    }
                }

                if (awake)
                {
                    //Find nearby higher value "enemy"
                    //Move in front of it
                }
                break;
                
            case EnemyAIType.HIDING:
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
                break;
        }
	}
}
