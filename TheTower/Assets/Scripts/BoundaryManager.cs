using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour {

    private static BoundaryManager instance = null;

    [SerializeField] private Transform[] rightLimits;
    [SerializeField] private Transform[] leftLimits;
    private CameraBehaviour cam;
    private int i = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;

        cam = Camera.main.transform.GetComponent<CameraBehaviour>();

        cam.RightLimit = rightLimits[i];
        cam.LeftLimit = leftLimits[i];
    }

    public void ChangeLimits()
    {
        i++;
        cam.RightLimit = rightLimits[i];
        cam.LeftLimit = leftLimits[i];
    }
}
