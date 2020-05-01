using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniMapHHS.Models;

namespace UniMapHHS.Mocking
{
    public interface IMock
    {
        //Both
        public List<Glossary> GetGlossary();
        public void SetContext(DbAppContext context);
        public string GetTempUsername(Controller cont);
        public string GetTempRole(Controller cont);
        public void SetTempUsername(Controller cont, string username);
        public void SetTempRole(Controller cont, string role);

        //Home
        public Category[] GetCategories();

        public List<Favourite> GetFavourites();

        public List<BuildingFloor> GetBuildingFloors();
        
        //Analytics
        public List<Location> GetLocations();

        public List<History> GetHistory();

        public bool SetLockedLocation(Location location);

        public void AddFavourite(Favourite favourite);

        public void RemoveFavourite(Favourite favourite);

        public void DeleteAccount(string username);

        //Login
        public List<User> GetUsers();

        public void SetLangPref(string lang, string username);
    }
}
