using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandMenuHandler : MonoBehaviour {
    public GameObject build;
    public GameObject attack;

    public GameObject bombChecked;
    public GameObject gunChecked;
    public GameObject gravityChecked;
    
    // Start is called before the first frame update
    void Start() {
        attack.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
    }

    public void SetIsBuilding(bool isBuilding) {
        build.SetActive(isBuilding);
        attack.SetActive(!isBuilding);
    }
    
    /**
     * Items:
     * 0 - Nothing
     * 1 - gravity
     * 2 - gun
     * 3 - Bomb
     */
    public void SetCurrentSelectetItem(int item) {
        
        switch (item) {
            case -1:
                bombChecked.SetActive(false);
                gunChecked.SetActive(false);
                gravityChecked.SetActive(false);
                break;
            case 0:
                bombChecked.SetActive(false);
                gunChecked.SetActive(false);
                gravityChecked.SetActive(true);
                break;
            case 1:
                bombChecked.SetActive(false);
                gunChecked.SetActive(true);
                gravityChecked.SetActive(false);
                break;
            case 2: 
                bombChecked.SetActive(true);
                gunChecked.SetActive(false);
                gravityChecked.SetActive(false);
                break;
        }
    }
}