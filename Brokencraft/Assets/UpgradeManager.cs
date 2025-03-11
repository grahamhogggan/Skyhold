using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UpgradeManager : MonoBehaviour
{
   private static List<string> UpgradesBoughtNames;
    private static List<int> UpgradesBoughtCount;
   private string savepath;
   void Start()
   {
    savepath = Overlord.saveDirectory+"/upgrades.save";
    if(Overlord.OverwriteMode||!File.Exists(savepath))
    {
        UpgradesBoughtNames = new List<string>();
        UpgradesBoughtCount = new List<int>();
    }
    else
    {
        UpgradeSave save =JsonUtility.FromJson<UpgradeSave>(File.ReadAllText(savepath));
        UpgradesBoughtCount =save.countSaveData;
        UpgradesBoughtNames = save.nameSaveData;
    }
    SaveManager.Register(Save);
   }
   public void Save()
   {
    string data = JsonUtility.ToJson(new UpgradeSave(UpgradesBoughtNames,UpgradesBoughtCount));
    File.WriteAllText(savepath,data);
   }
    // Update is called once per frame
    void Update()
    {
        
    }
    public static void Purchase(string name)
    {
        if(UpgradesBoughtNames.IndexOf(name)!=-1)
        {
            UpgradesBoughtCount[UpgradesBoughtNames.IndexOf(name)]++;
        }
        else
        {
            UpgradesBoughtNames.Add(name);
            UpgradesBoughtCount.Add(1);
        }
    }
    public static int GetCost(string name)
    {
        if(UpgradesBoughtNames.IndexOf(name)!=-1) return (int)Mathf.Pow(2,UpgradesBoughtCount[UpgradesBoughtNames.IndexOf(name)]);
        return 1;
    }
    public static int GetUpgradeAmount(string name)
    {
        int index = UpgradesBoughtNames.IndexOf(name);
        if(index == -1) return 0;
        else
        {
            return UpgradesBoughtCount[index];
        }
    }
}
class UpgradeSave
{
    public List<string> nameSaveData;
    public List<int> countSaveData;
    public UpgradeSave(List<string> n,List<int> c)
    {
        nameSaveData = n;
        countSaveData = c;
    }
}
