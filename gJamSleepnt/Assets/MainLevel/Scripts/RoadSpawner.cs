using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _roads;
    [SerializeField] private float _roadLength;
    private GameObject _road;
    
    void Start()
    {
        GameObject currentRoad = _roads[Random.Range(0, _roads.Count)];
        float currentRoadLength = currentRoad.transform.GetChild(0).lossyScale.z / 2;
        Vector3 position = new Vector3(0, 0, transform.position.z + currentRoadLength);
        _road = Instantiate(currentRoad, position, Quaternion.identity);
    }

    public void Spawn()
    {
        GameObject currentRoad = _roads[Random.Range(0, _roads.Count)];
        float currentRoadLength = currentRoad.transform.GetChild(0).lossyScale.z / 2;
        _roadLength = _road.transform.GetChild(0).lossyScale.z / 2;
        Vector3 position = new Vector3(0, 0, _road.transform.position.z + _roadLength + currentRoadLength);
        _road = Instantiate(currentRoad, position, Quaternion.identity);
    }
}
