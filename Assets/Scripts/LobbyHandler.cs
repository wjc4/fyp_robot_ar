using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class JoinPayload
{
    public string id;
    public string name;
}

[System.Serializable]
public class UpdateLobbyPayload
{
    public string id;
    public bool ready;
}

[System.Serializable]
public class UpdateLobbyResponsePlayer
{
    public string name;
    public bool ready;
}

[System.Serializable]
public class UpdateLobbyResponse
{
    public UpdateLobbyResponsePlayer[] players;
    public bool start;
}

public class LobbyHandler : MonoBehaviour
{
    public string lobbyUrl;
    public string playerID;
    public string playerName;
    public GameObject LobbyPanel;
    public GameObject GamePanel;
    public GameObject WheelMaster;

    private string joinLobbyAPI;
    private string updateLobbyAPI;
    private JoinPayload data;

    // Start is called before the first frame update
    void Start()
    {
        joinLobbyAPI = lobbyUrl + "join";
        updateLobbyAPI = lobbyUrl + "update_lobby";
        data = new JoinPayload();
        data.id = playerID;
        data.name = playerName;
        StartCoroutine(JoinLobby());
    }

    IEnumerator JoinLobby()
    {
        var jsonString = JsonUtility.ToJson(data);
        UnityWebRequest request = UnityWebRequest.Put(joinLobbyAPI, jsonString);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            // Show results as text
            Debug.Log(request.downloadHandler.text);
            JoinPayload response = JsonUtility.FromJson<JoinPayload>(request.downloadHandler.text);
            Debug.Log(response.id);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLobbyPayload updateData = new UpdateLobbyPayload();
        updateData.id = playerID;
        updateData.ready = true;
        StartCoroutine(UpdateLobby(updateData));
    }

    IEnumerator UpdateLobby(UpdateLobbyPayload updateData)
    {
        var jsonString = JsonUtility.ToJson(updateData);
        UnityWebRequest request = UnityWebRequest.Put(updateLobbyAPI, jsonString);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            // Show results as text
            Debug.Log(request.downloadHandler.text);
            UpdateLobbyResponse response = JsonUtility.FromJson<UpdateLobbyResponse>(request.downloadHandler.text);
            if (response.start)
            {
                StartGame();
            }
        }
    }

    void StartGame()
    {
        LobbyPanel.SetActive(false);
        GamePanel.SetActive(true);
        WheelMaster.GetComponent<WheelHandler>().enabled = true;
        WheelMaster.GetComponent<CollisionHandler>().enabled = true;
    }
}
