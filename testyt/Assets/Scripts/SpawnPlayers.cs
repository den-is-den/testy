using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject anyCam;
    public float minX, minY, maxX, maxY;
    private double rdm;

    void Start()
    {
        double rdm = Random.Range(-10f, 10f);
        if (rdm > 0)
        {
            Vector2 randomPosition = new Vector2(Random.Range(minX, minY), Random.Range(maxX, maxY));
            PhotonNetwork.Instantiate(playerOne.name, randomPosition, Quaternion.identity);
        }
        else
        {
            Vector2 randomPosition = new Vector2(Random.Range(minX, minY), Random.Range(maxX, maxY));
            PhotonNetwork.Instantiate(playerTwo.name, randomPosition, Quaternion.identity);
        }
    }
}