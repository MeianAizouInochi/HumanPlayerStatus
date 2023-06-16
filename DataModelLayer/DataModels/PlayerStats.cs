using System;

namespace DataModelLayer.DataModels
{
    public class PlayerStats
    {
        public string? id { get; set; }

        public string? Id { get; set; }

        public int HumanPlayerLevel { get; set; }

        public long HumanPlayerExp { get; set; }

        public long HumanPlayerLevelMaxExp { get; set; }

        public int STR { get; set; }

        public int AGI { get; set; }

        public int INT { get; set; }

        public int COMM { get; set; }

        public int MENSTB { get; set; }

        public int CRT { get; set; }

        public int HP { get; set; }

        public int HYG { get; set; }
    }
}
