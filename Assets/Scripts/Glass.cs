using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour {

    public float UpSpeed = 0.00000000000001f;
    public float MaxPositionY = 0.046f;
    public bool IsAnimation { get; private set; }

    public void StartAnimation() {
        IsAnimation = true;
    }

    void Awake() {
        IsAnimation = false;    
    }

    void Start () {		
	}

    void Update() {
        if (IsAnimation) {
            Vector3 position = transform.position;
            var diff = UpSpeed * Time.deltaTime;
            position.y += diff;
            transform.position = position;
            
            if (position.y >= MaxPositionY) {
                IsAnimation = false;
            }            
        }
    }

}
