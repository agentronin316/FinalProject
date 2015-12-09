using UnityEngine;
using System.Collections;

public enum EnemyAIType
{
    STATIONARY,
    HIDING,
    CONCEALING
}

public class ScriptEnemyData : MonoBehaviour {

    public EnemyAIType aiType = EnemyAIType.STATIONARY;
    public float moveSpeed = 0;
    public bool collectible = true;
    public int pointValue = 100;
    public float awakeDistance = 40f;
}
