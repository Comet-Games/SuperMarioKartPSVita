using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour {

    [SerializeField] private List<Transform> carTransformList;
    private List<CheckpointSingle> checkpointSingleList;
    private List<int> nextCheckpointSingleIndexList;

    public int Lap;

    private void Awake()
    {
        Transform checkpointsTransform = transform;

        checkpointSingleList = new List<CheckpointSingle>();
        foreach(Transform checkpointSingleTransform in checkpointsTransform)
        {
            CheckpointSingle checkpointSingle = checkpointSingleTransform.GetComponent<CheckpointSingle>();
            
            checkpointSingle.SetTrackCheckpoints(this);

            checkpointSingleList.Add(checkpointSingle);
        }

        nextCheckpointSingleIndexList = new List<int>();
        foreach(Transform carTransform in carTransformList)
        {
            nextCheckpointSingleIndexList.Add(0);
        }
    }

    public void CarThroughCheckpoint(CheckpointSingle checkpointSingle, Transform carTransform)
    {
        int nextCheckpointSingleIndex = nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)];
        if (checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex)
        {
            //correct checkpoint
            if (checkpointSingleList.IndexOf(checkpointSingle) == 0)
            {
                Lap++;
                Debug.Log("Lap: " + Lap);
            }
            Debug.Log("Correct");
            nextCheckpointSingleIndexList[carTransformList.IndexOf(carTransform)] 
                =  (nextCheckpointSingleIndex + 1) % checkpointSingleList.Count;
        }
        else
        {
            //wrong checkpoint
            Debug.Log("Wrong");
        }
    }
}
