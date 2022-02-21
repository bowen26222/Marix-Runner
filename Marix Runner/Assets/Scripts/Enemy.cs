using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Runner
{
    public GameObject ReplaceMenu;
    [SerializeField]
    public int faceTo, weakness, shield;
    public bool IsOffShield;
    override public void Resprown(float x, float y, int i)
    {
        faceTo = 1;
        if (My_Renderer.enabled == false)
        {
            transform.position = new Vector2(x, y);
            num = i;
            rx = blockController.RunnerPosition[num].x;
            ry = blockController.RunnerPosition[num].y;
            My_Renderer.enabled = true;
            if(shield == 0)
            {
                IsOffShield = true;
            }
        }
    }
    new public void getDamege(int damage)
    {
        if(IsOffShield == false)
        {
            blood -= (damage / 2);
        }
        else
        {
            blood -= damage;
        }
        if (blood <= 0)
        {
            die();
        }
    }
    public void BackAttack()
    {
        GameObject.Find("ReplaceMenu").GetComponentInChildren<Canvas>().enabled = true;
        weaponController.ReplaceWeapon = weaponNum;
        die();
    }
}
