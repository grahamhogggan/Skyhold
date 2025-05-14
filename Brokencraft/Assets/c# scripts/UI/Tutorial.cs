using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TextWriter writer;
    [SerializeField] private float waitToHideTime;
    [SerializeField] private TMP_Text nextGoalText;
    [SerializeField] private GameObject nextGoalPanel;
    [SerializeField] private GameObject hide;
    
    void Update()
    {
        if(Overlord.paused) return;
        if(writer.TimeSinceFinished()>waitToHideTime)
        {
            hide.SetActive(false);
        }
        else
        {
            hide.SetActive(true);
        }
        if(nextGoalText.text =="")
        {
            nextGoalPanel.SetActive(false);
        }
        else
        {
            nextGoalPanel.SetActive(true);
        }
        if(PersistentData.tutorialStep == 1)
        {
            writer.SetTarget("Have a moment to take in your surroundings");
            PersistentData.tutorialStep = 2;
        }
        else if(PersistentData.tutorialStep == 2)
        {
            if(writer.TimeSinceFinished()>=7)
            {
                PersistentData.tutorialStep = 3;
            }
        }
        else if(PersistentData.tutorialStep == 3)
        {
            writer.SetTarget("The first thing you'll need to do down here is gather resources. Go collect 5 pieces of wood");
            PersistentData.tutorialStep = 4;
        }
        else if(PersistentData.tutorialStep == 4)
        {
            nextGoalText.text = "Gather 5 wood";
            int numCollected = PlayerInventory.Quantity("wood")+PlayerInventory.Quantity("palm wood");
            if(numCollected>=5)
            {
                nextGoalText.text = "";
                PersistentData.tutorialStep = 5;
            }
        }
        else if(PersistentData.tutorialStep == 5)
        {
            writer.SetTarget("Press TAB to enter your inventory. Click the '+' next to the planks recipe to craft wood planks");
            nextGoalText.text = "Craft 5 planks";
            PersistentData.tutorialStep = 6;
        }
        else if(PersistentData.tutorialStep == 6)
        {
            int numCrafted = PlayerInventory.Quantity("planks");
            if(numCrafted>=5)
            {
                nextGoalText.text = "";
                PersistentData.tutorialStep = 7;
            }
        }
        else if(PersistentData.tutorialStep == 7)
        {
            writer.SetTarget("You'll also want to be able to mine more effectively. Craft sticks, and then craft a pickaxe");
            nextGoalText.text = "Craft a pickaxe";
            PersistentData.tutorialStep = 8;
        }
        else if(PersistentData.tutorialStep == 8)
        {
            int numCrafted = PlayerInventory.Quantity("pickaxe");
            if(numCrafted>=1)
            {
                nextGoalText.text = "";
                PersistentData.tutorialStep = 9;
            }
        }
        else if(PersistentData.tutorialStep == 9)
        {
            writer.SetTarget("One last thing, As you continue, you'll gather energy. You can unlock upgrades with this");
            PersistentData.tutorialStep = 10;
        }
        else if(PersistentData.tutorialStep == 10)
        {
            if(writer.TimeSinceFinished()>=7)
            {
                PersistentData.tutorialStep = 11;
            }
        }
        else if(PersistentData.tutorialStep == 11)
        {
            writer.SetTarget("Head to the upgrades tab in your inventory to unlock upgrades with it");
            PersistentData.tutorialStep = 12;
        }
        else if(PersistentData.tutorialStep == 12)
        {
        }
    }
}
