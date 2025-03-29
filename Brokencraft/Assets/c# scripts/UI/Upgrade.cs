using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrade : MonoBehaviour
{
   [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text purchasedText;
   private int cost;
   public string name;
   public int initialCost;
   public AudioSource soundEffect;
void Start()
{
     cost = UpgradeManager.GetCost(name)*initialCost;
}
   public void Buy()
   {
    if(ScoreManager.GetActiveScore()>=cost)
    {
        ScoreManager.SpendScore(cost);
        UpgradeManager.Purchase(name);
        soundEffect.Play();
    }
   }
public void Update()
{
    costText.text = cost.ToString();
    cost = UpgradeManager.GetCost(name)*initialCost;
    purchasedText.text = UpgradeManager.GetUpgradeAmount(name).ToString();

}

}
