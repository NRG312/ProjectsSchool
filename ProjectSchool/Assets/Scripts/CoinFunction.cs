using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFunction : Singleton<CoinFunction>
{
    public GameObject Coinprefab;

    public void SpawnCoin()
    {
        GameObject Coin = Instantiate(Coinprefab, PipeSpawner.newPipe.transform);
        Coin.transform.position = PipeSpawner.newPipe.transform.position;
    }
}
