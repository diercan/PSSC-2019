using System;

namespace FrBaschet.Domain.ViewModels
{
    public class GameView
    {
        public string title { get; set; }
        public DateTime data { get; set; }
        public string commissioner { get; set; }
        public string referee1 { get; set; }
        public string referee2 { get; set; }
    }
}