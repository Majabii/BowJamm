using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TriggerDescription : MonoBehaviour
{

    public TMP_Text leftTriggerText;
    public TMP_Text rightTriggerText;

    private bool lefthanded = false;

    // Start is called before the first frame update
    void Start()
    {
        if (lefthanded) {
            leftTriggerText.text = "Arrow";
            rightTriggerText.text = "Draw bow";
        } else {
            leftTriggerText.text = "Draw bow";
            rightTriggerText.text = "Arrow";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchHand(bool right_handed) {
        lefthanded = !right_handed;
        if (lefthanded) {
            leftTriggerText.text = "Arrow";
            rightTriggerText.text = "Draw bow";
        } else {
            leftTriggerText.text = "Draw bow";
            rightTriggerText.text = "Arrow";
        }
    }
}
