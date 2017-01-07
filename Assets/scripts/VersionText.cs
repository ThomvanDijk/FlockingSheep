using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class VersionText : MonoBehaviour {

    // OnGUI is called once per draw
    void OnGUI() {
        // Text alignment
        var style = GUI.skin.GetStyle("Label");
        style.alignment = TextAnchor.LowerLeft;

        GUI.skin.label.fontSize = 10;
        GUI.Label(new Rect(5, Screen.height - 100, 200, 100), "Version: 0.2.0");
    }

}
