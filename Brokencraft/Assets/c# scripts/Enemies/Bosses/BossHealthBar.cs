using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    public static float maxHealth;
    public static float currentHealth;
    public static GameObject instance;
    [SerializeField] private GameObject linked;

    private RectTransform T;
    void Awake()
    {
        instance = gameObject;
        gameObject.SetActive(false);
        T = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (maxHealth == 0) return;
        T.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,1000*currentHealth/maxHealth);
    }
    void OnEnable()
    {
        linked.SetActive(true);
    }
    void OnDisable()
    {
        linked.SetActive(false);
    }
}
