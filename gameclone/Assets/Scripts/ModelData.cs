using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This file contains data structures used to represent the Model
 */

namespace ModelData
{
    // ############ Data types ###########
    [System.Serializable]
    public enum RatCellType
    {
        CashDay,
        Opportynity,
        Market,
        Doodle,
        Charity,
        Child,
        LostTheJob
    };

    [System.Serializable]
    public enum FastCellType
    {
        Commertial,
        NonCommertial,
        CashDay,
        LostTheMoney,
        CommertialSingleTime,
        MercyAction
    };

    [System.Serializable]
    public class FastCircleCell
    {
        public FastCircleCell() { }
        public FastCircleCell(FastCellType type, string name)
        {
            this.cellType = type;
            this.name = name;
        }

        public FastCellType cellType;
        public string name;
        public string text;
        public int price;
        public int income;
        public int scoreThreshold;
        public int looseTheMoneyPercentage;
    };

    class Pair<T1, T2>
    {
        public Pair() { }
        public Pair(T1 first, T2 second)
        {
            this.First = first;
            this.Second = second;
        }

        public T1 First;
        public T2 Second;
    }

    [System.Serializable]
    class RatCellType2Name : Pair<RatCellType, string>
    {
        public RatCellType2Name() { }
        public RatCellType2Name(RatCellType first, string second) : base(first, second) { }
    }
}