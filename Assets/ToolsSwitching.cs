using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ToolsSwitching : MonoBehaviour {
    public int selectedTool = 0;

    // Start is called before the first frame update
    void Start() {
        SelectedTool();
    }

    private void SelectedTool() {
        int i = 0;
        foreach (Transform tools in transform) {
            if (i == selectedTool) {
                tools.gameObject.SetActive(true);
            }
            else {
                tools.gameObject.SetActive(false);
            }

            i++;
        }
    }

    // Update is called once per frame
    void Update() {
        int prevSelected = selectedTool;
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelInput > 0f) {
            if (selectedTool >= transform.childCount - 1) {
                selectedTool = 0;
            }
            else
                selectedTool++;
            
        }else if (scrollWheelInput < 0f) {
            if (selectedTool <= 0) {
                selectedTool = transform.childCount -1;
            }
            else
                selectedTool--;

        }

        if (prevSelected != selectedTool) {
            SelectedTool();
        }
        
        
    }
}