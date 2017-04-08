using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageBottle : MonoBehaviour {

    public GameObject Water;
    public GameObject Glass;
    public GameObject SakuraParticle;
    public float BottleDelayTime = 3.0f;
    public float ParticleDelayTime = 2.0f;
    public UnityEvent OnEndEvent;

    Glass glass;
    bool startTrigger = false;

	void Start () {
        glass = Glass.GetComponent<Glass>();
        Water.SetActive(false);
        SakuraParticle.SetActive(false);
	}

    public void StartStage() {
        Water.SetActive(true);
        glass.StartAnimation();
        startTrigger = true;
        SakuraParticle.SetActive(false);
    }

    void Update () {
        StartCoroutine(process());
	}

    IEnumerator process() {
        if (startTrigger) {
            if (glass.IsAnimation) {
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("bottle done");
            yield return StartCoroutine(delay(BottleDelayTime));
            Debug.Log("sakura start");
            SakuraParticle.SetActive(true);
            Debug.Log("sakura paticle");
            yield return StartCoroutine(delay(ParticleDelayTime));
            Debug.Log("Done");
            OnEndEvent.Invoke();
        }
    }

    IEnumerator delay(float sec) {
        yield return new WaitForSeconds(sec);
    }
}
