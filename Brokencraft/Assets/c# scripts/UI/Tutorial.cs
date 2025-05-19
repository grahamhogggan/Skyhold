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
        if (Overlord.paused) return;
        if (writer.TimeSinceFinished() > waitToHideTime)
        {
            hide.SetActive(false);
        }
        else
        {
            hide.SetActive(true);
        }
        if (nextGoalText.text == "")
        {
            nextGoalPanel.SetActive(false);
        }
        else
        {
            nextGoalPanel.SetActive(true);
        }
        if (PersistentData.tutorialStep == 1)
        {
            writer.SetTarget("Have a moment to take in your surroundings");
            PersistentData.tutorialStep = 2;
        }
        else if (PersistentData.tutorialStep == 2)
        {
            if (writer.TimeSinceFinished() >= 7)
            {
                PersistentData.tutorialStep = 3;
            }
        }
        else if (PersistentData.tutorialStep == 3)
        {
            writer.SetTarget("The first thing you'll need to do down here is gather resources. Go collect 5 pieces of wood");
            PersistentData.tutorialStep = 4;
        }
        else if (PersistentData.tutorialStep == 4)
        {
            nextGoalText.text = "Gather 5 wood";
            int numCollected = PlayerInventory.Quantity("wood") + PlayerInventory.Quantity("palm wood");
            if (numCollected >= 5)
            {
                nextGoalText.text = "";
                PersistentData.tutorialStep = 5;
            }
        }
        else if (PersistentData.tutorialStep == 5)
        {
            writer.SetTarget("Press TAB to enter your inventory. Click the '+' next to the planks recipe to craft wood planks");
            nextGoalText.text = "Craft 5 planks";
            PersistentData.tutorialStep = 6;
        }
        else if (PersistentData.tutorialStep == 6)
        {
            int numCrafted = PlayerInventory.Quantity("planks");
            if (numCrafted >= 5)
            {
                nextGoalText.text = "";
                PersistentData.tutorialStep = 7;
            }
        }
        else if (PersistentData.tutorialStep == 7)
        {
            writer.SetTarget("You'll also want to be able to mine more effectively. Craft sticks, and then craft a pickaxe");
            nextGoalText.text = "Craft a pickaxe";
            PersistentData.tutorialStep = 8;
        }
        else if (PersistentData.tutorialStep == 8)
        {
            int numCrafted = PlayerInventory.Quantity("pickaxe");
            if (numCrafted >= 1)
            {
                nextGoalText.text = "";
                PersistentData.tutorialStep = 9;
            }
        }
        else if (PersistentData.tutorialStep == 9)
        {
            writer.SetTarget("Using this pickaxe, you can collect more valuable materials found only underground. Collect 5 metal");
            nextGoalText.text = "Collect 5 metal";
            PersistentData.tutorialStep = 10;
        }
        else if (PersistentData.tutorialStep == 10)
        {
            int numCrafted = PlayerInventory.Quantity("metal");
            if (numCrafted >= 5)
            {
                nextGoalText.text = "";
                PersistentData.tutorialStep = 11;
            }
        }
        else if (PersistentData.tutorialStep == 11)
        {
            writer.SetTarget("Use the metal to craft a beam, and then a sword. You'll need this to effectively fight off enemies");
            PersistentData.tutorialStep = 12;
        }
        else if (PersistentData.tutorialStep == 12)
        {
            int numCrafted = PlayerInventory.Quantity("sword");
            if (numCrafted >= 1)
            {
                nextGoalText.text = "";
                PersistentData.tutorialStep = 13;
            }
        }
        else if (PersistentData.tutorialStep == 14)
        {
            writer.SetTarget("The next important material you'll want is wires. These can usually only be collected from defeated enemies.");
            nextGoalText.text = "Collect 3 wires";
            PersistentData.tutorialStep = 15;
        }
        else if (PersistentData.tutorialStep == 15)
        {
            int numCrafted = PlayerInventory.Quantity("wires");
            if (numCrafted >= 3)
            {
                nextGoalText.text = "";
                PersistentData.tutorialStep = 16;
            }
        }
        else if (PersistentData.tutorialStep == 16)
        {
            writer.SetTarget("To heal form your battles, you need light and warmth. Craft a torch to heal you");
            nextGoalText.text = "Craft torches";
            PersistentData.tutorialStep = 17;
        }
        else if (PersistentData.tutorialStep == 17)
        {
            int numCrafted = PlayerInventory.Quantity("torch");
            if (numCrafted >= 1)
            {
                nextGoalText.text = "";
                PersistentData.tutorialStep = 18;
            }
        }
        else if (PersistentData.tutorialStep == 18)
        {
            writer.SetTarget("Place the torch to heal you.");
            PersistentData.tutorialStep = 19;
        }
        else if (PersistentData.tutorialStep == 19)
        {
            if (writer.TimeSinceFinished() >= 4)
            {
                PersistentData.tutorialStep = 20;
            }
        }
        else if (PersistentData.tutorialStep == 20)
        {
            writer.SetTarget("You can create machines to do work for you. These all require processors");
            nextGoalText.text = "Craft a processor";
            PersistentData.tutorialStep = 21;
        }
        else if (PersistentData.tutorialStep == 21)
        {
            int numCrafted = PlayerInventory.Quantity("processor");
            if (numCrafted >= 1)
            {
                nextGoalText.text = "";
                PersistentData.tutorialStep = 22;
            }
        }
        else if (PersistentData.tutorialStep == 22)
        {
            writer.SetTarget("Craft an extractor. These will automatically gather resources for you.");
            nextGoalText.text = "Craft an extractor";
            PersistentData.tutorialStep = 23;
        }
        else if (PersistentData.tutorialStep == 23)
        {
            int numCrafted = PlayerInventory.Quantity("extracter");
            if (numCrafted >= 1)
            {
                nextGoalText.text = "";
                PersistentData.tutorialStep = 24;
            }
        }
        else if (PersistentData.tutorialStep == 24)
        {
            writer.SetTarget("Good luck on your journey. If you make it past night five, we'll give you an even bigger challenge");
            PersistentData.tutorialStep = 25;
        }
    }
}
