using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using CenterSpace.NMath.Core;
namespace CenterSpace.NMath.Core.Examples.CSharp
{

    public class DollarScript : MonoBehaviour
    {
        Text Dollars;
        public float DollarFloat;
        private string file = "points.dat";
        void Start()
        {
            Dollars = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            DollarFloat = CrashPointCalc.TotalPoints / 1.2007f;
        }
    }
}
