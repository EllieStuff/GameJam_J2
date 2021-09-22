using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomFood : MonoBehaviour
{
    GameObject[] listOfFoodPrefabs;


    // Start is called before the first frame update
    void Start()
    {
        listOfFoodPrefabs = Resources.LoadAll<GameObject>("Prefabs/Food");

        StartCoroutine(SpawnRandomFoodCoroutine());
    }


    IEnumerator SpawnRandomFoodCoroutine()
    {
        float minDelay = 1.0f, maxDelay = 6.0f, scaleInc = 20.0f;
        float delay = Random.RandomRange(minDelay, maxDelay);

        while (true)
        {
            yield return new WaitForSeconds(delay);
            delay = Random.RandomRange(minDelay, maxDelay);
            transform.position = new Vector3(Random.RandomRange(-9, 9), transform.position.y, transform.position.z);

            int rndFoodId = Random.RandomRange(0, listOfFoodPrefabs.Length);
            GameObject tmpGameObject = listOfFoodPrefabs[rndFoodId];
            Vector3 tmpScale = tmpGameObject.transform.localScale;
            tmpGameObject.transform.localScale = new Vector3(tmpScale.x * scaleInc, tmpScale.y * scaleInc, tmpScale.z * scaleInc);
            Instantiate(tmpGameObject, transform.position, Random.rotation);
            tmpGameObject.transform.localScale = tmpScale;

        }

    }

}
