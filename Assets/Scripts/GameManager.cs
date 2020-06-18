using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Axis_of_rotation;
    public GameObject Buttons;
    public GameObject[] Panel = new GameObject[9];
    public GameObject ShuffleButton;
    public Sprite[] PanelPicture = new Sprite[2];
    public GameObject ScoreText;
    public GameObject TimeText;

    private int[] Panelcolor = new int[9];//0:オフ 1:オン 2:当たり
    public int CanPush = 0;
    public int i = 0;
    public int RandP = 3;
    public int RandC = 0;
    public int RotateVec = 1;
    public int xyz = 0;
    public float ThisTime = 0;
    public int CorrectCount = 0;
    public float intime;

    public int Score=0;

    public int[] RotateSwitch=new int[2];
    /*パネルのクリック判定*/
    public void PushPanel0()
    {
        JudgePanel(0);
    }
    public void PushPanel1()
    {
        JudgePanel(1);
    }
    public void PushPanel2()
    {
        JudgePanel(2);
    }
    public void PushPanel3()
    {
        JudgePanel(3);
    }
    public void PushPanel4()
    {
        JudgePanel(4);
    }
    public void PushPanel5()
    {
        JudgePanel(5);
    }
    public void PushPanel6()
    {
        JudgePanel(6);
    }
    public void PushPanel7()
    {
        JudgePanel(7);
    }
    public void PushPanel8()
    {
        JudgePanel(8);
    }

    async void JudgePanel(int PanelNo)
    {
        if (CanPush == 1)
        {
            if (Panelcolor[PanelNo] == 0)
            {
                //ミス処理
                CanPush = 0;
                Debug.Log("パネル"+PanelNo+"は不正解パネルです");
                ShuffleButton.GetComponentInChildren<Text>().text = "不正解";
                CorrectCount = 0;
                await Task.Delay(1000);
                ShufflePanel();
                
            }
            else if (Panelcolor[PanelNo] == 3)
            {
                //正解処理
                Panelcolor[PanelNo] = 1;
                Panel[PanelNo].GetComponent<Image>().sprite =
                    PanelPicture[1];
                CorrectCount++;
                Debug.Log("残りパネル:" + (RandP - CorrectCount));
                if (CorrectCount == RandP)
                {
                    ShuffleButton.GetComponentInChildren<Text>().text = "全パネル正解";
                    Debug.Log("全パネル正解");
                    Score++;
                    CorrectCount = 0;
                    await Task.Delay(1000);
                    if (Score % 5 == 0)
                    {
                        RandP++;
                        ShuffleButton.GetComponentInChildren<Text>().text = "Lv.UP!";
                        await Task.Delay(1000);
                    }
                    CanPush = 0;
                    ShufflePanel();
                }
            }
        }
    }
    /*判定ここまで*/

    public void ShufflePanel()
    {
        //初期化
        for (i = 0; i < 9; i++)
        {
            Panelcolor[i] = 0;
        }

        //振り分け部
        while (RandC < RandP)
        {
            i = Random.Range(0, 9);
            if (Panelcolor[i] == 0)
            {
                Panelcolor[i] = 3;
                RandC++;
            }
            if (RandC == RandP)
            {
                RandC = 0;
                break;
            }

        }

        //表示部
        for (i = 0; i < 9; i++)
        {
            if (Panelcolor[i] == 0)
            {
                Panel[i].GetComponent<Image>().sprite =
                    PanelPicture[0];
            }
            else if (Panelcolor[i] == 3)
            {
                Panel[i].GetComponent<Image>().sprite =
                    PanelPicture[1];
            }
        }
        ShuffleButton.GetComponentInChildren<Text>().text = "覚えた";
    }


    //パネル隠し
    public async void PushHideButton()
    {
        if (CanPush == 0) { 
        //表示部
        await Task.Delay(10);
        for (i = 0; i < 9; i++)
            {
            Panel[i].GetComponent<Image>().sprite =
                    PanelPicture[0];
                 CanPush = 1;
            }
        await Task.Delay(10);
            //回転部
            xyz = Random.Range(0, 3);
            //Z軸:0
            if (xyz == 0)
            {
                i = Random.Range(0, 2);
                Debug.Log("回転方向:" + i);
                if (i == 1) RotateVec = -1;
                else RotateVec = 1;
                RotateSwitch[i] = 0;
                for (RotateSwitch[i] = 0; RotateSwitch[i] < 18 && i != 2; RotateSwitch[i]++)
                {
                    await Task.Delay(10);
                    Axis_of_rotation.transform.Rotate(new Vector3(0, 0, 5.0f * RotateVec));
                    Debug.Log("Z軸回転" + RotateSwitch[i]);
                }
            }
            //Y軸:1
            else if (xyz == 1)
            {
                i = Random.Range(0, 2);
                Debug.Log("回転方向:" + i);
                if (i == 1) RotateVec = -1;
                else RotateVec = 1;
                RotateSwitch[i] = 0;
                for (RotateSwitch[i] = 0; RotateSwitch[i] < 18 && i != 2; RotateSwitch[i]++)
                {
                    await Task.Delay(15);
                    Buttons.transform.Rotate(new Vector3(0, 10.0f * RotateVec, 0));
                    Debug.Log("Y軸回転" + RotateSwitch[i]);
                }
                foreach(Transform child in Buttons.transform)
                {
                    child.transform.Rotate(new Vector3(0, 180.0f, 0));
                }

            }
            else if (xyz == 2)
            {
                i = Random.Range(0, 2);
                Debug.Log("回転方向:" + i);
                if (i == 1) RotateVec = -1;
                else RotateVec = 1;
                RotateSwitch[i] = 0;
                for (RotateSwitch[i] = 0; RotateSwitch[i] < 18 && i != 2; RotateSwitch[i]++)
                {
                    await Task.Delay(15);
                    Buttons.transform.Rotate(new Vector3(10.0f * RotateVec, 0, 0));
                    Debug.Log("Y軸回転" + RotateSwitch[i]);
                }
                foreach (Transform child in Buttons.transform)
                {
                    child.transform.Rotate(new Vector3(0, 180.0f, 0));
                }

            }
        }
        else if (CanPush == 2)
        {
            //タイトル移動処理
            SceneManager.LoadScene("Title1");
        }
        ShuffleButton.GetComponentInChildren<Text>().text = "パネルを選択";

    }

    // Start is called before the first frame update
    async void Start()
    {
        CanPush = 0;
        intime = Time.time;
        ShuffleButton.GetComponentInChildren<Text>().text = " ";
        await Task.Delay(1000);
        ShuffleButton.GetComponentInChildren<Text>().text = "3";
        await Task.Delay(1000);
        ShuffleButton.GetComponentInChildren<Text>().text = "2";
        await Task.Delay(1000);
        ShuffleButton.GetComponentInChildren<Text>().text = "1";
        await Task.Delay(1000);
        for (i = 0; i < 9; i++)
        {
            Panelcolor[i] = 1;
        }

        ShufflePanel();
    }

    // Update is called once per frame
    void Update()
    {
        //スコア表示
        ScoreText.GetComponent<Text>().text = "SCORE:"+Score;
        ThisTime = 64.00f+intime - Time.time;
        if (ThisTime >= 0.00f)
        {
            if (ThisTime <= 60.00f) TimeText.GetComponent<Text>().text = "TIME:" + ThisTime.ToString("f2");
        }
        else
        {
            TimeText.GetComponent<Text>().text = "TIME:0.00";
            ShuffleButton.GetComponentInChildren<Text>().text = "終了";
            CanPush = 2;
        }


     
    }
}
