﻿namespace HealthTracker.Server.Modules.Community.DTOs
{
    public class CreateMessageDTO
    {
        public int UserIdFrom { get; set; }
        public int UserIdTo { get; set; }
        public required string Text { get; set; }
    }
}
