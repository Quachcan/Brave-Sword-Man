using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyBaseData", menuName = "Data/EnemyBase Data/Base Data")]
public class D_EnemyBase : ScriptableObject
{
    public float maxHealth = 30f;

    public float wallCheckDistance =0.2f;
    public float ledgeCheckDistance = 0.4f;

    public float minAgroDistance = 10f;
    public float maxAgroDistance = 12f;

    public float closeRangeActionDistance = 1f;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
