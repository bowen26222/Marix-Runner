using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    [SerializeField]
    public int demage;
    [SerializeField]
    public Runner nowRunner;
    [SerializeField]
    public BlockController blockController;
    public bool IfReadyAttack;
    public int weaponType;
    virtual public void showAttackRange() { }
    virtual public void Attack(Runner attackRunner) { }
    public void TrunFloorToRed(int i, int j)
    {
        blockController.floors[i, j].GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
        blockController.floors[i, j].GetComponent<FloorController>().IsInTheWeaponRange = true;
        blockController.floors[i, j].GetComponent<FloorController>().ChosenWeapon = this;
    }
    public void TrunFloorToYellow(int i, int j)
    {
        blockController.floors[i, j].GetComponent<SpriteRenderer>().color = new Color(242, 255, 0, 255);
        blockController.floors[i, j].GetComponent<FloorController>().IsInTheWeaponRange = true;
        blockController.floors[i, j].GetComponent<FloorController>().ChosenWeapon = this;
    }
    public void TrunFloorToNormal(int i, int j)
    {
        blockController.floors[i, j].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        blockController.floors[i, j].GetComponent<FloorController>().IsInTheWeaponRange = false;
        blockController.floors[i, j].GetComponent<FloorController>().ChosenWeapon = null;
    }
    public bool checkPosition(Enemy attackRunner)
    {
        if (attackRunner.rx == nowRunner.rx && attackRunner.ry == nowRunner.ry - 1 && attackRunner.faceTo == 4)
        {
            return true;
        }
        else if (attackRunner.rx == nowRunner.rx && attackRunner.ry == nowRunner.ry + 1 && attackRunner.faceTo == 2)
        {
            return true;
        }
        else if (attackRunner.rx == nowRunner.rx + 1 && attackRunner.ry == nowRunner.ry && attackRunner.faceTo == 3)
        {
            return true;
        }
        else if (attackRunner.rx == nowRunner.rx - 1 && attackRunner.ry == nowRunner.ry && attackRunner.faceTo == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
