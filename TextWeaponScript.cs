using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWeaponScript : MonoBehaviour
{
    public Text SelfText;
    public GameObject Parent;
    public Camera MainCam;
    public GameObject WeaponLootParent;
    void Start() 
    {
        MainCam = FindObjectOfType<Camera>();
    }

    void Update()
    {
        SelfText.text = Parent.GetComponent<LootGeneral>().WeaponName;
        transform.position = MainCam.WorldToScreenPoint(WeaponLootParent.transform.position);
    }
}
