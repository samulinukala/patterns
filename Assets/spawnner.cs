using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnner : MonoBehaviour
{
    public int ammounttoSpawn = 20;
    public float radius1 = 20;
    public float radius2 = 1;
    public float lifeTime = 5;
    public int ammounttoSpawn2;
    public Vector3 minPosition;
    public Vector3 maxPosition;
    public enemy enemyPrefab;
    public bool spawnBool;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void spawn()
    {
        if (spawnBool == true)
        {

            for (int i = 0; i < ammounttoSpawn; i++)
            {

                int rand = UnityEngine.Random.Range(0, 4);
                GameObject go = Instantiate(enemyPrefab, transform.position + UnityEngine.Random.insideUnitSphere * radius1, UnityEngine.Random.rotation).gameObject;

                Vector3 randomPosition = new Vector3(
                   UnityEngine.Random.Range(minPosition.x, maxPosition.x),
                   UnityEngine.Random.Range(minPosition.y, maxPosition.y),
                    UnityEngine.Random.Range(minPosition.z, maxPosition.z));

                go.GetComponent<Rigidbody>().AddForce(randomPosition, ForceMode.Impulse);
                go.GetComponent<MeshRenderer>().material.color = UnityEngine.Random.ColorHSV();
                Destroy(go, lifeTime);

            }
            for (int i = 0; i < ammounttoSpawn2; i++)
            {

                GameObject go = Instantiate(enemyPrefab, transform.position + UnityEngine.Random.insideUnitSphere * radius2, UnityEngine.Random.rotation).gameObject;
                go.GetComponent<MeshRenderer>().material.color = UnityEngine.Random.ColorHSV();
                Vector3 randomPosition = new Vector3(
                    UnityEngine.Random.Range(minPosition.x, maxPosition.x),
                    UnityEngine.Random.Range(minPosition.y, maxPosition.y),
                     UnityEngine.Random.Range(minPosition.z, maxPosition.z));

                go.GetComponent<Rigidbody>().AddForce(randomPosition, ForceMode.Impulse);
                Destroy(go, lifeTime);

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        spawn();
    }
}
