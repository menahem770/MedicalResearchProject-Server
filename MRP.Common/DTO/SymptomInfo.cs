using System;

namespace MRP.Common.DTO
{
    public class SymptomInfo
    {
        public string SymptomName { get; set; }
        public bool BoolValue { get; set; }
        public string StringValue { get; set; }
        public int NumberValue { get; set; }
        public DateTime DateValue { get; set; }
    }
}