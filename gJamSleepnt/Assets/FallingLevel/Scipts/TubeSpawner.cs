using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tubes;
    [SerializeField] private float _tubeLength;

    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        Vector3 position = new Vector3(0, 0, transform.position.z + _tubeLength);
        Instantiate(_tubes[Random.Range(0, _tubes.Count)], position, Quaternion.identity);
    }
}
