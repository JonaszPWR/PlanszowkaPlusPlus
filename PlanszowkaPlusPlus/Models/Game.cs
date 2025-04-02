namespace PlanszowkaPlusPlus.Models;
using System.ComponentModel.DataAnnotations;

    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be at least 0.")]
        public int TotalNumber { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be at least 0.")]
        public int AvailableNumber { get; set; }
    }
