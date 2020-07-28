using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPoint : MonoBehaviour {

    public bool followingPlayer;
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
        {
            Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y + 2, 0);
            gameObject.transform.position = newPos;
        }
    }

    public void SetFollowing(bool following)
    {
        followingPlayer = following;

        if (!following)
            transform.position = startingPosition;
    }
}
