using UnityEngine;

namespace EnumTypes
{
    public enum Item
    {
        Coin,
        //코인은 3종류, 각각 프리팹에서 조정
        Speed,
        //속도 2종류
        Shop,
        Last
    }
    public enum Tier
    {
        Defult,
        Desert,
        mountain,
        City,
        Last
    }

    public enum Shop
    {
        Tier,
        Engine,
        Booster,
        Last
    }
}
