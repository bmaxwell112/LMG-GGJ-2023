using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    [SerializeField] Cloud cloudPrefab;
    [SerializeField] float cloudSpawnRate = 0;
    [SerializeField] bool cloudsOn = true;
    [SerializeField] float initialClouds = 5;
    [SerializeField] Vector2 cloudHeight = new Vector2(3f, 11f);
    [SerializeField] Vector2 cloudSpawnRateMinMax = new Vector2(5f, 7f);
    [SerializeField] Vector2 speedMinMax = new Vector2(0.5f, 2f);    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < initialClouds; i++)
        {
            float startX = Random.Range(-13f, 13f);
            AddCloud(startX);
        }
        StartCoroutine(SpawnClouds());
    }

    private IEnumerator SpawnClouds(){
        while(cloudsOn)
        {
            //RANDOM NUMBERS
            float startX = -30f;
            AddCloud(startX);
            yield return new WaitForSeconds(cloudSpawnRate);
        }
    }

    private void AddCloud(float startX){

        cloudSpawnRate = Random.Range(cloudSpawnRateMinMax.x, cloudSpawnRateMinMax.y);
        float startY = Random.Range(cloudHeight.x, cloudHeight.y);
        float speed = Random.Range(speedMinMax.x, speedMinMax.y);

        Vector3 startPos = new Vector3(startX, startY, 0);
        Cloud cloud = Instantiate(cloudPrefab, startPos, Quaternion.identity);
        cloud.Initialize(speed);
        cloud.transform.parent = this.transform;
        cloudSpawnRate = Random.Range(3f, 5f);
    }
}
