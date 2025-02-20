using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Inventory
{
    public List<string> items;

    public Inventory()
    {
        items = new List<string>();
    }


    public void AddItem(string item)
    {
        for(int i = 0; i<items.Count; i++)
        {
            if (items[i] == item)
            {
                items.Insert(i + 1,item);
                return;
            }
        }
        items.Add(item);
    }
    public void RemoveItem(string item)
    {
        items.Remove(item);
    }
    public void UseItems(string item, int quantity)
    {
        for(int i  = 0; i<quantity; i++)
        {
            items.Remove(item);
        }    
    }
    public int Quantity(string item)
    {
        int cnt = 0;
        foreach(string item2 in items)
        {
            if(item2.Equals(item)) cnt++;
        }
        return cnt;
    }
    public string GetItem(int index)
    {
        int resultIndex = GetRawIndex(index);
        if(resultIndex == -1)
        {
            return null;
        }
        else
        {
            return items[resultIndex];
        }
        
    }
    public int GetRawIndex(int index)
    {
        if(items.Count<=0)
        {
            return -1;
        }
        int changes = 1;
        int i = 0;
        if(items.Count == 1)
        {
            if(index == 0)
            {
            return 0;
            }
            else
            return -1;
        }
        while(changes<=index)
        {
            i++;
            if(i>=items.Count)
            {
                return -1;
            }
            if (items[i] != items[i-1])
            {
                changes++;
            }
        }
        return i;
    }
    public int ItemCount()
    {
        int cnt = 0;
        string curr = "bloonarius mcBloon";
        for(int i = 0; i<items.Count;i++)
        {
            if (items[i] != curr)
            {
                cnt++;
                curr = items[i];
            }
        }
        return cnt;
    }
    public void SwitchItems(int index1, int index2)
    {
        if(index2<index1)
        {
            int temp = index1;
            index1 = index2;
            index2 = temp;
        }
        if(index1 == index2)
        {
            return;
        }
        List<string> newItems = new List<string>();
        int index = 0;
        int ti1 = GetRawIndex(index1);
        int ti2 = GetRawIndex(index2);

        while(index<ti1)
        {
            newItems.Add(items[index]);
            index++;
        }
        index = ti2;

        string str = GetItem(index2);
        while(index<items.Count&&items[index]==str)
        {
            newItems.Add(items[index]);
            index++;
        }
        index = GetRawIndex(index1+1);
        if(index==-1)
        {
            return;
        }
        if(index != ti2)
        {
        while(index<items.Count&&index<ti2)
        {
            newItems.Add(items[index]);
            index++;
        }
        }

        index = ti1;
        str  = GetItem(index1);
        while(items[index]==str)
        {
            newItems.Add(items[index]);
            index++;
        }
        index = GetRawIndex(index2+1);
        if(index!=-1)
        {
        while(index<items.Count)
        {
            newItems.Add(items[index]);
            index++;
        }
        }

        items = newItems;
    }
}
