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
    private Dictionary<string, CheckpointInfo> checkpoints = new Dictionary<string, CheckpointInfo>();
    [System.Serializable]
    public class CheckpointInfo
    {
        public string ObjectTarget;
        public int Section;
        public string NextObjectTarget;
        public bool First;
        public bool Last;

        public CheckpointInfo(string newObjectTarget, int newSection, string newNextObjectTarget, bool newFirst, bool newLast)
        {
            ObjectTarget = newObjectTarget;
            Section = newSection;
            NextObjectTarget = newNextObjectTarget;
            First = newFirst;
            Last = newLast;
            
        }
    }
    private readonly CheckpointInfo initCheckpoint = new CheckpointInfo("Target1", 1, "Target2", true, false);

    public AudioSource teleportSound;



// Start is called before the first frame update
void Start()
    {
        FlagText.text = "Section: " + CurrSection + "/" + TotalSection;
        LapText.text = "Lap: " + CurrLap + "/" + TotalLap;
        NotificationText.text = "debug";
        Disappear(NotificationText);
        Disappear(NotificationBG);

        //Set up dictionary mapping collision object to object target
        //Dictionary<string, string> checkpoints = new Dictionary<string, string>(); // commmented out because alr declared as private var

        CheckpointInfo CheckPointB = new CheckpointInfo("Target2", 2, "Target3", false, true);
        CheckpointInfo CheckPointC = new CheckpointInfo("Target3", 3, "Target4", false, false);
        CheckpointInfo CheckPointD = new CheckpointInfo("Target4", 4, "Target1", false, false);
        //You can place variables into the Dictionary with the
        //Add() method.
        checkpoints.Add("Teleporter Pad A", initCheckpoint);
        checkpoints.Add("Teleporter Pad B", CheckPointB);
        checkpoints.Add("Teleporter Pad C", CheckPointC);
        checkpoints.Add("Teleporter Pad D", CheckPointD);

        // TODO bad hardcoded stuff pls fix
        UpdateGreen(initCheckpoint.ObjectTarget);
        UpdateRed(CheckPointB.ObjectTarget);
        //UpdateRed(CheckPointC.ObjectTarget);
        //UpdateRed(CheckPointD.ObjectTarget);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        //old
        /*

        if (col.gameObject.name == "Flag1" || col.gameObject.name == "Flag2")
        {
            Disappear(col.gameObject);
            UpdateText(col.gameObject);
            StartCoroutine(Respawn(respawntime, col.gameObject));
        }
        else if (col.gameObject.name == "Teleporter Pad A")
        {
            teleportSound.Play();
        }

         */

        //new
        string tempname = col.gameObject.name;
        //This is a safer, but slow, method of accessing
        //values in a dictionary.
        if (checkpoints.TryGetValue(tempname, out CheckpointInfo temp))
        {
            //success!
            UpdateTextNew(temp);
        }
        else
        {
            //failure!
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

    protected virtual void UpdateTextNew(CheckpointInfo obj)
    {
        if (obj.Section == CurrSection)
        {
            UpdateStats();
            // TODO set self to red
            UpdateRed(obj.ObjectTarget);
            if (obj.Last)
            {
                // TODO set init to green
                UpdateGreen(initCheckpoint.ObjectTarget);
            }
            else
            {
                // TODO set next to green
                UpdateGreen(obj.NextObjectTarget);
            }
            // TODO play sound
        }
        else
        {
            // TODO play sound
            WrongFlagAlert();
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

    protected virtual void UpdateColor()
    {
        var podAColor = GameObject.Find("Teleporter Pad A").GetComponent<ParticleColorHandler>();
        podAColor.ToGreen();
    }

    protected virtual void UpdateGreen(string ObjectTarget)
    {
        var ColorHandler = GameObject.Find(ObjectTarget).GetComponent<ObjectTargetColorHandler>();
        ColorHandler.ToGreen();
    }

    protected virtual void UpdateRed(string ObjectTarget)
    {
        var ColorHandler = GameObject.Find(ObjectTarget).GetComponent<ObjectTargetColorHandler>();
        ColorHandler.ToRed();
    }

}