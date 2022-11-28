using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{


    public static GameManager Instance;
    [SerializeField] private int _currentLevel;
    [SerializeField] private float _currentLevelProgress;
    [SerializeField] public int CurrentAmount;
    [SerializeField] public int CurrentPowerLevel;
    [SerializeField] public int CurrentRangeLevel;
    [SerializeField] public int CurrentCapacityLevel;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    

    



}
