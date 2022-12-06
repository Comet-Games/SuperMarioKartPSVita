using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public int lapAmount;
    public int laps;
    public KartController controller;
    private TrackCheckpoints trackCheckpoints;

    void Awake()
    {
        trackCheckpoints = GetComponentInChildren<TrackCheckpoints>();
    }

    void Update()
    {
        laps = trackCheckpoints.Lap;   
        if(trackCheckpoints.Lap > lapAmount)
        {
            
        }
    }
}
