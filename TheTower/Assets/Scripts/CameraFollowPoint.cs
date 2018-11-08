using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPoint : MonoBehaviour {

    private bool followingPlayer;
    private Vector3 startingPosition;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        followingPlayer = false;
        startingPosition = transform.position;
    }

    private void Update()
    {
        if (followingPlayer)
            gameObject.transform.position = player.transform.position;
    }

    public void StartFollowing()
    {
        followingPlayer = true;
    }

    public void StopFollowing()
    {
        followingPlayer = false;
        transform.position = startingPosition;
    }
}
