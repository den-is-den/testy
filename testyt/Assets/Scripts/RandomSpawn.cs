using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public float spawn_delay = 0.005f;

    public GameObject prefab;

    public Transform spawn_point;

    public Vector2 value;

    public int Count = 20;
    private bool start = true;
    void Update()
    {
        if (start)
        {
            StartCoroutine("Square_spawn");
            start = false;
        }
    }

    IEnumerator Square_spawn()
    {
        GameObject parent = new GameObject();
        for(int count = Count; count > 0; count--)
        {
            Vector2 pos = new Vector2(Random.Range(spawn_point.position.x - value.x, spawn_point.position.x + value.x), Random.Range(spawn_point.position.y - value.y, spawn_point.position.y + value.y));
            GameObject obj = Instantiate(prefab, pos, Quaternion.identity, parent.transform);
            yield return new WaitForSeconds(spawn_delay);
        }
    }
}
