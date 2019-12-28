using GameRentWeb.ExceptionClasses;
using GameRentWeb.Models;
using GameRentWeb.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GameRentWeb.GenericModels
{
    public class GameDDD
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public int Qty { get; private set; }


        //operations

        public GameDDD RentGame()
        {
            if (Qty > 0)
                Qty--;
            else
                throw new Exception("Not available");
            return this;
        }
        //private List<Game> _games;

        //public ReadOnlyCollection<Game> Values { get { return _games.AsReadOnly(); } }

        //internal GameDDD(IDataBaseRepo<Game> repo)
        //{
        //    _games = repo.GetAllObjects().Result.ToList();
        //}

        //internal void AddGame(Game game, IDataBaseRepo<Game> repo)
        //{
        //    if (IsFound(game,repo))
        //    {
        //        repo.Insert(game);
        //        _games.Add(game);
        //    }
        //    else
        //    {
        //        throw new ItemNotFound(game.Name);
        //    }
        //}
        //internal void DeleteGame(Game game,IDataBaseRepo<Game> repo)
        //{
        //    if (IsFound(game, repo))
        //    {
        //        _games.Remove(game);
        //        repo.Delete(game.Id);
        //    }
        //    else
        //    {
        //        throw new ItemNotFound(game.Name);
        //    }

        //}
        //internal bool IsFound(Game game,IDataBaseRepo<Game> repo)
        //{
        //    if (_games.Find(g => g.Id == game.Id) != null)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

    }
}
