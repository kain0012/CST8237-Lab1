using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Snake : MonoBehaviour {
    // Current Movement Direction
    // (by default it moves to the right)

    public GameObject foodPrefab;

    // Borders
    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    Vector2 dir = Vector2.right;
    public Text score;
    public Text lives;
    int scoreNum = 0;
    int livesNum = 3;
    // Keep Track of Tail
    List<Transform> tail = new List<Transform>();
    
    // Did the snake eat something?
    bool ate = false;

    // Tail Prefab
    public GameObject tailPrefab;

    // Use this for initialization
    void Start () {
        // Move the Snake every 300ms
        score.text = "Score: " + scoreNum;
        lives.text = "Lives: " + livesNum;
        InvokeRepeating("Move", 0.3f, 0.3f);
        Spawn();    
    }
    
    // Update is called once per Frame
    void Update() {
        // Move in a new Direction?
        if (Input.GetKey(KeyCode.RightArrow))
            dir = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow))
            dir = -Vector2.up;    // '-up' means 'down'
        else if (Input.GetKey(KeyCode.LeftArrow))
            dir = -Vector2.right; // '-right' means 'left'
        else if (Input.GetKey(KeyCode.UpArrow))
            dir = Vector2.up;
    }
    
    void Move() {
        // Save current position (gap will be here)
        Vector2 v = transform.position;

        // Move head into new direction (now there is a gap)
        transform.Translate(dir);

        // Ate something? Then insert new Element into gap
        if (ate) {
            // Load Prefab into the world
            GameObject g =(GameObject)Instantiate(tailPrefab,
                                                  v,
                                                  Quaternion.identity);
            

            // Keep track of it in our tail list
            tail.Insert(0, g.transform);

            // Reset the flag
            ate = false;
        }
        // Do we have a Tail?
        else if (tail.Count > 0) {
            // Move last Tail Element to where the Head was
            tail.Last().position = v;

            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count-1);
        }
    }
    
    void OnTriggerEnter2D(Collider2D coll) {
        // Food?
        if (coll.name.StartsWith("FoodPrefab")) {
            // Get longer in next Move call
            ate = true;
            
            // Remove the Food
            Destroy(coll.gameObject);
            scoreNum++;
            score.text = "Score: " + scoreNum;
            Spawn();
        }
        // Collided with Tail or Border
        else {
            int total = tail.Count;
            for (int i = total; i > 0; i--) {
                Destroy(tail.ElementAt(i-1).gameObject);
                tail.RemoveAt(i - 1);
            }
            transform.position = Vector2.zero;
            livesNum--;

            if (livesNum == 0) {
                scoreNum = 0;
                livesNum = 3;

            }
            score.text = "Score: " + scoreNum;
            lives.text = "Lives: " + livesNum;

        }
    }
    void Spawn()
    {
        // x position between left & right border
        int x = (int)Random.Range(borderLeft.position.x,
                                  borderRight.position.x);

        // y position between top & bottom border
        int y = (int)Random.Range(borderBottom.position.y,
                                  borderTop.position.y);

        // Instantiate the food at (x, y)
        Instantiate(foodPrefab,
                    new Vector2(x, y),
                    Quaternion.identity); // default rotation
    }
}