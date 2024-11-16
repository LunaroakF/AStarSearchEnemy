using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowHealthRecoverPath : MonoBehaviour
{
    public List<Transform> TargetRecovery;
    public HealthState healthState;
    public AStarSearch aStarSearch;
    public PathListMoveTo pathListMoveTo;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {
        pathListMoveTo = GetComponent<PathListMoveTo>();
        aStarSearch = GetComponent<AStarSearch>();
        healthState = GetComponent<HealthState>();
    }
    // Update is called once per frame
    void Update()
    {
        if (healthState != null)
        {
            if (healthState.HealthBlood <= 30 && aStarSearch.orderedPathPoints!=null)
            {
                if (TargetRecovery.Count > 0)
                {
                    int randomIndex = Random.Range(0, TargetRecovery.Count);
                    aStarSearch.TargetLandMark = TargetRecovery[randomIndex];
                    aStarSearch.enabled = true;
                    pathListMoveTo.pathPoints = aStarSearch.orderedPathPoints;
                    pathListMoveTo.enabled = true;
                    aStarSearch.enabled = false;
                    this.enabled = false;
                }
            }
        }
    }
}
