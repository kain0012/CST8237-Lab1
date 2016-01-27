using UnityEngine;
using System.Collections;

public class distancer : MonoBehaviour {
    GameObject spawnedObject;
    public GameObject prefabToSpawn = null;
    float x = 0;
    bool isIncreasing = true;
    public float movementSpeed = 0.5f;

    // Use this for initialization
    void Start () {
        spawnedObject = GameObject.Instantiate(prefabToSpawn) as GameObject;
        spawnedObject.name = "Four Potions";
        spawnedObject.transform.parent = transform;
        spawnedObject.transform.localPosition = Vector3.zero;

    }

    // Update is called once per frame
    void Update () {
        Vector3 points = new Vector3(0, 0, 1);
        Vector3 parentPoints = new Vector3(spawnedObject.transform.parent.position.x, spawnedObject.transform.parent.position.y, spawnedObject.transform.parent.position.z);
        spawnedObject.transform.parent.Rotate(points);
        spawnedObject.transform.Rotate(points);
        if (x > 2) {
            isIncreasing = false;
        } else if (x < -2){
            isIncreasing = true;
        }

        if (isIncreasing) x += (movementSpeed * Time.deltaTime);
        else x -= (movementSpeed * Time.deltaTime);
        
        spawnedObject.transform.localPosition = new Vector3(x,0,0);


    }
}
