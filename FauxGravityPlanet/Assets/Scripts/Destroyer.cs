﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Destroyer : MonoBehaviour {

    [SerializeField, Range(5.0f, 100.0f)] float lifetime = 2.0f;
    [SerializeField, Range(10.0f, 60.0f)] float maxArea = 20.0f;

    public bool destroyWhenOut;
    void Awake()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {

        if (destroyWhenOut)
        {
            if (
            this.transform.position.x > maxArea || this.transform.position.x < -maxArea ||
            this.transform.position.y > maxArea || this.transform.position.y < -maxArea ||
            this.transform.position.z > maxArea || this.transform.position.z < -maxArea
            )
            {
                Destroy(gameObject);
            }
        }else {
        
            if (this.transform.position.y < -maxArea )
            {

                Destroy(gameObject);
            }        
        }
        
    }
}
