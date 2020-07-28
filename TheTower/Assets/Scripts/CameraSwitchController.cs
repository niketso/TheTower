using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchController : MonoBehaviour
{

    private Animator anim;
    private CameraFollowPoint currentFollowPoint;

    public List<CameraFollowPoint> followPoints = new List<CameraFollowPoint>();

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.instance.OnCurrentFloorChanged += ChangeToNextLevel;

        if (followPoints.Count > 0) 
        {
            currentFollowPoint = followPoints[GameManager.instance.CurrentFloor];
            currentFollowPoint.SetFollowing(true);
        }
        else
            Debug.LogError("CameraSwitchController::Start::The list of follow points is empty");
    }

    public void ChangeToNextLevel(int level) 
    {
        anim.Play($"CameraSwitch{level}");

        if (followPoints.Count <= level)
            Debug.LogError("CameraSwitchController::ChangeToNextLevel::The desired followPoint does not exist");
        else 
        {
            currentFollowPoint.SetFollowing(false);
            currentFollowPoint = followPoints[level];
            currentFollowPoint.SetFollowing(true);
        }
    }
}
