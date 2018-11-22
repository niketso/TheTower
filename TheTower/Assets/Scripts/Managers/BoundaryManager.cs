using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour {

    private static BoundaryManager instance = null;

    [SerializeField] private Transform[] rightLimits;
    [SerializeField] private Transform[] leftLimits;
    [SerializeField] private PlayerMovement player;
    private int i = 0;

    public Transform[] RightLimits
    {
        get { return rightLimits; }
    }

    public Transform[] LeftLimits
    {
        get{ return leftLimits; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        
        player.RightLimit = rightLimits[i];
        player.LeftLimit = leftLimits[i];
    }

    public void ChangeLimits()
    {
        i++;
        player.RightLimit = rightLimits[i];
        player.LeftLimit = leftLimits[i];
    }

    public void ResetLimits()
    {
        i = 0;
        player.RightLimit = rightLimits[i];
        player.LeftLimit = leftLimits[i];
    }
}
