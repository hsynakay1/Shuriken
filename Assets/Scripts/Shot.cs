using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;


public class Shot : MonoBehaviour
{
    
    public Queue<GameObject> Shurikens = new Queue<GameObject>();

    public Animator animator;
    public Transform attackPoint;
    public float attackPower = 250f;
    public float upwardPower = 50f;
    public Rigidbody shurikenRigitbody;

    public Button shootButton;

    private bool run;
    private GameObject shuriken;
    private Camera _camera;


    private void Start()
    {
        _camera = Camera.main;
    }

    public void ShootingShuriken()
    {
        animator.SetBool("shot", true);
        StartCoroutine(ShotTimer());
    }
    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator ShotTimer()
    {
        yield return new WaitForSeconds(0.84f);
        animator.SetBool("shot", false);
        shuriken = ObjectPooling.Instance.GetPoolObject(0);
        shuriken.transform.position = attackPoint.position;
        shuriken.GetComponent<Rigidbody>().velocity = attackPoint.forward * attackPower * Time.deltaTime;
        
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.gameObject.name == "FireButton")
                {
                    ShootingShuriken();
                }
            }
        }
    }
} 
