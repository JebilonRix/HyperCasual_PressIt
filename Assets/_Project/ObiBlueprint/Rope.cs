using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

namespace PressIt
{
    public class Rope : MonoBehaviour
    {
        public ObiRopeCursor orc;
        public ObiRope or;

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                orc.ChangeLength(or.restLength + 1f);
            }
        }
    }
}