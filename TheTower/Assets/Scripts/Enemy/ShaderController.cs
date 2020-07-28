using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderController : MonoBehaviour, iPoolable
{
    [Range(0, 0.5f)]
    public float spawnEffectSpeed;
    [Range(0, 0.5f)]
    public float despawnEffectSpeed;
    public Color effectColor;

    private Material shader;

    public void TriggerSpawn(Action<bool> callback = null) 
    {
        StartCoroutine(TriggerShader(0, 1, spawnEffectSpeed, success => { if (callback != null) callback.Invoke(success); }));
    }

    public void TriggerDespawn(Action<bool> callback = null) 
    {
        StartCoroutine(TriggerShader(1 , 0 , despawnEffectSpeed, success => { if (callback != null) callback.Invoke(success); }));
    }

    IEnumerator TriggerShader(float startingValue, float endValue, float speed, Action<bool> callback = null) 
    {
        float progress = 0.0f;
        float value;

        do
        {
            value = Mathf.Lerp(startingValue, endValue, progress);
            shader.SetFloat("_Fade", value);
            yield return null;

            progress += speed;

        } while (value != endValue);

        if (callback != null)
            callback.Invoke(true);
    }

    private void Awake()
    {
        if (shader == null)
            shader = GetComponent<SpriteRenderer>().material;

        shader.SetColor("_EdgeColor" , effectColor);
    }

    public void OnUnpool()
    {
        TriggerSpawn();
    }

    public void OnPool()
    {
        shader.SetFloat("_Fade" , 0);
    }
}
