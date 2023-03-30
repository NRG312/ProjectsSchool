using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public float repeatRate = 1;
    private float timer = 0;
    public float height = 5;
    public GameObject[] prefabPipe;

    [HideInInspector]public static GameObject newPipe;

    // Update is called once per frame
    void Update()
    {
        if (timer > repeatRate)
        {
            timer = 0;
            SpawnPipe();
        }

        timer += Time.deltaTime;
    }

    private void SpawnPipe()
    {
        int RandomCreate = Random.Range(0, prefabPipe.Length);
        newPipe = Instantiate(prefabPipe[RandomCreate]);
        newPipe.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
        CoinFunction.Instance.SpawnCoin();
        Destroy(newPipe, 10f);
    }
}
