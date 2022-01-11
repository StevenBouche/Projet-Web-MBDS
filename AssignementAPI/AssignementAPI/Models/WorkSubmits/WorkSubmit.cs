﻿using Assignment.DAL.Models;
using System.Text.Json.Serialization;

namespace AssignmentAPI.Models.WorkSubmits
{
    public class WorkSubmit
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;
        [JsonPropertyName("grade")]
        public double Grade { get; set; }
        [JsonPropertyName("comment")]
        public string Comment { get; set; } = string.Empty;
        [JsonPropertyName("state")]
        public WorkSubmitState State { get; set; }

        public WorkSubmit()
        {

        }

        public WorkSubmit(WorkSubmitEntity entity)
        {
            Id = entity.Id;
            Label = entity.Label;
            Grade = entity.Grade;
            Comment = entity.Comment;
            State = entity.State;
        }
    }
}
