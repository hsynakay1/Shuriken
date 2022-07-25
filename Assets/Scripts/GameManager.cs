using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private HealtRemember remember;
    private int Index ;

    private void Start()
    {
        Index = 10;
    }

    public void DeathSomeOne()
    {
        Index--;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
    }

    public void RestartPlayer()
    {
        DeathSomeOne();
        gameObject.SetActive(false);
        transform.position = new Vector3(Random.Range(-48f, 48f), 0.1f, Random.Range(-48f, 48f));
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(true);
        remember.PlayerArmor = 100;
        remember.PlayerHealth = 100;
    }
    private void Update()
    {
        if (Index == 0) GameOver();
        if (remember.PlayerHealth <= 0) RestartPlayer();
        
        Debug.Log(Index);
    }
}
