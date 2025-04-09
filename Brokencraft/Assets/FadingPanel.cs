using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FadingPanel : MonoBehaviour
{
    public TMP_Text textMesh;
    public Image panel;
    public float rate;
    // Start is called before the first frame update
    public void Show(string msg)
    {
        textMesh.text = msg;
        if (rate > 0)
        {
            rate *= -2;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (panel.color.a > 0.95f && rate < 0)
        {
            rate *= -0.5f;
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0.95f);
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b,0.95f);
        }
        if (panel.color.a < 0)
        {
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0);
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b,0);

        }
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, panel.color.a - Time.deltaTime * rate);
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, textMesh.color.a - Time.deltaTime * rate);
    }
}
