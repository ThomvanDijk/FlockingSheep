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

    // UpdateSheep is called from the FlockManager class.
    public void updateSheep(List<GameObject> sheepList) {
        flock(sheepList);
        updatePosition();
    }

    // We accumulate a new acceleration each time based on three rules.
    public void flock(List<GameObject> sheepList) {
        CustomVector2 sep = separate(sheepList);
        CustomVector2 ali = align(sheepList);
        CustomVector2 coh = cohesion(sheepList);

        // Arbitrarily weight these forces.
        sep.multS(1.8f);
        ali.multS(1.0f);
        coh.multS(1.0f);

        // Add the force vectors to acceleration.
        applyForce(sep);
        applyForce(ali);
        applyForce(coh);
    }
    
    void applyForce(CustomVector2 force) {
        acceleration.add(force);
    }
    
    // Method to update position.
    void updatePosition() {
        velocity.add(acceleration);

        rotation = CustomVector2.deg2rad(velocity.getAngle());

        velocity.limit(maxspeed);
        position.add(velocity);

        acceleration.multS(0);

        this.transform.position = new Vector3(position.x, this.transform.position.y, position.y);
    }

    // Separation.
    // Method checks for nearby boids and steers away.
    public CustomVector2 separate(List<GameObject> sheepList) {
        CustomVector2 sum = new CustomVector2(0, 0);
        int count = 0;

        // For every boid in the system, check if it's too close.
        foreach (var other in sheepList) {
            float d = position.dist(other.GetComponent<SheepBehaviour>().position);

            // If the distance is greater than 0 and less than an arbitrary amount (0 when you are yourself).
            if ((d > 0) && (d < separation)) {
                // Calculate vector pointing away from neighbor.
                CustomVector2 locationcopy = new CustomVector2(position.x, position.y);
                CustomVector2 diff = locationcopy.sub(other.GetComponent<SheepBehaviour>().position);

                diff.normalize();
                diff.divS(d);       // Weight by distance.
                sum.add(diff);
                count++;            // Keep track of how many.
            }
        }

        // Average -- divide by how many.
        if (count > 0) {
            sum.divS(count);
            sum.normalize();
        }

        // As long as the vector is greater than 0.
        if (sum.mag() > 0) {
            sum.multS(maxspeed);
            CustomVector2 steer = sum.sub(velocity);
            steer.limit(maxforce);
        }
        return sum;
    }

    // Alignment.
    // For every nearby boid in the system, calculate the average velocity.
    public CustomVector2 align(List<GameObject> sheepList) {
        CustomVector2 sum = new CustomVector2(0, 0);
        int count = 0;
        foreach (var other in sheepList) {
            float d = position.dist(other.GetComponent<SheepBehaviour>().position);
            if ((d > 0) && (d < neighbordist)) {
                sum.add(other.GetComponent<SheepBehaviour>().velocity);
                count++;
            }
        }
        if (count > 0) {
            sum.divS(count);
            sum.normalize();
            sum.multS(maxspeed);
            CustomVector2 steer = sum.sub(velocity);
            steer.limit(maxforce);
            return steer;
        }
        else {
            return new CustomVector2(0, 0);
        }
    }

    // Cohesion.
    // For the average location (i.e. center) of all nearby boids, calculate steering vector towards that location.
    public CustomVector2 cohesion(List<GameObject> sheepList) {
        CustomVector2 sum = new CustomVector2(0, 0);
        int count = 0;
        foreach (var other in sheepList) {
            float d = position.dist(other.GetComponent<SheepBehaviour>().position);
            if ((d > 0) && (d < neighbordist)) {
                sum.add(other.GetComponent<SheepBehaviour>().position); // Add location
                count++;
            }
        }
        if (count > 0) {
            sum.divS(count);
            return seek(sum);  // Steer towards the location.
        }
        else {
            return new CustomVector2(0, 0);
        }
    }

    //Here you calculate the steering towards a target.
    public CustomVector2 seek(CustomVector2 target) {
        CustomVector2 targetcopy = new CustomVector2(target.x, target.y);
        CustomVector2 desired = targetcopy.sub(position);

        desired.normalize();
        desired.multS(maxspeed);
        CustomVector2 steer = desired.sub(velocity);
        steer.limit(maxforce);

        return steer;
    }

}
