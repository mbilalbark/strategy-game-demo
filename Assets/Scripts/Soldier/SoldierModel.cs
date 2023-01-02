using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STGD.Core.Base
{
    public class SoldierModel : UnitModel
    {
        [SerializeField]
        private float speed;
        public float Speed { get { return speed;  } set { speed = value; } }

        [SerializeField]
        private int damage;

        public int Damage { get { return damage; } }

        [SerializeField]
        private float attackSpeed;

        public float AttackSpeed { get { return attackSpeed; } }

    }
}
