using System;
using System.Collections.Generic;

namespace RhythmNamespace
{
    [Serializable]
    public class RhythmData
    {
        public string name;
        public int bpm;
        public int[] pattern = new int[16];
    }

    [Serializable]
    public class RhythmFile
    {
        public List<RhythmData> rhythms;
    }
}

