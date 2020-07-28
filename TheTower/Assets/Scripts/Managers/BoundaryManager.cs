using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour {

    public static BoundaryManager instance = null;

    public Transform[] rightLimits;
    public Transform[] leftLimits;

    [SerializeField] private PlayerMovement player;

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

        player.RightLimit = rightLimits[0];
        player.LeftLimit = leftLimits[0];
    }

    private void Start()
    {
        GameManager.instance.OnCurrentFloorChanged += ChangeLimits;
    }

    public void ChangeLimits(int level)
    {
        player.RightLimit = rightLimits[level];
        player.LeftLimit = leftLimits[level];
    }
}
