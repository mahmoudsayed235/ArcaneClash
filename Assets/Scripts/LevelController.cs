using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [System.Serializable]
    public class EnemyType
    {
        public GameObject enemyPrefab;
        public int minDifficultyLevel;
        public int maxDifficultyLevel;
    }
    public float minTimeBetweenWaves = 5f; // Minimum time between waves
    public float maxTimeBetweenWaves = 20f; // Maximum time between waves
    public List<EnemyType> enemyTypes;
    public Transform[] spawnPoints;
    public int minGroupsPerWave = 1;
    public int maxGroupsPerWave = 5;

    public int minEnemiesPerGroup = 1;
    public int maxEnemiesPerGroup = 3;

    private int currentWaveNumber=1;
    private int currentDifficultyLevel = 1; // Set initial difficulty level
    bool isAlive = true;
    float timeBetweenWaves;
    UIController uiController;
    void Start()
    {

        uiController = GameObject.FindObjectOfType<UIController>();
        uiController.UpdateWaveNumber(1);
        timeBetweenWaves = Mathf.Lerp(maxTimeBetweenWaves, minTimeBetweenWaves, (float)currentDifficultyLevel / 10f);
        StartCoroutine(CreatingFunction());
    }

    IEnumerator CreatingFunction()
    {
        while (isAlive)
        {
            StartNextWave();
            print("timeBetweenWaves : " + timeBetweenWaves);
            yield return new WaitForSeconds(timeBetweenWaves);

        }
    }
    public void GameOver()
    {
        isAlive = false;
    }

    void StartNextWave()
    {
        int numGroups = CalculateNumGroups(); // Determine number of enemy groups for this wave
        print("numGroups : "+ numGroups);

        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints); // Create a list of available spawn points

        for (int i = 0; i < numGroups; i++)
        {
            SpawnEnemyGroup(availableSpawnPoints);
        }
    }

    int CalculateNumGroups()
    {
        // Calculate the number of groups based on the current difficulty level
        int numGroups = Mathf.Clamp(currentDifficultyLevel, minGroupsPerWave, maxGroupsPerWave);
        return numGroups;
    }

    void SpawnEnemyGroup(List<Transform> availableSpawnPoints)
    {
        // Determine number of enemies in the group based on difficulty level
        int numEnemies = Random.Range(minEnemiesPerGroup, maxEnemiesPerGroup + 1) * currentDifficultyLevel;

        // Select enemy types for this difficulty level
        List<GameObject> availableEnemies = new List<GameObject>();
        foreach (var enemyType in enemyTypes)
        {
            if (currentDifficultyLevel >= enemyType.minDifficultyLevel && currentDifficultyLevel <= enemyType.maxDifficultyLevel)
            {
                availableEnemies.Add(enemyType.enemyPrefab);
            }
        }

        // Randomly select an enemy type from available enemies
        GameObject enemyPrefab = availableEnemies[Random.Range(0, availableEnemies.Count)];

        // Randomly select spawn point without repeating
        int randomIndex = Random.Range(0, availableSpawnPoints.Count);
        Transform spawnPoint = availableSpawnPoints[randomIndex];
        availableSpawnPoints.RemoveAt(randomIndex); // Remove the chosen spawn point from the list of available spawn points

        
       
        // Instantiate multiple enemies of the selected type at spawn point
        for (int i = 0; i < numEnemies; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere;

            Vector3 spawnPosition = spawnPoint.position + randomOffset;
            
            spawnPosition.y = 0.2f;
            

            GameObject enemy=  Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, spawnPoint);
            //setting boundries

            spawnPosition = enemy.transform.localPosition;

            spawnPosition.x = Random.RandomRange(-0.45f, 0.45f);

            enemy.transform.localPosition = spawnPosition;
        }
        currentWaveNumber++;
        if (currentWaveNumber == 6)
        {
            IncreaseDifficulty();
        }

        uiController.UpdateWaveNumber(((currentDifficultyLevel-1)*5)+currentWaveNumber);
    }

    // You can call this method when you want to increase the difficulty level
    public void IncreaseDifficulty()
    {
        print("IncreaseDifficulty");
        currentWaveNumber = 1;
        currentDifficultyLevel++;
        timeBetweenWaves = Mathf.Lerp(maxTimeBetweenWaves, minTimeBetweenWaves, (float)currentDifficultyLevel / 10f);

    }

}
