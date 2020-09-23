using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHealthScript : MonoBehaviour
{
    public Text HPText;
    public GameObject Player;
    void Start()
    {
        HPText = GetComponent<Text>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        double PlayerHealth = Mathf.Round(Player.GetComponent<PlayerGeneral>().HP),PlayerHealthMax = Mathf.Round(Player.GetComponent<PlayerGeneral>().HPMax);
        string PlayerHealthString = PlayerHealth.ToString()+"/"+PlayerHealthMax.ToString();
        HPText.text = PlayerHealthString;
    }
}
