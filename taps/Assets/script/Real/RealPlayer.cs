using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealPlayer : RealParty
{
    /// <summary>
    /// 플레이어의 최대 스태미너
    /// </summary>
    public float floatOfMaxStamina;

    /// <summary>
    /// 플레이어의 현재 스태미너
    /// </summary>
    public float floatOfStamina;

    /// <summary>
    /// 1. 플레이어 2. 기사 3. 사무라이 4. 수도승
    /// </summary>
    public int intOfType;

    public int LEVEL;

    public void NewPlayer(float hp, float sta, int type, int level =0)
    {
        floatOfMaxHp = 4;
        floatOfMaxStamina = 8;
        floatOfHp = hp;
        floatOfStamina = sta;
        intOfType = type;
        LEVEL = level; 
    }
}


// 전투 디자인

// 플레이어는 총 4명의 캐릭터가 있고 강점도 다르다

// 주인공   캐릭터의 강점은 단일 공격
// 기사     캐릭터의 강점은 지속력
// 사무라이 캐릭터의 강점은 광역공격
// 수도승   캐릭터의 강점은 회복 능력

// 캐릭터의 능력 발휘는 크게 2가지이다

// 적을 처지했을떄 발동
// 노트를 일정 수 성공시 발동

// 주인공 -    다음 대상에 피해를 가함