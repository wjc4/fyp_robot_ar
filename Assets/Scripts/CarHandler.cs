using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class payload
{
    public string action;
    public float multiplier;
    public float degree;
}

public class CarHandler : MonoBehaviour
{
    public string url;
    public int testvar;
    public float multiplier = 1f;
    public bool stun = false;

    private string api;
    private payload data;
    private string prevAction;
    // Start is called before the first frame update
    void Start()
    {
        api = url + "command";
        data = new payload();
        data.action = "stop";
        data.multiplier = multiplier;
        data.degree = 0;
        var jsonString = JsonUtility.ToJson(data);
        UnityWebRequest request = UnityWebRequest.Put(api, jsonString);
        request.SetRequestHeader("Content-Type", "application/json");
        request.SendWebRequest();
        prevAction = data.action;
    }

    // Update is called once per frame
    void Update()
    {
        data.multiplier = multiplier;

        WheelHandler wheel = GetComponent<WheelHandler>();
        if (stun)
        {
            data.action = "right";
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
        else if (wheel.simple)
        {
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
        } else
        {
            if (wheel.brakeStatus == 1 && wheel.gasStatus == 1)
            {
                data.action = "stop";
            }
            else if (wheel.brakeStatus == 1)
            {
                //data.action = "stop";
                data.action = "forward_steer";
                data.degree = wheel.steeringRatio;

            }
            else if (wheel.gasStatus == 1)
            {
                //data.action = "forward";
                data.action = "reverse_steer";
                data.degree = wheel.steeringRatio;

            }
            else if (wheel.steeringStatus == -1)
            {
                data.action = "left";
            }
            else if (wheel.steeringStatus == 1)
            {
                data.action = "right";
            }
            else
            {
                data.action = "stop";
            }


            var jsonString = JsonUtility.ToJson(data);
            //print(jsonString);
            UnityWebRequest request = UnityWebRequest.Put(api, jsonString);
            request.SetRequestHeader("Content-Type", "application/json");
            request.SendWebRequest();

        }
        
        

    }
}
