using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CustomVector2 : MonoBehaviour {

    // properties
    public float x;
    public float y;

    // constructor
    public CustomVector2(float xx, float yy) {
        this.x = xx;
        this.y = yy;
    }

    // methods
    public CustomVector2 fromAngle(float angle) {
        angle *= (Mathf.PI / 180);
        this.x = Mathf.Cos(angle);
        this.y = Mathf.Sin(angle);
        return this;
    }

    public CustomVector2 copy() {
        return new CustomVector2(this.x, this.y);
    }

    // info (getters)
    public float mag() {
        float getal = (x * x) + (y * y);
        float pos = Mathf.Sqrt(getal);
        return pos;
    }

    public float dist(CustomVector2 other) {
        float p1 = this.x - other.x;
        float p2 = this.y - other.y;
        return Mathf.Sqrt(Mathf.Pow(p1, 2) + Mathf.Pow(p2, 2));
    }

    public CustomVector2 getNormalized() {
        CustomVector2 t = new CustomVector2(this.x, this.y);
        t.normalize();
        return t;
    }

    public CustomVector2 normalize() {
        float m = (float)this.mag();
        this.x = this.x / m;
        this.y = this.y / m;
        return this;
    }

    public CustomVector2 limit(float max) {
        if (this.mag() > max) {
            this.normalize();
            this.multS(max);
        }
        return this;
    }

    public float getAngle() {
        float angle = Mathf.Atan2(y, x);
        return angle * (180 / Mathf.PI);
    }

    // manipulators
    public CustomVector2 add(CustomVector2 other) {
        this.x += other.x;
        this.y += other.y;
        return this;
    }

    public CustomVector2 sub(CustomVector2 other) {
        this.x -= other.x;
        this.y -= other.y;
        return this;
    }

    public CustomVector2 mult(CustomVector2 other) {
        this.x *= other.x;
        this.y *= other.y;
        return this;
    }

    public CustomVector2 multS(float scalar) {
        this.x *= scalar;
        this.y *= scalar;
        return this;
    }

    public CustomVector2 div(CustomVector2 other) {
        this.x /= other.x;
        this.y /= other.y;
        return this;
    }

    public CustomVector2 divS(float scalar) {
        this.x /= scalar;
        this.y /= scalar;
        return this;
    }

    public static float rad2deg(float a) {
        return a * (180 / Mathf.PI);
    }

    public static float deg2rad(float a) {
        return a * (Mathf.PI / 180);
    }

}
