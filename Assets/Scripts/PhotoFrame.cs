using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoFrame : MonoBehaviour {

    public float UpSpeed;
    public float MaxUp = 1;

    float upDistance = 0;

    void Update() {
        Vector3 position = transform.position;
        var diff = UpSpeed * Time.deltaTime;
        position.y += diff;
        transform.position = position;
        upDistance += diff;

        if (upDistance >= MaxUp) {
            UpSpeed = 0;
        }
    }
}
