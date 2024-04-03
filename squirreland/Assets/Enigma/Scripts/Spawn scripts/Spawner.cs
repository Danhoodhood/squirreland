using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tetrisObject;
    
    void Start()
    {
        SpawnRandom();
    }
    public void SpawnRandom()
    {
        int index = Random.Range(0, tetrisObject.Length); 
        Instantiate(tetrisObject[index], transform.position, Quaternion.identity);
    }


}
