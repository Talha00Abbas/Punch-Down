using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spawnCars : MonoBehaviour
{
    public GameObject carPrefab;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    private void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(carPrefab.name, randomPosition, Quaternion.identity);
    }
}
