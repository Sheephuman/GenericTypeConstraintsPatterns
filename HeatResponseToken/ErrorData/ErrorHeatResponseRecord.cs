using System;
using System.Collections.Generic;
using System.Text;

namespace HeatResponseToken.ErrorData
{
    public class ErrorHeatResponseRecord
    {
        public string Location { get; set; } = string.Empty;

        public DateTime RecordedAt { get; set; }

        public double Temperature { get; set; }
    }
}
