using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public static class ExtensionMethods {

    public static Vector2 fromAngle(this Vector2 vector2, float angle) {
        angle *= (Mathf.PI / 180);
        vector2.x = Mathf.Cos(angle);
        vector2.y = Mathf.Sin(angle);
        return vector2;
    }

    public static Vector2 copy(this Vector2 vector2) {
        return new Vector2(vector2.x, vector2.y);
    }

    public static float mag(this Vector2 vector2) {
        float number = (vector2.x * vector2.x) + (vector2.y * vector2.y);
        float pos = Mathf.Sqrt(number);
        return pos;
    }

    public static float dist(this Vector2 vector2, Vector2 other) {
        float p1 = vector2.x - other.x;
        float p2 = vector2.y - other.y;
        return Mathf.Sqrt(Mathf.Pow(p1, 2) + Mathf.Pow(p2, 2));
    }

    public static Vector2 getNormalized(this Vector2 vector2) {
        Vector2 t = new Vector2(vector2.x, vector2.y);
        t.normalize();
        return t;
    }

    public static Vector2 normalize(this Vector2 vector2) {
        float m = (float)vector2.mag();
        vector2.x = vector2.x / m;
        vector2.y = vector2.y / m;
        return vector2;
    }

    public static Vector2 limit(this Vector2 vector2, float max) {
        if (vector2.mag() > max) {
            vector2.normalize();
            vector2.multS(max);
        }
        return vector2;
    }

    public static float getAngle(this Vector2 vector2) {
        float angle = Mathf.Atan2(vector2.y, vector2.x);
        return angle * (180 / Mathf.PI);
    }

    public static Vector2 add(this Vector2 vector2, Vector2 other) {
        vector2.x += other.x;
        vector2.y += other.y;
        return vector2;
    }

    public static Vector2 sub(this Vector2 vector2, Vector2 other) {
        vector2.x -= other.x;
        vector2.y -= other.y;
        return vector2;
    }

    public static Vector2 mult(this Vector2 vector2, Vector2 other) {
        vector2.x *= other.x;
        vector2.y *= other.y;
        return vector2;
    }

    public static Vector2 multS(this Vector2 vector2, float scalar) {
        vector2.x *= scalar;
        vector2.y *= scalar;
        return vector2;
    }

    public static Vector2 div(this Vector2 vector2, Vector2 other) {
        vector2.x /= other.x;
        vector2.y /= other.y;
        return vector2;
    }

    public static Vector2 divS(this Vector2 vector2, float scalar) {
        vector2.x /= scalar;
        vector2.y /= scalar;
        return vector2;
    }

}
