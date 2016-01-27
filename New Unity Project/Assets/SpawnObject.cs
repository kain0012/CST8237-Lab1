using UnityEngine;
using System.Collections;

public class SpawnObject : MonoBehaviour {

    public GameObject prefabToSpawn = null;
    // Use this for initialization
    GameObject spawnedObject;

    void Start () {
        spawnedObject = GameObject.Instantiate(prefabToSpawn) as GameObject;
        //the reason that you need to typecast it is because it inherits the Instantiate function
        //for the Object class
        spawnedObject.name = "Three Potions";
        spawnedObject.transform.parent = transform;
        //transform drives the hierchy
        // the transform that belongs to this object is now the transform that belongs to the
        // parent object
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 points = new Vector3(0, 0, 1);
        Vector3 parentPoints = new Vector3(spawnedObject.transform.parent.position.x, spawnedObject.transform.parent.position.y, spawnedObject.transform.parent.position.z);
        spawnedObject.transform.parent.Rotate(points);
        spawnedObject.transform.Rotate(points);

    }
}
