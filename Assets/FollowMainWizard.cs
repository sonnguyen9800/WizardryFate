﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMainWizard : MonoBehaviour
{
    public GameObject target; //the enemy's target
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.time);
    }
}