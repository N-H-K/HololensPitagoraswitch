using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDango : MonoBehaviour {

    public List<GameObject> BallPrefabs;
    public GameObject StartPoint;
    List<GameObject> balls;
    bool startTriggerFlag = false;

    public void StartStage() {
        startTriggerFlag = true;
    }

    void Awake() {
        balls = new List<GameObject>();
    }

	void Start () {
        instantiateBalls();        
	}

    void instantiateBalls() {
        foreach (var prefab in BallPrefabs) {
            var ball = Instantiate(prefab);
            ball.SetActive(false);
            balls.Add(ball);
        }
    }

	void Update () {
        if (startTriggerFlag) {
            StartCoroutine(startDango());
        }
	}

    IEnumerator startDango() {
        foreach (var ball in balls) {
            ball.transform.position = StartPoint.transform.position;
            ball.transform.rotation = Quaternion.identity;
            ball.SetActive(true);
            yield return new WaitForSeconds(0.7f);
        }
        startTriggerFlag = false;
    }
}
