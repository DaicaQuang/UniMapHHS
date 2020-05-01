using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using UniMapHHS.Models;

namespace UniMapHHS.Mocking
{
    public class DBHandler : IMock
    {
        private DbAppContext _context;

        public void SetContext(DbAppContext context)
        {
            this._context = context;
        }

        public Category[] GetCategories()
        {
            return _context.Categories.ToArray();
        }

        public List<Favourite> GetFavourites()
        {
            return _context.Favourites.ToList();
        }

        public List<Glossary> GetGlossary()
        {
            return _context.Glossaries.ToList();
        }

        public List<History> GetHistory()
        {
            return _context.Histories.ToList();
        }

        public List<Location> GetLocations()
        {
            
            return _context.Locations.ToList();
        }

        public List<BuildingFloor> GetBuildingFloors()
        {
            return _context.BuildingFloors.ToList();
        }
        public bool SetLockedLocation(Location location)
        {
            _context.Entry(location).State = EntityState.Modified;
            _context.SaveChanges();
            return location.Locked;
        }

        public void AddFavourite(Favourite favourite)
        {
            _context.Favourites.Add(favourite);
            _context.SaveChanges();
        }

        public void RemoveFavourite(Favourite favourite)
        {
            _context.Favourites.Remove(favourite);
            _context.SaveChanges();
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public void DeleteAccount(string username)
        {
            User user = _context.Users.Find(username);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public void SetLangPref(string lang, string username)
        {
            var result = _context.Users.SingleOrDefault(u => u.Username == username);
            if (result != null)
            {
                result.langPref = lang;
                _context.SaveChanges();
            }
        }

        public string GetTempUsername(Controller cont)
        {
            var user = cont.TempData["Username"];
            cont.TempData.Keep("Username");
            if (user != null)
            {
                return user.ToString();
            }
            return "Guest";
        }

        public string GetTempRole(Controller cont)
        {
            var role = cont.TempData["Role"];
            cont.TempData.Keep("Role");
            if (role != null)
            {
                return role.ToString();
            }
            return "Guest";
        }

        public void SetTempUsername(Controller cont, string username)
        {
            cont.TempData["Username"] = username;
        }

        public void SetTempRole(Controller cont, string role)
        {
            cont.TempData["Role"] = role;
        }
    }
}
