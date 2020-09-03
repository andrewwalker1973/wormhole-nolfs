using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceTotalDisplay : MonoBehaviour
{

  StateManger theStateManager;
    DiceRoller theDiceRoller;

    Image DiceFaceImage;

    // Start is called before the first frame update
    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManger>();
      //  theDiceRoller = GameObject.FindObjectOfType<DiceRoller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (theStateManager.IsDoneRolling == false)
        {
           // Debug.Log("in dice total");
            GetComponent<TMP_Text>().text = "Dice Total =  ?";
            //TRY blankj dice face image
          //  theDiceRoller.ResetDiceImage();
        }
        else
        {
            GetComponent<TMP_Text>().text = "Dice Total =  " + theStateManager.DiceTotal;
        }
    }
}
