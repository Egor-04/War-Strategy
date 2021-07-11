using UnityEngine;

public class Building : MonoBehaviour
{
    [Header("Building Type")]
    public TeamGroup CurrentBuildingTeamGroup;
    
    [Header("ID")]
    public int BuildingID;

    [Header("Building Health")]
    public ObjectHealth _objectHealth;

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
