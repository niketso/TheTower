﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform rightLimit;
    private Camera cam;

    public Transform LeftLimit
    {
        get
        {
            return leftLimit;
        }

        set
        {
            leftLimit = value;
        }
    }

    public Transform RightLimit
    {
        get
        {
            return rightLimit;
        }

        set
        {
            rightLimit = value;
        }
    }

    private void Awake()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    void Update ()
    {
        Vector3 rightPos = cam.ViewportToWorldPoint(Vector3.one);
        Vector3 leftPos = cam.ViewportToWorldPoint(Vector3.zero);

        checkbounds(rightPos, leftPos);
	}

    void checkbounds(Vector3 right, Vector3 left)
    {
        if (right.x >= rightLimit.position.x)
            cam.transform.position = cam.transform.position - new Vector3(right.x - rightLimit.position.x, 0, 0);
        if (left.x <= leftLimit.position.x)
            cam.transform.position = cam.transform.position + new Vector3(leftLimit.position.x - left.x, 0, 0);
    }
}
