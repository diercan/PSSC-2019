using System;
using FrBaschet.Domain.Models;

namespace FrBaschet.Domain.Entities
{
    public class GameEntityModel : EntityModel
    {
        public DateTime Date { get; set; }
        public TeamEntityModel HomeTeamEntityModel { get; set; }
        public TeamEntityModel AwayTeamEntityModel { get; set; }
        public RefereeEntity Referee1 { get; set; }
        public RefereeEntity Referee2 { get; set; }
        public CommissionerEntity Commissioner { get; set; }
    }
}