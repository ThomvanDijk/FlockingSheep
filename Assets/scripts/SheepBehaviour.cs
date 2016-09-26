using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SheepBehaviour : MonoBehaviour {

    //private List<GameObject> sheepList;
    private Vector2 position;

    // Use this for initialization.
    void Start () {
        //sheepList = new List<GameObject>();
        position = new Vector2(this.transform.position.x, this.transform.position.y);
    }
	
	// Update is called once per frame.
	void Update() {
	
	}

    // UpdateSheep is called from the FlockManager class.
    public void updateSheep(List<GameObject> sheepList) {
        foreach (var t in sheepList) {
            //Debug.Log(t.transform.position.x);
        }
    }

}
