using HoloLensXboxController;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Runner : MonoBehaviour {

    public GameObject StageDango;
    public GameObject StageBottle;
    public GameObject StageSakura;
    StageDango stageDango;
    StageBottle stageBottle;
    StageSakura stageSakura;
    WebSocketClient websocketClient;
    ControllerInput controllerInput;
    bool dangoTrigger = false;
    bool bottleTrigger = false;
    bool sakuraTrigger = false;

    void Awake() {
        controllerInput = new ControllerInput(0, 0.19f);
        websocketClient = new WebSocketClient(Configure.WebsocketUri);
        websocketClient.OnMessaged += WebsocketClient_OnMessaged;
    }    

    void Start () {
        stageDango = StageDango.GetComponent<StageDango>();
        stageBottle = StageBottle.GetComponent<StageBottle>();
        stageSakura = StageSakura.GetComponent<StageSakura>();
        websocketClient.Connect();
        websocketClient.Send("Connect");
    }
	
	void Update () {
        controllerInput.Update();
        if (controllerInput.GetButtonDown(ControllerButton.Y)) {
            PreProcess();
        }
        if (controllerInput.GetButtonDown(ControllerButton.X)) {
            StartStage();
        }

        if (dangoTrigger) {
            StartDango();
            dangoTrigger = false;
        }

        if (bottleTrigger) {
            StartBottle();
            bottleTrigger = false;
        }

        if (sakuraTrigger) {
            StartSakura();
            sakuraTrigger = false;
        }
	}

    public void PreProcess() {
        StageDango.SetTransparent(true);
        StageSakura.SetActive(false);

    }

    private void WebsocketClient_OnMessaged(object sender, WebSocketClient.WebSocketEventArgs e) {
        switch (e.Message) {
            case "dango":
                dangoTrigger = true;
                break;
            case "bottle":
                bottleTrigger = true;
                break;
            case "sakura_debug":
                sakuraTrigger = true;
                break;
        }        
    }

    void OnDestroy() {
        websocketClient.Disconnect();
    }

    public void StartStage() {
        Debug.Log("cherry");
        websocketClient.Send("start");
    }

    public void StartDango() {        
        stageDango.StartStage();
    }

    public void StartBottle() {
        stageBottle.StartStage();
    }

    public void StartSakura() {
        StageSakura.SetActive(true);        
        stageSakura.StartStage();
    }

    public void doDangoEndProcess() {
        websocketClient.Send("arrive");
    }

    bool isSakura = false;
    public void doBottleEndProcess() {
        Debug.Log("sakura");
        if (!isSakura) {
            websocketClient.Send("sakura");
            isSakura = true;
        }        
        sakuraTrigger = true;
    }
}
