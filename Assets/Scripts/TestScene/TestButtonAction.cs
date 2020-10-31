using System;
using UnityEngine;

namespace BBO.BBO.TestScene
{
    public class TestButtonAction : MonoBehaviour
    {
#if UNITY_EDITOR

        public void OnClickButton1()
        {
            throw new NotImplementedException();
        }

        public void OnClickButton2(bool val)
        {
            throw new NotImplementedException();
        }

        public void OnClickButton3(string val)
        {
            throw new NotImplementedException();
        }

        public void OnClickButton4(int val)
        {
            throw new NotImplementedException();
        }

#endif
    }
}
