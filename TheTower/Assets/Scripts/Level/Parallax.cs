using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float height;
    private float startPosition;
    private Vector3 origin;
    private float parallaxEffect;
    float dist;
    float buildingPos;

    private void Awake()
    {
        parallaxEffect = -6.0f;
        startPosition = transform.position.y;
        origin = transform.position;
    }

    private void Start()
    {
        GameManager.instance.OnCurrentFloorChanged += InteractParallax;
    }

    public void InteractParallax(int level) 
    {
        if (level != 0)
        {
            Vector3 initialPos = transform.position;
            Vector3 finalPos = new Vector3(initialPos.x, startPosition + parallaxEffect, initialPos.z);

            StartCoroutine(MoveParalax(initialPos , finalPos));
        }
        else
            StartCoroutine(MoveParalax(transform.position, origin));
    }

    IEnumerator MoveParalax(Vector3 initialPos, Vector3 finalPos)
    {
        float i = 0;

        Debug.Log("parallax::MoveParallax()");

        do
        {
            transform.position = Vector3.Lerp(initialPos , finalPos , i);
            i += 0.1f;
            yield return null;
        } while (transform.position != finalPos);
        
        BuildingPosition();
    }

    public void BuildingPosition() 
    {
        startPosition = transform.position.y;
    }
}
