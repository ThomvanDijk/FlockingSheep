using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SheepBehaviour : MonoBehaviour {

    private Vector2 position;
    private Vector2 velocity;
    private Vector2 acceleration;

    private float maxforce;         // Maximum steering force.
    private float maxspeed;         // Maximum speed.
    private float neighbordist;     // Detection range.
    private float separation;       // Separation between two boids.
    private float rotation;

    // Use this for initialization.
    void Start() {
        position = new Vector2(this.transform.position.x, this.transform.position.z);
        float angle = (Random.Range(0, 101) * 0.01f) * (Mathf.PI * 2);
        velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        acceleration = new Vector2(0, 0);

        maxforce = 0.1f;
        maxspeed = 0.05f;
        neighbordist = 120;    // Neighbor detection range.
        separation = 40;       // The distance for the seperation-force to apply.
    }

    // UpdateSheep is called from the FlockManager class.
    public void updateSheep(List<GameObject> sheepList) {
        flock(sheepList);
        updatePosition();
    }

    // We accumulate a new acceleration each time based on three rules.
    public void flock(List<GameObject> sheepList) {
        Vector2 sep = separate(sheepList);
        Vector2 ali = align(sheepList);
        Vector2 coh = cohesion(sheepList);

        // Arbitrarily weight these forces.
        sep = sep.multS(0.1f);
        ali = ali.multS(0.2f);
        coh = coh.multS(0.1f);

        // Add the force vectors to acceleration.
        applyForce(sep);
        applyForce(ali);
        applyForce(coh);
    }

    void applyForce(Vector2 force) {
        acceleration = acceleration.add(force);
    }

    // Method to update position.
    void updatePosition() {
        velocity = velocity.add(acceleration);

        rotation = velocity.getAngle() * (180 / Mathf.PI);
        // a * (180 / Mathf.PI); rad2deg
        // a * (Mathf.PI / 180); deg2rad

        //this.transform.rotation = Quaternion.EulerRotation();

        velocity = velocity.limit(maxspeed);
        position = position.add(velocity);

        acceleration = acceleration.multS(0); // Reset acceleration.

        this.transform.position = new Vector3(position.x, this.transform.position.y, position.y);
    }

    // Separation.
    // Method checks for nearby boids and steers away.
    public Vector2 separate(List<GameObject> sheepList) {
        Vector2 sum = new Vector2(0, 0);
        int count = 0;

        // For every boid in the system, check if it's too close.
        foreach (var other in sheepList) {
            float d = position.dist(other.GetComponent<SheepBehaviour>().position);

            // If the distance is greater than 0 and less than an arbitrary amount (0 when you are yourself).
            if ((d > 0) && (d < separation)) {
                // Calculate vector pointing away from neighbor.
                Vector2 locationcopy = new Vector2(position.x, position.y);
                Vector2 diff = locationcopy.sub(other.GetComponent<SheepBehaviour>().position);

                diff = diff.normalize();
                diff = diff.divS(d);       // Weight by distance.
                sum = sum.add(diff);
                count++;            // Keep track of how many.
            }
        }

        // Average -- divide by how many.
        if (count > 0) {
            sum = sum.divS(count);
            sum = sum.normalize();
        }

        // As long as the vector is greater than 0.
        if (sum.mag() > 0) {
            sum = sum.multS(maxspeed);
            Vector2 steer = sum.sub(velocity);
            steer = steer.limit(maxforce);
        }
        return sum;
    }

    // Alignment.
    // For every nearby boid in the system, calculate the average velocity.
    public Vector2 align(List<GameObject> sheepList) {
        Vector2 sum = new Vector2(0, 0);
        int count = 0;

        foreach (var other in sheepList) {
            float d = position.dist(other.GetComponent<SheepBehaviour>().position);
            if ((d > 0) && (d < neighbordist)) {
                sum = sum.add(other.GetComponent<SheepBehaviour>().velocity);
                count++;
            }
        }

        if (count > 0) {
            sum = sum.divS(count);
            sum = sum.normalize();
            sum = sum.multS(maxspeed);
            Vector2 steer = sum.sub(velocity);
            steer = steer.limit(maxforce);
            return steer;
        }

        else {
            return new Vector2(0, 0);
        }
    }

    // Cohesion.
    // For the average location (i.e. center) of all nearby boids, calculate steering vector towards that location.
    public Vector2 cohesion(List<GameObject> sheepList) {
        Vector2 sum = new Vector2(0, 0);
        int count = 0;
        foreach (var other in sheepList) {
            float d = position.dist(other.GetComponent<SheepBehaviour>().position);
            if ((d > 0) && (d < neighbordist)) {
                sum = sum.add(other.GetComponent<SheepBehaviour>().position); // Add location
                count++;
            }
        }
        if (count > 0) {
            sum = sum.divS(count);
            return seek(sum);  // Steer towards the location.
        }
        else {
            return new Vector2(0, 0);
        }
    }

    //Here you calculate the steering towards a target.
    public Vector2 seek(Vector2 target) {
        Vector2 targetcopy = new Vector2(target.x, target.y);
        Vector2 desired = targetcopy.sub(position);

        desired = desired.normalize();
        desired = desired.multS(maxspeed);
        Vector2 steer = desired.sub(velocity);
        steer = steer.limit(maxforce);

        return steer;
    }

}
