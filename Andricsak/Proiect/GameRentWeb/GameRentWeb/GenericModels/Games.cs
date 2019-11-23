using GameRentWeb.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GameRentWeb.GenericModels
{
    public class Games
    {
        private List<Game> _games;
        public ReadOnlyCollection<Game> Values { get { return _games.AsReadOnly(); } }

        internal Games()
        {
            _games = new List<Game>();
        }

        internal void AddGame(Game game)
        {
            _games.Add(game);
        }
        internal void DeleteGame(Game game)
        {
            _games.Remove(game);
        }
    }
}
