using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    // The purpose of this object is essentially to hold the selected team and take it to the next scene for it to manage.
    
    private string selectedTeam;
    public int score; // We can add whatever to this script, these are just some of the things I imagine would be in it. 
    public string playerName;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("PlayerInfo");

        if(objs.Length  > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public string GetTeam()
    {
        return selectedTeam;
    }

    public void SetTeam(string team)
    {
        selectedTeam = team;
    }
}
