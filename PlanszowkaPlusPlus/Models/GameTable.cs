namespace PlanszowkaPlusPlus.Models;
using System.ComponentModel.DataAnnotations;


    public class GameTable
    {
        //TODO:constructor public Table() { generateId(); Number...; isFree = true; }
        public int Id { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be at least 0.")]
        public int Number { get; set; }
        public bool IsFree { get; set; }
    }

