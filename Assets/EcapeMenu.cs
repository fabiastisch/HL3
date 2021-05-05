using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EcapeMenu : MonoBehaviour {
    public GameObject canvas;

    public Toggle fullscreen;
    private bool inEscapeMenu = false;
    

    // Start is called before the first frame update
    void Start() {
        canvas.SetActive(false);
        // fullscreen.isOn = !Screen.fullScreen;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            inEscapeMenu = !inEscapeMenu;
            if (inEscapeMenu) {
                canvas.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else {
                canvas.SetActive(false);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void FullScreenToggle(bool value) {
        Screen.fullScreen = !Screen.fullScreen;
    }
}