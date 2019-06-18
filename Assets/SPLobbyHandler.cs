using System.Collections;
using UnityEngine;


public class SPLobbyHandler : MonoBehaviour
{

    public GameObject LobbyPanel;
    public GameObject GamePanel;
    public GameObject WheelMaster;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        StartCoroutine(UpdateLobby());
    }

    IEnumerator UpdateLobby()
    {
        StartGame();
        yield return true;
    }

    void StartGame()
    {
        LobbyPanel.SetActive(false);
        GamePanel.SetActive(true);
        WheelMaster.GetComponent<WheelHandler>().enabled = true;
        WheelMaster.GetComponent<SPCollisionHandler>().enabled = true;
    }

}
