using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HoloLensXboxController;

public class PositionAdjust : MonoBehaviour, IFocusable {

    public float RotateSpeed = 2.0f;
    public float MoveSpeed = 0.01f;

    ControllerInput controllerInput;
    bool isSelected = false;
    bool isFocused = false;
    bool isTransparent = false;

    void Awake() {
        controllerInput = new ControllerInput(0, 0.19f);
    }	
	
    void Update () {
        controllerInput.Update();
        isSelected = checkIsSelected();
        if (isSelected) {
            changeTransform();
        }

        if (controllerInput.GetButtonDown(ControllerButton.B)) {
            gameObject.SetTransparent(!isTransparent);
        }
    }

    bool checkIsSelected() {
        if (controllerInput.GetButtonDown(ControllerButton.A)) {
            if (isFocused && !isSelected) {
                return true;
            }
            return false;
        }        
        return isSelected;
    }

    void changeTransform() {
        float moveHorizontal = 0.0f, moveVertical = 0.0f, moveDepth = 0.0f;
        if (controllerInput.GetButton(ControllerButton.RightShoulder)) {
            moveDepth = MoveSpeed * controllerInput.GetAxisLeftThumbstickY();
        } else {
            moveHorizontal = MoveSpeed * controllerInput.GetAxisLeftThumbstickX();
            moveVertical = MoveSpeed * controllerInput.GetAxisLeftThumbstickY();
        }
        var rotateAroundY = RotateSpeed * controllerInput.GetAxisRightThumbstickX();
        transform.Translate(moveHorizontal, moveVertical, moveDepth);
        transform.Rotate(0, -rotateAroundY, 0);
    }


    public void OnFocusEnter() {
        isFocused = true;
    }

    public void OnFocusExit() {
        isFocused = false;
    }
}
