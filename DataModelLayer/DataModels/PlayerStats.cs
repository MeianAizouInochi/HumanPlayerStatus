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

        public double STR { get; set; }

        public double AGI { get; set; }

        public double INT { get; set; }

        public double COMM { get; set; }

        public double MENSTB { get; set; }

        public double CRT { get; set; }

        public double HP { get; set; }

        public double HYG { get; set; }
    }
}
