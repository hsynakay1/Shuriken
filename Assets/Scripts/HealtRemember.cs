using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Memory", menuName = "HealthMemory")]
public class HealtRemember : ScriptableObject
{
    public float PlayerHealth = 100;
    public float PlayerArmor = 100;

    public float EnemyHealth = 100;
    public float EnemyArmor = 100;
}
