using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


public class ShurikenDamage : MonoBehaviour
{
    [SerializeField] private HealtRemember memoryHealtRemember;
    private int damage;
    private int value;
    private int armorPiercing;
    
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("enemyShuriken"))
        {
            damage = Random.Range(10, 50);
            value = Random.Range(0, 50);
            armorPiercing = (damage * value) / 100;
            damage = damage - armorPiercing;
            if (memoryHealtRemember.PlayerArmor <= 0)
            {
                memoryHealtRemember.PlayerHealth -= damage + armorPiercing;  
            }
            else
            {
                memoryHealtRemember.PlayerArmor -= damage;
                memoryHealtRemember.PlayerHealth -= armorPiercing;   
            }

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy") && !gameObject.CompareTag("enemyShuriken"))
        {
            damage = Random.Range(10, 50);
            value = Random.Range(0, 50);
            armorPiercing = (damage * value) / 100;
            damage = damage - armorPiercing;
            if (memoryHealtRemember.EnemyArmor <= 0)
            {
                memoryHealtRemember.EnemyHealth -= damage + armorPiercing;
            }
            else
            {
                memoryHealtRemember.EnemyArmor -= damage;
                memoryHealtRemember.EnemyHealth -= armorPiercing;   
            }
            ObjectPooling.Instance.SetPoolObject(gameObject,0);
        }

        if (gameObject.CompareTag("enemyShuriken") && collision.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("enemyShuriken") && collision.gameObject.CompareTag("wall"))
        {
            ObjectPooling.Instance.SetPoolObject(gameObject,0);
        }
    }
   
}
