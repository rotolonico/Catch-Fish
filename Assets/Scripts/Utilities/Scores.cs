using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    [Serializable]
    public class Scores
    {
        public Dictionary<string, string> Easy;
        public Dictionary<string, string> Medium;
        public Dictionary<string, string> Hard;
    }
}
