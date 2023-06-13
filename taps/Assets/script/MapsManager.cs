using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapsManager : MonoBehaviour
{

    public List<string> GetVs;

    /// <summary>
    /// ���, �ٴ�, ������Ʈ ��
    /// </summary>
    public List<string> GetVs1;
    public List<string> GetVs2;
    public List<string> GetVs3;

    public GameManager gs;

    private void start()
    {
        gs = GameManager.instance;
    }

    public List<Text> texts;

    private void Update()
    {
        if(gs == null)
        {
            gs = GameManager.instance;
        }

        switch (GameManager.instance.mapnumber)
        {
            case 0:
                texts[0].text = GetVs[0] + "��";
                texts[1].text = GetVs[1] + "����";
                texts[2].text = GetVs[2] + "����";
                break;
            case 1:
                texts[0].text = GetVs[0] + "����";
                texts[1].text = GetVs[1] + "����";
                texts[2].text = GetVs[2] + GetVs1[gs.map1[0]]+", " + GetVs2[gs.map1[1]] + ", " + GetVs3[gs.map1[2]];
                break;
            case 2:
                texts[0].text = GetVs[0] + "����";
                texts[1].text = GetVs[1] + GetVs1[gs.map1[0]] + ", " + GetVs2[gs.map1[1]] + ", " + GetVs3[gs.map1[2]];
                texts[2].text = GetVs[2] + GetVs1[gs.map2[0]] + ", " + GetVs2[gs.map2[1]] + ", " + GetVs3[gs.map2[2]];
                break;
            case 3:
                texts[0].text = GetVs[0] + GetVs1[gs.map1[0]] + ", " + GetVs2[gs.map1[1]] + ", " + GetVs3[gs.map1[2]];
                texts[1].text = GetVs[1] + GetVs1[gs.map2[0]] + ", " + GetVs2[gs.map2[1]] + ", " + GetVs3[gs.map2[2]];
                texts[2].text = GetVs[2] + GetVs1[gs.map3[0]] + ", " + GetVs2[gs.map3[1]] + ", " + GetVs3[gs.map3[2]];
                break;                                       
            case 4:
                texts[0].text = GetVs[0] + GetVs1[gs.map2[0]] + ", " + GetVs2[gs.map2[1]] + ", " + GetVs3[gs.map2[2]];
                texts[1].text = GetVs[1] + GetVs1[gs.map3[0]] + ", " + GetVs2[gs.map3[1]] + ", " + GetVs3[gs.map3[2]];
                texts[2].text = GetVs[2] + GetVs1[gs.map4[0]] + ", " + GetVs2[gs.map4[1]] + ", " + GetVs3[gs.map4[2]];
                break;
            case 5:
                texts[0].text = GetVs[0] + GetVs1[gs.map3[0]] + ", " + GetVs2[gs.map3[1]] + ", " + GetVs3[gs.map3[2]];
                texts[1].text = GetVs[1] + GetVs1[gs.map4[0]] + ", " + GetVs2[gs.map4[1]] + ", " + GetVs3[gs.map4[2]];
                texts[2].text = GetVs[2] + GetVs1[gs.map5[0]] + ", " + GetVs2[gs.map5[1]] + ", " + GetVs3[gs.map5[2]];
                break;
            case 6:
                texts[0].text = GetVs[0] + GetVs1[gs.map4[0]] + ", " + GetVs2[gs.map4[1]] + ", " + GetVs3[gs.map4[2]];
                texts[1].text = GetVs[1] + GetVs1[gs.map5[0]] + ", " + GetVs2[gs.map5[1]] + ", " + GetVs3[gs.map5[2]];
                texts[2].text = GetVs[2] + "��";
                break;
            default:
                break;
        }


    }


}
