using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance && instance != this)
            Destroy(this.gameObject);
        else 
        {
            instance = this;
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }

    [SerializeField] private GameObject player;
    public GameObject Player 
    {
        get 
        {
            if (!player)
                player = GameObject.FindGameObjectWithTag("Player");

            return player;
        }
        set { player = value; }
    }

    private int currentFloor;
    public Action<int> OnCurrentFloorChanged;
    public int CurrentFloor 
    {
        get => currentFloor;
        private set 
        {
            currentFloor = value;

            if (OnCurrentFloorChanged != null)
                OnCurrentFloorChanged.Invoke(currentFloor);
        }
    }

    public void ChangeFloor(int newFloor) 
    {
        CurrentFloor = newFloor;
    }

    [SerializeField] private ObjectPool meleePool;
    public ObjectPool MeleePool 
    {
        get => meleePool;
    }

    [SerializeField] private ObjectPool shotPool;
    public ObjectPool ShotPool 
    {
        get => shotPool;    
    }

    [SerializeField] private ObjectPool specialPool;
    public ObjectPool SpecialPool 
    {
        get => specialPool;
    }

    [SerializeField] private Transform leftLimit;
    public Transform LeftLimit 
    {
        get => leftLimit;
    }

    [SerializeField] private Transform rightLimit;
    public Transform RightLimit 
    {
        get => rightLimit;
    }

    private void Start()
    {
        currentFloor = 0;
    }
}
