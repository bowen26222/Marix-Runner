using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockController : MonoBehaviour
{
    public GameObject playMenu;
    const int MaxFloor = 1000;
    const int MaxRunner = 100;
    [SerializeField]
    public int[,] map;
    [SerializeField]
    public int mapx, mapy;
    [SerializeField]
    GameObject[] blocks;
    [SerializeField]
    GameObject blockController;
    public GameObject[,] floors;
    [SerializeField]
    GameObject[] Runners = new GameObject[MaxRunner];
    [SerializeField]
    public Vector2Int[] RunnerPosition = new Vector2Int[MaxRunner];
    public float sx, sy;
    public int moveTeam;
    private void Awake()
    {
        playMenu = GameObject.Find("PlayMenu");
        moveTeam = 1;
    }
    void Start()
    {
        floors = new GameObject[mapx+1, mapy+1];
        map = new int[mapx+1, mapy+1];
        for (int i = 0; i <= mapx; i++)
        {
            for (int j = 0; j <= mapy; j++)
            {
                map[i, j] = 0;
            }
        }
        for (int i = 0; i <= mapx; i++)
        {
            for(int j = 0; j <= mapy; j++)
            {
                floors[i,j] = Instantiate(blocks[map[i, j]],new Vector3(sx+i,sy+j,0),blockController.transform.rotation,blockController.transform);
            }
        }
        for (int i = 0; i <= mapx; i++)
        {
            for (int j = 0; j <= mapy; j++)
            {
                floors[i, j].GetComponent<FloorController>().SetFloor(i, j);
            }
        }
        for (int i = 0; i < Runners.Length; i++)
        {
            Runners[i].GetComponent<Runner>().Resprown(sx + RunnerPosition[i].x, sy + RunnerPosition[i].y,i);
            floors[RunnerPosition[i].x, RunnerPosition[i].y].GetComponent<FloorController>().SetRunner(Runners[i].GetComponent<Runner>());
        }
        playMenu.GetComponent<Canvas>().enabled = false;
    }
    void Update()
    {
        
    }
    public void EndTheRound()
    {
        Button[] buttons = playMenu.GetComponentsInChildren<Button>();
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }
        playMenu.GetComponent<Canvas>().enabled = false;
        moveTeam = 1;
    }
}
