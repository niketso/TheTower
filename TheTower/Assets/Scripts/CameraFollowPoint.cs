using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPoint : MonoBehaviour {

    private bool followingPlayer;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        followingPlayer = false;
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
}
