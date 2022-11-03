using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUi : MonoBehaviour
{
    public bool Player;
    public bool BoolOfFocus;

    public SpriteRenderer[] SpriteRenderersOfHp;
    public SpriteRenderer[] SpriteRenderersOfSta;

    public SpriteRenderer SpriteRendererOfMainsprite;
    public SpriteRenderer SpriteRendererOfBackSprite;

    public List<Color> colorOfUi;

    public void UpdateOfCharacterUi(int Hp, int Sta)
    {
        HpManage(Hp);
        StaminaManage(Sta);
        LayerManager();
    }

    public void HpManage(int inputHp)
    {
        int leftOfHp = inputHp % 4;
        inputHp -= leftOfHp;
        inputHp /= 4;

        Color colorOfBase = colorOfUi[0];
        Color colorOfBig = colorOfUi[1];

        switch (inputHp)
        {
            case 0:
                colorOfBase = colorOfUi[0];
                colorOfBig = colorOfUi[3];
                break;
            case 1:
                colorOfBase = colorOfUi[3];
                colorOfBig = colorOfUi[4];
                break;
            case 2:

                colorOfBase = colorOfUi[3];
                colorOfBig = colorOfUi[4];
                break;
            default:
                colorOfBase = colorOfUi[3];
                colorOfBig = colorOfUi[4];
                break;
        }

        for (int i = 0; i < 4; i++)
        {
            SpriteRenderersOfHp[i].color = colorOfBase;
        }

        for (int i = 0; i < leftOfHp; i++)
        {
            SpriteRenderersOfHp[i].color = colorOfBig;
        }

    }

    public void StaminaManage(int inputSta)
    {
        int leftOfHp = inputSta % 4;
        inputSta -= leftOfHp;
        inputSta /= 4;

        Color colorOfBase = colorOfUi[0];
        Color colorOfBig = colorOfUi[1];

        switch (inputSta)
        {
            case 0:
                colorOfBase = colorOfUi[0];
                colorOfBig = colorOfUi[1];
                break;
            case 1:
                colorOfBase = colorOfUi[1];
                colorOfBig = colorOfUi[2];
                break;
            case 2:

                colorOfBase = colorOfUi[1];
                colorOfBig = colorOfUi[2];
                break;
            default:
                colorOfBase = colorOfUi[1];
                colorOfBig = colorOfUi[2];
                break;
        }

        for (int i = 0; i < 4; i++)
        {
            SpriteRenderersOfSta[i].color = colorOfBase;
        }

        for (int i = 0; i < leftOfHp; i++)
        {
            SpriteRenderersOfSta[i].color = colorOfBig;
        }

    }

    public void LayerManager()
    {
        
        if(BoolOfFocus == true)
        {
            // 체력바들 뒤로
            foreach (SpriteRenderer i in SpriteRenderersOfHp)
            {
                i.sortingOrder = 40;
            }

            foreach (SpriteRenderer i in SpriteRenderersOfSta)
            {
                i.sortingOrder = 40;
            }

            // 기본스프라이트들
            SpriteRendererOfMainsprite.sortingOrder = 30;
            SpriteRendererOfBackSprite.sortingOrder = 20;
        }
        else
        {
            // 체력바들 뒤로
            foreach (SpriteRenderer i in SpriteRenderersOfHp)
            {
                i.sortingOrder = -6;
            }

            foreach (SpriteRenderer i in SpriteRenderersOfSta)
            {
                i.sortingOrder = -6;
            }
            //Debug.Log(name);

            // 기본스프라이트들
            SpriteRendererOfMainsprite.sortingOrder = -7;
            SpriteRendererOfBackSprite.sortingOrder = -8;
        }
    }

}
