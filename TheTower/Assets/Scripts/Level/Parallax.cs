using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float height;
    private float startPosition;
    private float origin;
    private float parallaxEffect;
    float dist;
    float buildingPos;

    private void Start()
    {
        parallaxEffect = -6.0f;
        startPosition = transform.position.y;
        origin = transform.position.y;
        //height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    public void ElevatorUp()
    {
        Debug.Log("parallax::ElevatorUp()");
        dist = startPosition + parallaxEffect;
               
        transform.position = new Vector3(transform.position.x, dist, transform.position.z);

       BuildingPosition();
    }

    public void ResetParallax()
    {
        transform.position = new Vector3(0, origin, 0);
    }

    public void BuildingPosition() 
    {
       startPosition = dist;
    }

}
