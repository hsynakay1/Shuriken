using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBars : MonoBehaviour
{
   [SerializeField] private HealtRemember remember;

   public Image PlayerHBar;
   public Image PlayerABar;
   public Image EnemyHBar;
   public Image EnemyABar;

   private void Update()
   {
      PlayerHBar.fillAmount = remember.PlayerHealth / 100;
      PlayerABar.fillAmount = remember.PlayerArmor / 100;

      EnemyHBar.fillAmount = remember.EnemyHealth / 100;
      EnemyABar.fillAmount = remember.EnemyArmor / 100;

   }
}
