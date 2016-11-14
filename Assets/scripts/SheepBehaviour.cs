using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SheepBehaviour : MonoBehaviour {

    private Vector2 position;
    private Vector2 velocity;
    private Vector2 acceleration;
    private Vector2 collide;
    private Animator animator;
    private float maxForce;         // Maximum steering force.
    private float fixedMaxSpeed;
    private float maxSpeed;         // Maximum speed.
    private float neighborDist;     // Detection range.
    private float separation;       // Separation between two boids.
    private float dogDetection;     // Range where the sheep sees the dog.
    private float dogSepDist;       // Distance for seperation from dog.
    private float obstSepDist;      // Distance for obstacle seperation.
    private float rotation;
    private float rotationSpeed;

    private bool graze;
    private bool collision;

    // Use this for initialization.
    void Start() {
        position = new Vector2(this.transform.position.x, this.transform.position.z);
        float angle = (Random.Range(0, 101) * 0.01f) * (Mathf.PI * 2);
        velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        acceleration = new Vector2(0, 0);
        collide = new Vector2(0, 0);
        animator = GetComponent<Animator>();

        maxForce = 0.01f;
        fixedMaxSpeed = 0.08f;
        maxSpeed = fixedMaxSpeed;
        neighborDist = 8;   // Neighbor detection range always needs to be higher than separation.
        separation = 2;     // The distance for the seperation-force to apply.
        dogDetection = 15;
        dogSepDist = 2.5f;
        obstSepDist = 4;
        rotation = 0;
        rotationSpeed = 2;

        graze = false;
        collision = false;
    }

    // UpdateSheep is called from the FlockManager class.
    public void updateSheep(List<GameObject> sheepList, GameObject sheepdog) {
        flock(sheepList, sheepdog);
        updatePosition();
    }

    // We accumulate a new acceleration each time based on three rules.
    public void flock(List<GameObject> sheepList, GameObject sheepdog) {
        // Dependent on the distance of the dog the sheep walk faster or not.
        float distanceFromDog = position.dist(new Vector2(sheepdog.transform.position.x, sheepdog.transform.position.z));

        if (distanceFromDog > dogDetection) {
            maxSpeed = 0;
        }
        else {
            maxSpeed = map(distanceFromDog, 0, dogDetection, fixedMaxSpeed, 0);

            if (maxSpeed < 0.01f) {
                maxSpeed = 0;
            }
        }

        Vector2 dog = separate(null, sheepdog);
        Vector2 sep = separate(sheepList, null);
        Vector2 ali = align(sheepList);
        Vector2 coh = cohesion(sheepList);
        //Vector2 grass = cohesion(grassList);

        // If there is a collision with a solid object.
        if (collision) {
            collide = collide.multS(0.8f);
        }

        // Arbitrarily weight these forces.
        dog = dog.multS(0.5f);
        sep = sep.multS(0.3f);
        ali = ali.multS(0.2f);
        coh = coh.multS(0.2f);

        // Add the force vectors to acceleration.
        applyForce(collide);
        applyForce(dog);
        applyForce(sep);
        applyForce(ali);
        applyForce(coh);
    }

    private void applyForce(Vector2 force) {
        acceleration = acceleration.add(force);

        // This statement filters NaN valuse in acceleration.
        if (float.IsNaN(acceleration.x) || float.IsNaN(acceleration.y)) {
            acceleration = new Vector2(0, 0);
            Debug.Log("Warning! Acceleration set to 0 because acceleration was NaN!");
        }
    }

    // Method to update position.
    private void updatePosition() {
        velocity = velocity.add(acceleration);
        velocity = velocity.limit(maxSpeed);
        position = position.add(velocity);   
        acceleration = acceleration.multS(0);   // Reset acceleration.

        // Rotate sheep in it's direction.
        float targetRotation = velocity.getAngle();
        targetRotation *= -1;
        targetRotation += 90;
        rotation = Mathf.LerpAngle(rotation, targetRotation, rotationSpeed * Time.deltaTime);
        this.transform.rotation = Quaternion.Euler(0, rotation, 0);

        // Check wich animation needs to be applied.
        checkAnimation();

        // Give this sheep a new position based on the velocity.
        this.transform.position = new Vector3(position.x, this.transform.position.y, position.y);
    }

    private void checkAnimation() {
        float speed = 0;

        if (velocity.mag() > 0.005f && velocity.mag() < 0.03f) {
            speed = 0.2f;
        }
        else if (velocity.mag() >= 0.03f) {
            speed = 0.6f;
        }
        else {
            speed = 0;
        }

        // Change is sheep is grazing.
        animator.SetBool("graze", graze);

        // Change the speed inside the animator.
        animator.SetFloat("speed", speed);
    }

    void OnTriggerEnter(Collider other) {
        // If the other object is not grass, sheep or sheepdog then collision is true.
        if (!other.CompareTag("Grass") && !other.CompareTag("Sheep") && !other.CompareTag("Sheepdog")) {
            collide = separate(null, other.gameObject);
            collision = true;
        }
    }

    void OnTriggerStay(Collider other) {
        // Check if the other object is indeed a sheep.
        if (other.CompareTag("Grass")) {

            if (other.GetComponent<GrassManager>().grassValue > 0 && velocity.mag() <= 0) {
                graze = true;
            }
            else {
                graze = false;
            }
        }
    }

    void OnTriggerExit(Collider other) {
        graze = false;
        collision = false;
    }

    // Separation.
    // Method checks for nearby boids and steers away.
    private Vector2 separate(List<GameObject> list, GameObject single) {
        Vector2 sum = new Vector2(0, 0);
        int count = 0;

        // If the argument is not a single object but a list...
        if (single == null) {
            // For every boid in the system, check if it's too close.
            foreach (var other in list) {
                float d = position.dist(other.GetComponent<SheepBehaviour>().position);

                // If the distance is greater than 0 and less than an arbitrary amount (0 when you are yourself).
                if ((d > 0.0f) && (d < separation)) {
                    // Calculate vector pointing away from neighbor.
                    Vector2 locationcopy = new Vector2(position.x, position.y);
                    Vector2 diff = locationcopy.sub(other.GetComponent<SheepBehaviour>().position);

                    diff = diff.normalize();
                    diff = diff.divS(d);        // Weight by distance.
                    sum = sum.add(diff);
                    count++;                    // Keep track of how many.
                }
            }
        }

        // If the argument is not a list but a single object...
        if (list == null) {
            float d = position.dist(new Vector2(single.transform.position.x, single.transform.position.z));

            // If the distance is greater than 0 and less than an arbitrary amount (0 when you are yourself).
            if (d < dogSepDist && single.CompareTag("Sheepdog")) {
                // Calculate vector pointing away from neighbor.
                Vector2 locationcopy = new Vector2(position.x, position.y);
                Vector2 diff = locationcopy.sub(new Vector2(single.transform.position.x, single.transform.position.z));

                diff = diff.normalize();
                diff = diff.divS(d);        // Weight by distance.
                sum = sum.add(diff);
                count++;                    // Keep track of how many.
            }
            // This is for seperation with obstacles.
            else if (d < obstSepDist) {
                // Calculate vector pointing away from neighbor.
                Vector2 locationcopy = new Vector2(position.x, position.y);
                Vector2 diff = locationcopy.sub(new Vector2(single.transform.position.x, single.transform.position.z));

                diff = diff.normalize();
                diff = diff.divS(d);        // Weight by distance.
                sum = sum.add(diff);
                count++;                    // Keep track of how many.
            }
        }

        // Average -- divide by how many.
        if (count > 0) {
            sum = sum.divS(count);
            sum = sum.normalize();
        }

        // As long as the vector is greater than 0.
        if (sum.mag() > 0) {
            sum = sum.multS(maxSpeed);
            Vector2 steer = sum.sub(velocity);
            steer = steer.limit(maxForce);
        }
        return sum;
    }

    // Alignment.
    // For every nearby boid in the system, calculate the average velocity.
    private Vector2 align(List<GameObject> sheepList) {
        Vector2 sum = new Vector2(0.0f, 0.0f);
        int count = 0;

        foreach (var other in sheepList) {
            float d = position.dist(other.GetComponent<SheepBehaviour>().position);
            if ((d > 0.0f) && (d < neighborDist)) {
                sum = sum.add(other.GetComponent<SheepBehaviour>().velocity);
                count++;
            }
        }

        if (count > 0) {
            sum = sum.divS(count);
            sum = sum.normalize();
            sum = sum.multS(maxSpeed);
            Vector2 steer = sum.sub(velocity);
            steer = steer.limit(maxForce);
            return steer;
        }

        else {
            return new Vector2(0.0f, 0.0f);
        }
    }

    // Cohesion.
    // For the average location (i.e. center) of all nearby boids, calculate steering vector towards that location.
    private Vector2 cohesion(List<GameObject> sheepList) {
        Vector2 sum = new Vector2(0.0f, 0.0f);
        int count = 0;
        foreach (var other in sheepList) {
            float d = position.dist(other.GetComponent<SheepBehaviour>().position);
            if ((d > 0.0f) && (d < neighborDist)) {
                sum = sum.add(other.GetComponent<SheepBehaviour>().position); // Add location
                count++;
            }
        }
        if (count > 0) {
            sum = sum.divS(count);
            return seek(sum);  // Steer towards the location.
        }
        else {
            return new Vector2(0.0f, 0.0f);
        }
    }

    // Here you calculate the steering towards a target.
    private Vector2 seek(Vector2 target) {
        Vector2 targetcopy = new Vector2(target.x, target.y);
        Vector2 desired = targetcopy.sub(position);

        desired = desired.normalize();
        desired = desired.multS(maxSpeed);
        Vector2 steer = desired.sub(velocity);
        steer = steer.limit(maxForce);

        return steer;
    }

    // map(973, 0, 1023, 0, 255); // returns: 242
    private float map(float x, float in_min, float in_max, float out_min, float out_max) {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

}
