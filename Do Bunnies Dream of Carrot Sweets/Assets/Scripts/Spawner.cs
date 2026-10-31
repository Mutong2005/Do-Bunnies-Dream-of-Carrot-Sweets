using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject objectToSpawn; // Drag your Prefab here
    [SerializeField] private float spawnInterval = 3.5f; // Time between spawns
        
    public Transform ParentStartPoint;
    public Transform ParentEndPoint;

    private void Start()
    {
        // Start the continuous spawn loop
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        // Spawn at start

            // Spawn the object
            GameObject newSpawnS = Instantiate(objectToSpawn, transform.position, Quaternion.identity);

            // 2. Grab the script component on the spawned object
            SpawnedBubble objectScriptS = newSpawnS.GetComponent<SpawnedBubble>();
             if (objectScriptS != null)
            {
                objectScriptS.EndPoint = ParentEndPoint;
                objectScriptS.StartPoint = ParentStartPoint;            
            }

        while (true)
        {
            // Wait for the specified interval
            yield return new WaitForSeconds(spawnInterval);

            // Spawn the object
            GameObject newSpawn = Instantiate(objectToSpawn, transform.position, Quaternion.identity);

            // 2. Grab the script component on the spawned object
            SpawnedBubble objectScript = newSpawn.GetComponent<SpawnedBubble>();
             if (objectScript != null)
            {
                objectScript.EndPoint = ParentEndPoint;
                objectScript.StartPoint = ParentStartPoint;            
            }
      }
    }
}