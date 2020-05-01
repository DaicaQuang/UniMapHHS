using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UniMapHHS.Models;

namespace UniMapHHS.Mocking
{
    public class MockHandler : IMock
    {
        public void SetContext(DbAppContext context)
        {
            // Method intentionally left empty.
        }

        public List<Favourite> Favourites { get; set; }
        public Category[] Categories { get; set; }
        public List<Glossary> Glossaries { get; set; }
        public List<BuildingFloor> BuildingFloors { get; set; }
        public List<History> Histories { get; set; }
        public List<Location> Locations { get; set; }
        public List<User> Users { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }


        public List<BuildingFloor> GetBuildingFloors()
        {
            return BuildingFloors;
        }

        public Category[] GetCategories()
        {
            return Categories;
        }

        public List<Favourite> GetFavourites()
        {
            return Favourites;
        }

        public List<Glossary> GetGlossary()
        {
            return Glossaries;
        }

        public List<History> GetHistory()
        {
            return Histories;
        }

        public List<Location> GetLocations()
        {
            return Locations;
        }

        public List<User> GetUsers()
        {
            return Users;
        }

        public bool SetLockedLocation(Location location)
        {
            var obj = Locations.FirstOrDefault(x => x.LocationId == location.LocationId);
            if (obj != null) obj.Locked = location.Locked;
            return location.Locked;
        }

        public void AddFavourite(Favourite favourite)
        {
            Favourites.Add(favourite);
        }

        public void RemoveFavourite(Favourite favourite)
        {
            Favourites.Remove(favourite);
        }

        public void DeleteAccount(string username)
        {
            User user = Users.FirstOrDefault(x => x.Username == username);
            Users.Remove(user);
        }
        public void SetLangPref(string lang, string username)
        {
            User user = Users.FirstOrDefault(x => x.Username == username);
            user.langPref = lang;
        }

        public string GetTempUsername(Controller cont)
        {
            return Username;
        }

        public string GetTempRole(Controller cont)
        {
            return Role;
        }

        public void SetTempUsername(Controller cont, string username)
        {
            Username = username;
        }

        public void SetTempRole(Controller cont, string role)
        {
            Role = role;
        }
    }
}
