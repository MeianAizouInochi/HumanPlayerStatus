using System;

namespace DataModelLayer.DataModels
{
    public class PlayerStats
    {
        public string? id { get; set; }

        public string? Id { get; set; }

        public long HumanPlayerLevel { get; set; }

        public long HumanPlayerExp { get; set; }

        public long HumanPlayerLevelMaxExp { get; set; }

        public float STR { get; set; }

        public float AGI { get; set; }
               
        public float INT { get; set; }
               
        public float COMM { get; set; }
               
        public float MENSTB { get; set; }
               
        public float CRT { get; set; }
               
        public float HP { get; set; }
               
        public float HYG { get; set; }

        public float[] DebuffRates { get; set; } = new float[8];

        public string[]? Traits { get; set; }

        public string[]? Debuffs { get; set; }
    }
}
