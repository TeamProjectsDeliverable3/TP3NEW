//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TimetablingSystem1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Request
    {
        public int RequestID { get; set; }
        public int RoundID { get; set; }
        public string ModuleCode { get; set; }
        public byte Day { get; set; }
        public byte Period { get; set; }
        public byte Length { get; set; }
        public string SpecialRequirements { get; set; }
        public byte NoOfRooms { get; set; }
        public int NoOfStudents { get; set; }
        public string RoomType { get; set; }
        public string SeatingType { get; set; }
        public string ParkCode { get; set; }
        public byte Priority { get; set; }
        public string Status { get; set; }
    
        public virtual Module Module { internal get; set; }
        public virtual Round Round { internal get; set; }
    }
}