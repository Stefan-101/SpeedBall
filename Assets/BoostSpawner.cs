using UnityEngine;

public class BoostSpawner : MonoBehaviour
{
    public GameObject boostPrefab;
    public float spawnInterval = 10f;
    public Vector2 spawnAreaMin = new Vector2(-20, -10);
    public Vector2 spawnAreaMax = new Vector2(20, 10);

    private void Start()
    {
        InvokeRepeating(nameof(SpawnBoost), 3f, spawnInterval);
    }

    void SpawnBoost()
    {
        Vector2 spawnPos = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );
        GameObject boostOrb = Instantiate(boostPrefab, spawnPos, Quaternion.identity);
        boostOrb.tag = "CloneBoostOrb";
    }
}
