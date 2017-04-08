using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DangoStopper : MonoBehaviour {

    public UnityEvent OnCollidered;

	void Start () {
		
	}
	
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision) {
        Debug.Log("collision");
        if (collision.gameObject.CompareTag("Player")) {
            OnCollidered.Invoke();
        }
    }
}
