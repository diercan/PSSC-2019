using System;
namespace HomeMadeMarketAPI.Models
{
    public class HomeMadeMarketDatabaseSettings : IHomeMadeMarketDatabaseSettings
    {
       
             public string UserCollectionName { get; set; }
             public string ProductsCollectionName { get; set; }
             public string ProfilesCollectionName { get; set; }
             public string ConnectionString { get; set; }
             public string DatabaseName { get; set; }
        
    }
   public interface IHomeMadeMarketDatabaseSettings {

             string UserCollectionName { get; set; }
             string ProductsCollectionName { get; set; }
             string ProfilesCollectionName { get; set; }
             string ConnectionString { get; set; }
             string DatabaseName { get; set; }
    }

}
