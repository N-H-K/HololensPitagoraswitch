using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSakura : MonoBehaviour {

    public GameObject Anchor;

    public void StartStage() {
        Anchor.SetActive(false);
        gameObject.SetActive(true);
    }

	void Start () {        
	}
	
	void Update () {
		
	}
}
