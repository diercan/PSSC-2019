using System;

namespace FrBaschet.Domain.ViewModels
{
    public class UpdateGameViewModel
    {
        public Guid Referee1 { get; set; }
        public Guid Referee2 { get; set; }
        public Guid Commissioner { get; set; }
    }
}