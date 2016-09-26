using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SheepBehaviour : MonoBehaviour {

    private CustomVector2 position;
    private CustomVector2 velocity;
    private CustomVector2 acceleration;

    private float maxforce;        //Maximum steering force.
    private float maxspeed;        //Maximum speed.
    private float neighbordist;     //Detection range.
    private float separation;       //Separation between two boids.
    private float rotation;

    private int diameter;

    // Use this for initialization.
    void Start () {
        position = new CustomVector2(this.transform.position.x, this.transform.position.z);
        velocity = new CustomVector2(0, 0);
        acceleration = new CustomVector2(0, 0);

        maxforce = 0.04f;
        maxspeed = 3.0f;
        neighbordist = 120;
        separation = 40;

        diameter = 32;
    }
	
	// Update is called once per frame.
	void Update() {
        velocity.add(acceleration);

        rotation = CustomVector2.deg2rad(velocity.getAngle());

        velocity.limit(maxspeed);
        position.add(velocity);

        acceleration.multS(0);

        this.transform.position = new Vector3(position.x, this.transform.position.y, position.y);
    }

    // UpdateSheep is called from the FlockManager class.
    public void updateSheep(List<GameObject> sheepList) {
        flock(sheepList);
    }

    // We accumulate a new acceleration each time based on three rules.
    public void flock(List<GameObject> sheepList) {
        //CustomVector2 sep = separate(sheepList);
        //CustomVector2 ali = align(sheepList);
        //CustomVector2 coh = cohesion(sheepList);

        // Arbitrarily weight these forces.
        //sep.multS(1.8f);
        //ali.multS(1.0f);
        //coh.multS(1.0f);

        // Add the force vectors to acceleration.
        //applyForce(sep);
        //applyForce(ali);
        //applyForce(coh);
    }

}
