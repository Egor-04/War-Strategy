using UnityEngine;

public class Building : MonoBehaviour
{
    public TeamGroup CurrentBuildingTeamGroup;
    public int BuildingID;

    private int _minID = 1;
    private int _maxID = 1000;

    private void Start()
    {
        BuildingID = Random.Range(_minID, _maxID);
    }

    private void Update()
    {
        
    }
}
