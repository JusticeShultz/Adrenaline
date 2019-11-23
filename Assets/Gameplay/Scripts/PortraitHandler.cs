using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitHandler : MonoBehaviour
{
    public Image healthBar;
    public GameObject ok;
    public GameObject mad;
    public GameObject REEE;
    public GameObject dying;

    void Update()
    {
        if(healthBar.fillAmount >= 0.765f)
        {
            ok.SetActive(true);
            mad.SetActive(false);
            REEE.SetActive(false);
            dying.SetActive(false);
        }
        else if (healthBar.fillAmount >= 0.5f)
        {
            ok.SetActive(false);
            mad.SetActive(true);
            REEE.SetActive(false);
            dying.SetActive(false);
        }
        else if (healthBar.fillAmount >= 0.17f)
        {
            ok.SetActive(false);
            mad.SetActive(false);
            REEE.SetActive(true);
            dying.SetActive(false);
        }
        else
        {
            ok.SetActive(false);
            mad.SetActive(false);
            REEE.SetActive(false);
            dying.SetActive(true);
        }
    }
}