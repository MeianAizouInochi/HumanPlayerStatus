using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelLayer.DataModels
{
    public class TransactionRecordModel
    {
        public string? id { get; set; }

        public string? Id { get; set; }

        public int[] Date { get; set; }

        public string[]? Items { get; set; }

        public string[]? ItemAmounts { get; set; }

        public float[] Credit { get; set; }

        public float[] Debit { get; set; }

        public string? Remarks { get; set; }

        public TransactionRecordModel()
        {
            
        }
    }
}
