using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DhakaStockExchangeWorker
{
    public class DSEModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid SerialNo { get; set; }

        public long Id { get; set; }

        [Required]
        public string TrandingCode { get; set; }

        public double LTP { get; set; }

        public double High { get; set; }

        public double Low { get; set; }

        public double Closep { get; set; }

        public double YCP { get; set; }

        public double Change { get; set; }

        public long Trade { get; set; }

        public double Value { get; set; }

        public long Volume { get; set; }
    }
}
