﻿using UnityEngine;

public class Utils {
    public static float getDistanceBetweenGameObjects(GameObject g1, GameObject g2) {
        return Vector3.Magnitude(g1.transform.position - g2.transform.position);
    }
}