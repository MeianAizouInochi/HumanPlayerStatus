using System;

namespace DataModelLayer.DataModels
{
    public class StatHistory
    {
        public string? id { get; set; } 

        public string? Id { get; set; }

        public int[] STR_history { get; set; } = new int[3];

        public int[] AGI_history { get; set; } = new int[3];

        public int[] INT_history { get; set; } = new int[3];

        public int[] COMM_history { get; set; } = new int[3];

        public int[] MENSTB_history { get; set; } = new int[3];

        public int[] CRT_history { get; set; } = new int[3];

        public int[] HP_history { get; set; } = new int[3];

        public int[] HYG_history { get; set; } = new int[3];

    }
}
