using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    TextWriter writer;
    [SerializeField] private float waitToHideTime;
    [SerializeField] private TMP_Text nextGoalText;
    [SerializeField] private GameObject nextGoalPanel;
    [SerializeField] private GameObject hide;
    void Start()
    {
        writer = GetComponent<TextWriter>();
    }
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
            writer.SetTarget("You've probably noticed how strange it all looks. Take a moment to take in your surroundings");
            PersistentData.tutorialStep = 2;
        }
        else if(PersistentData.tutorialStep == 2)
        {
            if(writer.TimeSinceFinished()>=15)
            {
                PersistentData.tutorialStep = 3;
            }
        }
        else if(PersistentData.tutorialStep == 3)
        {
            writer.SetTarget("The first thing we'll need to do down here is gather resources. Go collect 5 pieces of wood");
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
            writer.SetTarget("One last thing, After you've progressed enough, you can start building machines to extract score down here. You can use that score to upgrade yourself. Here's some score for now.");
            ScoreManager.AddScore(30);
            PersistentData.tutorialStep = 10;
        }
        else if(PersistentData.tutorialStep == 10)
        {
            if(writer.TimeSinceFinished()>=15)
            {
                PersistentData.tutorialStep = 11;
            }
        }
        else if(PersistentData.tutorialStep == 11)
        {
            writer.SetTarget("Head to the upgrades tab in your inventory to spend it");
            nextGoalText.text = "Get an upgrade";
            PersistentData.tutorialStep = 12;
        }
        else if(PersistentData.tutorialStep == 12)
        {
            if(ScoreManager.GetActiveScore()<30)
            {
                nextGoalText.text = "";
            }
        }
    }
}
