﻿namespace BreweryWholesaleManagement.Models
{
    public class BaseObject
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }
    }
}
