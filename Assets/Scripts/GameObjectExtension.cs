using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension {
    
    public static void SetTransparent(this GameObject gameObject, bool transparent, bool needSetChildrens = true) {
        if (gameObject == null) {
            return;
        }

        if (gameObject.GetComponent<Renderer>()) {
            var c = gameObject.GetComponent<Renderer>().material.color;
            float alpha = transparent ? 0.0f : 1.0f;
            gameObject.GetComponent<Renderer>().material.color = new Color(c.r, c.g, c.b, alpha);
        }

        if (!needSetChildrens) {
            return;
        }

        foreach (Transform childTransform in gameObject.transform) {
            SetTransparent(childTransform.gameObject, transparent, needSetChildrens);
        }
    }
}
