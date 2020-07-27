using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderController : MonoBehaviour, iPoolable
{
    public float maxValue = 0;
    [Range(0, 1)]
    public float spawnEffectSpeed;
    public Color effectColor;

    private Material shader;

    public void TriggerSpawn() 
    {
        StartCoroutine(SpawnEffect());
    }

    IEnumerator SpawnEffect() 
    {
        for (float i = 0; i < maxValue; i += spawnEffectSpeed) 
        {
            shader.SetFloat("_Fade" , i);
            yield return null;
        }
    }

    public void OnUnpool()
    {
        if(shader == null)
            shader = GetComponent<SpriteRenderer>().material;

        shader.SetColor("_EdgeColor" , effectColor);

        TriggerSpawn();
    }

    public void OnPool()
    {
        shader.SetFloat("_Fade" , 0);
    }
}
