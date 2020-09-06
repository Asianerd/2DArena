using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGeneral : MonoBehaviour
{
    //HP
    public double HP = 100, HPMax = 100, HPRegen = 1;
    public int HPRegenTime = 100, HPRegenClock = 0;

    void Update()
    {
        Regen();
        Debug.Log("HP " + HP + "/" + HPMax);
    }
    public void MinusHealth(float losthealth)
    {
        HP -= losthealth;
    }
    void Regen()
    {
        if(HPRegenClock == 0)
        {
            if (HP < HPMax)
            {
                if ((HP + HPRegen > HPMax) && (HP < HPMax))
                {
                    HP = HPMax;
                }
                else
                {
                    HP += HPRegen;
                }
            }
        }
        HPRegenClock++;
        if (HPRegenClock >= HPRegenTime+1) //>= so that HPRegenClock has 100 variants
            HPRegenClock = 0;
    }
}
