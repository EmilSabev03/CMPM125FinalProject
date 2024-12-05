//chatgpt helped with this script: https://chatgpt.com/share/67512e35-d894-8013-97b8-e8820ff16d4b

using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    [SerializeField] AudioSource laserSound;
    public GameObject laserPrefab;
    public GameObject laser2Prefab;
    public int lasersPerGroup = 5;  // Number of lasers to spawn in a group
    public Transform playerTransform;

    public float minZ = 19.49f;
    public float maxZ = 114.3588f;
    public float laserDistance = 30f;

    private bool alternateSpawns = false;
    private float lastSpawnX;
    private int lasersSpawnedThisGroup = 0; // Counter for lasers in the current group

    void Start()
    {
        // Spawn the initial group of lasers
        SpawnLaserGroup();

        lastSpawnX = playerTransform.position.x;
    }

    void Update()
    {
        if (playerTransform.position.x > lastSpawnX + laserDistance)
        {
            SpawnLaserGroup();  // Spawn a new group of lasers each time the player moves far enough
            lastSpawnX = playerTransform.position.x;
        }
    }

    void SpawnLaserGroup()
    {
        // Spawn 5 lasers in a group
        for (int i = 0; i < lasersPerGroup; i++)
        {
            SpawnLaser();
        }

        laserSound.Play();
    }

    void SpawnLaser()
    {
        float randomZ = Random.Range(minZ, maxZ);

        Vector3 spawnPosition = new Vector3(playerTransform.position.x + laserDistance, 0f, randomZ);

        if (alternateSpawns == false)
        {
            Instantiate(laserPrefab, spawnPosition, Quaternion.identity);
            alternateSpawns = true;
        }
        else
        {
            Instantiate(laser2Prefab, spawnPosition, Quaternion.identity);
            alternateSpawns = false;
        }

        lasersSpawnedThisGroup++;  // Increment the laser count for the current group
    }
}
