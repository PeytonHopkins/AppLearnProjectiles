using UnityEngine;

public class RandomObjectInstantiation : MonoBehaviour
{
    public GameObject prefab1; 
    public GameObject prefab2;
    public float minX = -5f; 
    public float maxX = 5f; 
    public float minY = -3f;
    public float maxY = 3f;

    // Start is called before the first frame update
    void Start()
    {
       
        InvokeRepeating("RandomInstantiate", 10f, 15f);
    }

    
    void RandomInstantiate()
    {
        
        int prefabIndex = Random.Range(0, 2);

       
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector3 randomPosition = new Vector3(randomX, randomY, 0f);

        
        if (prefabIndex == 0)
        {
            Instantiate(prefab1, randomPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(prefab2, randomPosition, Quaternion.identity);
        }
    }
}
