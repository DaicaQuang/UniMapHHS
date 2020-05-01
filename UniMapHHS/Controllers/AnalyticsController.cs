using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using UniMapHHS.Models;
using UniMapHHS.Mocking;

namespace UniMapHHS.Controllers
{
    public class AnalyticsController : Controller
    {
        private readonly IMock DBHandler;

        public AnalyticsController(DbAppContext context, IMock mock)
        {
            DBHandler = mock;
            mock.SetContext(context);
        }


        public IActionResult Index(int id, string lang)
        {
            
            string role = DBHandler.GetTempRole(this);

            if (role != "Teacher")
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.glossary = GetGlossary(lang);
            ViewBag.lang = lang != null ? lang : "EN";

            Location location = null;
            if (id != 0)
            {
                location = DBHandler.GetLocations().FirstOrDefault(x => x.LocationId.Equals(id));
                location.Category = DBHandler.GetCategories().FirstOrDefault(x => x.CategoryId.Equals(location.CategoryId));
                location.BuildingFloor = DBHandler.GetBuildingFloors().FirstOrDefault(x => x.BuildingFloorId.Equals(location.BuildingFloorId));
            }

            return View(location);
        }
        

        public Dictionary<int, string> GetGlossary(string lang)
        {
            var glossaryTable = DBHandler.GetGlossary();

            if (lang == "NL")
            {
                return glossaryTable.ToDictionary(id => id.GlossaryId, lang => lang.Dutch);
            }
            else if (lang == "SP")
            {
                return glossaryTable.ToDictionary(id => id.GlossaryId, lang => lang.Spanish);
            }
            else
            {
                return glossaryTable.ToDictionary(id => id.GlossaryId, lang => lang.English);
            }
        }

        // Returns the hourly average data of a key location for each day of the current week.
        [HttpPost]
        public List<int> GetExptectedActivity(int locationId)
        {
            List<History> historyList = GetHistoryByLocation(locationId);

            List<int> AM8 = new List<int>();
            List<int> AM9 = new List<int>();
            List<int> AM10 = new List<int>();
            List<int> AM11 = new List<int>();
            List<int> PM12 = new List<int>();
            List<int> PM1 = new List<int>();
            List<int> PM2 = new List<int>();
            List<int> PM3 = new List<int>();
            List<int> PM4 = new List<int>();
            List<int> PM5 = new List<int>();
            List<int> PM6 = new List<int>();
            List<int> PM7 = new List<int>();
            List<int> PM8 = new List<int>();

            foreach (History h in historyList)
            {
                int hour = h.TimeStamp.Hour;
                switch (hour)
                {
                    case 8:
                        AM8.Add(h.Quantity);
                        break;
                    case 9:
                        AM9.Add(h.Quantity);
                        break;
                    case 10:
                        AM10.Add(h.Quantity);
                        break;
                    case 11:
                        AM11.Add(h.Quantity);
                        break;
                    case 12:
                        PM12.Add(h.Quantity);
                        break;
                    case 13:
                        PM1.Add(h.Quantity);
                        break;
                    case 14:
                        PM2.Add(h.Quantity);
                        break;
                    case 15:
                        PM3.Add(h.Quantity);
                        break;
                    case 16:
                        PM4.Add(h.Quantity);
                        break;
                    case 17:
                        PM5.Add(h.Quantity);
                        break;
                    case 18:
                        PM6.Add(h.Quantity);
                        break;
                    case 19:
                        PM7.Add(h.Quantity);
                        break;
                    case 20:
                        PM8.Add(h.Quantity);
                        break;

                }
            }
            return new List<int>()
            { GetAverage(AM8), GetAverage(AM9), GetAverage(AM10), GetAverage(AM11), GetAverage(PM12),GetAverage(PM1), GetAverage(PM2),
                GetAverage(PM3), GetAverage(PM4), GetAverage(PM5), GetAverage(PM6), GetAverage(PM7), GetAverage(PM8) };
        }

        [HttpPost]
        public JsonResult GetDataFromCurrentMonth(int locationId)
        {
            return Json(GetMonthlyData(locationId, 0));
        }

        [HttpPost]
        public JsonResult GetDataFromPreviousMonth(int locationId)
        {
            return Json(GetMonthlyData(locationId, 1));
        }

        // Returns the average data of each day of the week from a month.
        public List<int> GetMonthlyData(int locationId, int month)
        {
            List<History> historyList = GetHistoryByLocation(locationId).Where(x => x.TimeStamp.Month.Equals(DateTime.Now.Month - month)).ToList();

            List<int> monday = new List<int>();
            List<int> tuesday = new List<int>();
            List<int> wednesday = new List<int>();
            List<int> thursday = new List<int>();
            List<int> friday = new List<int>();

            foreach (History h in historyList)
            {
                string day = h.TimeStamp.DayOfWeek.ToString().ToLower();
                switch (day)
                {
                    case "monday":
                        monday.Add(h.Quantity);
                        break;
                    case "tuesday":
                        tuesday.Add(h.Quantity);
                        break;
                    case "wednesday":
                        wednesday.Add(h.Quantity);
                        break;
                    case "thursday":
                        thursday.Add(h.Quantity);
                        break;
                    case "friday":
                        friday.Add(h.Quantity);
                        break;
                }
            }

            return new List<int>()
            { GetAverage(monday), GetAverage(tuesday), GetAverage(wednesday), GetAverage(thursday), GetAverage(friday) };
        }

        private List<History> GetHistoryByLocation(int locationId)
        {
            return DBHandler.GetHistory().Where(x => x.LocationId.Equals(locationId)).ToList();
        }

        private int GetAverage(List<int> list)
        {
            if (list.Count == 0)
            {
                return 0;
            }
            double average = Math.Ceiling(list.Average());
            return Convert.ToInt32(average);
        }

        // Returns the amount of favourites from a key location.
        [HttpPost]
        public int GetAmountOfFavourites(int locationId)
        {
            int favourites = DBHandler.GetFavourites().Count(x => x.LocationId.Equals(locationId));
            return favourites != 0 ? favourites : 0;
        }

        // Returns the current amount of people in a key location.
        [HttpPost]
        public int GetCurrectAmountOfPeople(int locationId)
        {
            List<History> historyList = DBHandler.GetHistory().Where(x => x.LocationId.Equals(locationId)).ToList();
            if (historyList.Count == 0)
            {
                return 0;
            }

            History h = historyList.LastOrDefault();
            return h.Quantity;
        }

        // Returns the maximum amount of people in a key location.
        [HttpPost]
        public int GetMaxAmountOfPeople(int locationId)
        {
            return DBHandler.GetLocations().FirstOrDefault(x => x.LocationId.Equals(locationId)).MaxCapacity;
        }
    }
}