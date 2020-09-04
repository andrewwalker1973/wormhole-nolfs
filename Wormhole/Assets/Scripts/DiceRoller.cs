using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class DiceRoller : MonoBehaviour
{
    StateManger theStateManager;
   





    // Start is called before the first frame update
    void Start()
    {
        DiceValues = new int[1];        // only using one dice
        DiceFaceImage = GetComponent<Image>();
        
        theStateManager = GameObject.FindObjectOfType<StateManger>();
     

       
   




    }

    public int[] DiceValues;        // int of all dice values


    //set images for the dice image
    public Sprite[] DiceImageOne;
    public Sprite[] DiceImageTwo;
    public Sprite[] DiceImageThree;
    public Sprite[] DiceImageFour;
    public Sprite[] DiceImageFive;
    public Sprite[] DiceImageSix;
    public Sprite[] DiceBlank;

    


    // Set Dice Image
    Image DiceFaceImage;






   

 
 

    public void RollTheDice()
    {
       

        if (theStateManager.IsDoneRolling == true)
        {
            // we ahve already rolled this turn
            return;
        }

        theStateManager.DiceTotal = 0;
     
        DiceFaceImage = GetComponent<Image>();

       


        

        // anim.SetTrigger("DiceRollAnim");
        for (int i = 0; i < DiceValues.Length; i++)
        {
            
                DiceValues[i] = Random.Range(1, 7);
            theStateManager.DiceTotal += DiceValues[i];

           


            switch (DiceValues[i])
            {
                case 1:
                    
                    this.transform.GetChild(i).GetComponent<Image>().sprite =
                    DiceImageOne[Random.Range(0, DiceImageOne.Length)];
                    break;
                case 2:
                    
                    this.transform.GetChild(i).GetComponent<Image>().sprite =
                   DiceImageTwo[Random.Range(0, DiceImageTwo.Length)];
                    break;
                case 3:
                    
                    this.transform.GetChild(i).GetComponent<Image>().sprite =
                   DiceImageThree[Random.Range(0, DiceImageThree.Length)];
                    break;
                case 4:
                    
                    this.transform.GetChild(i).GetComponent<Image>().sprite =
                   DiceImageFour[Random.Range(0, DiceImageFour.Length)];
                    break;
                case 5:
                 
                    this.transform.GetChild(i).GetComponent<Image>().sprite =
                   DiceImageFive[Random.Range(0, DiceImageFive.Length)];
                    break;
                case 6:
                   
                    this.transform.GetChild(i).GetComponent<Image>().sprite =
                   DiceImageSix[Random.Range(0, DiceImageSix.Length)];
                    break;
            }
        }

        ///  alter this to set dice value for testing   
        ///  
    theStateManager.DiceTotal = 100;
        theStateManager.IsDoneRolling = true;
        theStateManager.CheckLegalMoves();
        theStateManager.UIRollAgainPopup.SetActive(false);
    }
    
}

