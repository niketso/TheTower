using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splat : MonoBehaviour, iPoolable
{
    public Sprite[] splats;

    public float minSize;
    public float maxSize;

    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void OnPool()
    {
    }

    public void OnUnpool()
    {
        sprite.sprite = splats[Random.Range(0 , splats.Length)];

        float randomRot = Random.Range(0.0f, 360.0f);

        Vector3 randomSize = new Vector3(Random.Range(minSize, maxSize), Random.Range(minSize, maxSize), 1.0f);

        transform.rotation = Quaternion.Euler(0.0f , 0.0f , randomRot);
        transform.localScale = randomSize;
    }
}
