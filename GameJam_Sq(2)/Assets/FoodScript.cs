using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    public float cookingTime = 5;   //Cooking time in seconds
    public GameObject objectToSpawnIfCooked = null;
    public GameObject objectToSpawnIfChopped = null;
    public int numOfObjectsToSpawnIfChopped = 2;
    public bool destroyOutOfScreen = false;

    private int numOfObjectsToSpawnIfCooked = 1;
    private GameObject kitchen;
    private bool cooking = false;
    private bool chopping = false;
    const float edge = 2;
    private Outline outline;


    private void Start()
    {
        kitchen = GameObject.Find("Kitchen_Items");

        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.black;
        outline.OutlineWidth = 10f;
        outline.enabled = false;
    }

    private void Update()
    {
        if (chopping)
        {
            RaycastHit hit;
            if (Input.GetKeyDown(Const.MOUSE_RIGHT_BUTTON) && Physics.Raycast(transform.position, Vector3.up, out hit, 5))
            {
                if (hit.collider.CompareTag("Kitchen Knife"))
                {
                    Spawn(objectToSpawnIfChopped, numOfObjectsToSpawnIfChopped);
                    Destroy(gameObject);
                }
            }
        }
        
    }

    private void Spawn(GameObject objectToSpawn, int numOfObjectsToSpawn)
    {
        for (int i = 0; i < numOfObjectsToSpawn; i++)
        {
            Instantiate(objectToSpawn,
                new Vector3(transform.position.x, transform.position.y + i * edge, transform.position.z),
                objectToSpawn.transform.localRotation,
                kitchen.transform);
        }
    }

    //public void EnableOutline(bool _enabled)
    //{
    //    outline.enabled = _enabled;
    //}


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Pan"))
        {
            if (objectToSpawnIfCooked != null)
            {
                cooking = true;
                StartCoroutine(Cooking());
            }
        }
        
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Pan"))
        {
            if (objectToSpawnIfCooked != null)
            {
                cooking = false;
            }
        }
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Board"))
        {
            if (objectToSpawnIfChopped != null)
            {
                chopping = true;
            }
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Board"))
        {
            if (objectToSpawnIfChopped != null)
            {
                chopping = false;
            }
        }
    }


    IEnumerator Cooking()
    {
        double time = 0;
        while (time < cookingTime)
        {
            if (!cooking) yield break;
            time += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        Spawn(objectToSpawnIfCooked, numOfObjectsToSpawnIfCooked);
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        if(destroyOutOfScreen)
            Destroy(gameObject);
    }
}
