using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _allyPrefab;

    public void SpawnAlly()
    {
        Instantiate(_allyPrefab, transform.position, Quaternion.identity);
    }
}
