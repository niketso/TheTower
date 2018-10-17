using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundaries : MonoBehaviour {

    [SerializeField] Vector3 pushDistance;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position -= pushDistance;
    }
}
