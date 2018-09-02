using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMng : MonoBehaviour
{
    [SerializeField] private GameObject meleePrefab;
    [SerializeField] private GameObject rangedPrefab;
    [SerializeField] private GameObject specialPrefab;
    [SerializeField] private GameObject ply;
    private static EnemyMng instance = null;

    public static EnemyMng Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update ()
    {
		
	}

    public void SpawnSpecial()
    {
        Instantiate(specialPrefab,ply.transform.position, Quaternion.identity);
    }
}
