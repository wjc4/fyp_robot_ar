using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class payload
{
    public string action;
}

public class CarHandler : MonoBehaviour
{
    public string url;
    public int testvar;

    private string api;
    private payload data;
    private string prevAction;
    // Start is called before the first frame update
    void Start()
    {
        api = url + "command";
        data = new payload();
        data.action = "stop";
        var jsonString = JsonUtility.ToJson(data);
        UnityWebRequest request = UnityWebRequest.Put(api, jsonString);
        request.SetRequestHeader("Content-Type", "application/json");
        request.SendWebRequest();
        prevAction = data.action;
    }

    // Update is called once per frame
    void Update()
    {

        WheelHandler wheel = GetComponent<WheelHandler>();
        if (wheel.brakeStatus == 1)
        {
            //data.action = "stop";
            data.action = "forward";
        }
        else if (wheel.steeringStatus == -1)
        {
            data.action = "left";
        }
        else if (wheel.steeringStatus == 1)
        {
            data.action = "right";
        }
        else if (wheel.gasStatus == 1)
        {
            //data.action = "forward";
            data.action = "reverse";
        }
        else
        {
            data.action = "stop";
        }

        if (data.action != prevAction)
        {
            var jsonString = JsonUtility.ToJson(data);
            //print(jsonString);
            UnityWebRequest request = UnityWebRequest.Put(api, jsonString);
            request.SetRequestHeader("Content-Type", "application/json");
            request.SendWebRequest();
            prevAction = data.action;
        }
        

    }
}
