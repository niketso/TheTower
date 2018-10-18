using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundaries : MonoBehaviour {

    [SerializeField] private GameObject player;


    private void Update()
    {
        if (player.transform.position.x <= -4.3f)
        {
            transform.position = new Vector2(-4.3f, transform.position.y);
        }
        else if (transform.position.x >= 4.3f)
        {
            transform.position = new Vector2(4.3f, transform.position.y);
        }

        // Y axis
        if (transform.position.y <= -2.7f)
        {
            transform.position = new Vector2(transform.position.x, -2.7f);
        }
        else if (transform.position.y >= 2.7f)
        {
            transform.position = new Vector2(transform.position.x, 2.7f);
        }
    }

}
