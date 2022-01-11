﻿namespace Assignment.DAL.Models
{
    public class CourseImageEntity : BaseModel
    {
        public string Extention { get; set; } = string.Empty;
        public byte[] Data { get; set; } = Array.Empty<byte>();
        public virtual CourseEntity Course { get; set; } = new();
        public int CourseId { get; set; }
    }
}
