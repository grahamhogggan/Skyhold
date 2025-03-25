using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Enemy enemy;
    [SerializeField] private int maxHealth;
    [SerializeField] public int bossNumber;
    void Start()
    {
        BossHealthBar.maxHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        BossHealthBar.currentHealth = enemy.getHealth();
        BossHealthBar.instance.SetActive(true);
    }
    void OnDestroy()
    {
        if(BossHealthBar.instance!=null) BossHealthBar.instance.SetActive(false);
        BossBeater.BeatBoss(bossNumber);
        
    }
}
