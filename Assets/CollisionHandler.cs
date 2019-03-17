using System.Collections;
using System.Collections.Generic;
//using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollisionHandler : MonoBehaviour
{

    //public TextMeshProUGUI flagText;
    public Text FlagText;
    public Text LapText;
    public Text NotificationText;
    public Image NotificationBG;
    public float respawntime = 2f;

    private int CurrSection = 1;
    private int CurrLap = 1;
    public int TotalSection = 2;
    public int TotalLap = 3;

    // Start is called before the first frame update
    void Start()
    {
        FlagText.text = "Section: " + CurrSection + "/" + TotalSection;
        LapText.text = "Lap: " + CurrLap + "/" + TotalLap;
        NotificationText.text = "debbug";
        Disappear(NotificationText);
        Disappear(NotificationBG);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Flag1" || col.gameObject.name == "Flag2")
        {
            Disappear(col.gameObject);
            UpdateText(col.gameObject);
            StartCoroutine(Respawn(respawntime, col.gameObject));
        }

    }

    protected virtual void Disappear(GameObject obj)
    {
        obj.SetActive(false);
        //obj.GetComponent<Renderer>().enabled = false;
        //obj.GetComponent<Collider>().enabled = false;
    }

    protected virtual void Appear(GameObject obj)
    {
        obj.SetActive(true);
        //obj.GetComponent<Renderer>().enabled = true;
        //obj.GetComponent<Collider>().enabled = true;
    }

    protected virtual void Disappear(Text obj)
    {
        obj.enabled = false;
    }

    protected virtual void Appear(Text obj)
    {
        obj.enabled = true;
    }

    protected virtual void Disappear(Image obj)
    {
        obj.enabled = false;
    }

    protected virtual void Appear(Image obj)
    {
        obj.enabled = true;
    }


    IEnumerator Respawn(float respawntime, GameObject obj)
    {
        yield return new WaitForSeconds(respawntime);
        //RespawnHandler.Respawn();
        //yield return null;
        Appear(obj);
    }

    protected virtual void UpdateText(GameObject obj)
    {
        if (obj.name == "Flag1")
        {
            if (CurrSection == 1)
            {
                UpdateStats();

            } else
            {
                WrongFlagAlert();
            }
   
        }
        else if (obj.name == "Flag2")
        {
            if (CurrSection == 2)
            {
                UpdateStats();
            }
            else
            {
                WrongFlagAlert();
            }
        } 
    }

    protected virtual void UpdateStats()
    {
        if (CurrSection == TotalSection)
        {
            CurrLap = (CurrLap) % TotalLap + 1;

        }
        CurrSection = (CurrSection)%TotalSection+1;
        FlagText.text = "Section: " + CurrSection + "/" + TotalSection;
        LapText.text = "Lap: " + CurrLap + "/" + TotalLap;

    }

    protected virtual void WrongFlagAlert()
    {
        StartCoroutine(ShowNotification("Wrong Flag! Proceed to the correct flag.", 2f));
    }

    IEnumerator ShowNotification(string Message, float duration)
    {
        Appear(NotificationBG);
        Appear(NotificationText);
        NotificationText.text = Message;
        yield return new WaitForSeconds(duration);
        Disappear(NotificationText);
        Disappear(NotificationBG);
    }

}