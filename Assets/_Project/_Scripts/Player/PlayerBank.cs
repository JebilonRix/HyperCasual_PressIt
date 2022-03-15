using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PressIt
{
    public class PlayerBank : MonoBehaviour
    {
        [SerializeField] uint _money = 0;

        public void GainMoney(uint amount)
        {
            _money += amount;

            Debug.Log(amount);
        }
    }
}
