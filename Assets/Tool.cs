using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour {
    public bool isActive = true;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (isActive) {
            this.OnUpdate();
        }
    }

    /**
     * OnUpdate Is called once per frame, if isActive.
     */
    protected abstract void OnUpdate();
}