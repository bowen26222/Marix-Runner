
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class fatiaosword : weapon
{
    int[,] changes = new int[4, 2];
    private void Awake()
    {
        changes[0, 0] = 1;
        changes[1, 0] = -1;
        changes[2, 1] = 1;
        changes[3, 1] = -1;
        blockController = GameObject.Find("BlockController").GetComponent<BlockController>();
    }
    override public void showAttackRange() {
        if(IfReadyAttack == false)
        {
            blockController.floors[nowRunner.rx, nowRunner.ry].GetComponent<FloorController>().IsChosen = true;
            blockController.floors[nowRunner.rx, nowRunner.ry].GetComponent<FloorController>().ChosenWeapon = this;
            for (int i = 0; i < 4; i++)
            {
                if (nowRunner.rx + changes[i, 0] >= 0 && nowRunner.rx + changes[i, 0] <= blockController.mapx && nowRunner.ry + changes[i, 1] >= 0 && nowRunner.ry + changes[i, 1] <= blockController.mapy)
                {
                    if (blockController.map[nowRunner.rx + changes[i, 0], nowRunner.ry + changes[i, 1]] == -1) continue;
                    TrunFloorToRed(nowRunner.rx + changes[i, 0], nowRunner.ry + changes[i, 1]);
                }
            }
            IfReadyAttack = true;
        }
        else
        {
            blockController.floors[nowRunner.rx, nowRunner.ry].GetComponent<FloorController>().IsChosen = false;
            for (int i = 0; i < 4; i++)
            {
                if (nowRunner.rx + changes[i, 0] >= 0 && nowRunner.rx + changes[i, 0] <= blockController.mapx && nowRunner.ry + changes[i, 1] >= 0 && nowRunner.ry + changes[i, 1] <= blockController.mapy)
                {
                    if (blockController.map[nowRunner.rx + changes[i, 0], nowRunner.ry + changes[i, 1]] == -1) continue;
                    TrunFloorToNormal(nowRunner.rx + changes[i, 0], nowRunner.ry + changes[i, 1]);
                }
            }
            blockController.floors[nowRunner.rx, nowRunner.ry].GetComponent<FloorController>().ChosenWeapon = null;
            blockController.floors[nowRunner.rx, nowRunner.ry].GetComponent<FloorController>().playMenu.GetComponent<Canvas>().enabled = false;
            IfReadyAttack = false;
        }
    }
    override public void Attack(Runner attackRunner) { 
        if(IfReadyAttack == true)
        {
            if (attackRunner.GetType() == typeof(Enemy))
            {
                Enemy attackEnemy = (Enemy)attackRunner;
                if (checkPosition(attackEnemy) && attackEnemy.IsOffShield == true)
                {
                    attackEnemy.BackAttack();
                }
                else
                {
                    attackEnemy.getDamege(demage);
                }
            }
            else
            {
                attackRunner.getDamege(demage);
            }
        }
    }
}
