using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PressIt
{
    public interface IPresseble
    {
        void Smash(float multiplier);
        Transform GetTransform();
        GameObject GetGameObject();
    }
}