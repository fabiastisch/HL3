using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownManager : MonoBehaviour {
    public Text cooldownText;

    public String dash;
    public String jumpsAvaiable;
    private float dashEndTime = 0; 
    
    // Start is called before the first frame update
    void Start() {
        cooldownText.text = dash + " Ready";
        cooldownText.text += " \n" + jumpsAvaiable + GameSettings.Instance.playerMovement.jumps;
    }

    // Update is called once per frame
    void Update() {

        if (Time.time < dashEndTime) {
            float time = dashEndTime - Time.time;
            cooldownText.text = dash + " " + time.ToString("0.00") + " s";
        }
        else {
            cooldownText.text = dash + " Ready";
        }

        cooldownText.text += " \n" + jumpsAvaiable + GameSettings.Instance.playerMovement.GetCurrentJumps();

    }

    public void StartDashing(float time) {
        float startTime = Time.time;
        dashEndTime = startTime + time;

        /*Debug.Log("startTime"+startTime);
        Debug.Log("endTime"+endTime);
        */
        
       
        
    }
    
}