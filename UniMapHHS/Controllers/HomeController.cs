using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UniMapHHS.Models;
using UniMapHHS.Mocking;
using static UniMapHHS.Models.MapModel;
using System;

namespace UniMapHHS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMock DBHandler;

        public HomeController(DbAppContext context, IMock mock)
        {
            DBHandler = mock;
            mock.SetContext(context);
        }

        public IActionResult Index(string lang)
        {
            SetLangPref(lang);
            //LINQ-queries & ViewBags
            ViewBag.UserRole = GetUserRole();
            ViewBag.CategoryList = DBHandler.GetCategories();

            ViewBag.glossary = GetGlossary(lang);
            ViewBag.lang = lang != null ? lang : "EN";
            ViewBag.searchBar = DBHandler.GetLocations();
            GetMaps();
            return View();
        }


        private void SetLangPref(string lang)
        {
            string username = DBHandler.GetTempUsername(this);

            if (username != "Guest" && lang != null && lang != "")
            {
                DBHandler.SetLangPref(lang, username);
            }
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

        public IActionResult Privacy()
        {
            return View();
        }

        public string GetUserRole()
        {
            return DBHandler.GetTempRole(this);
        }

        [HttpPost]
        public JsonResult FetchLocations(int id)
        {
            string username = DBHandler.GetTempUsername(this);
            var locations = DBHandler.GetLocations().Select(l => new
            {
                ID = l.LocationId,
                CI = DBHandler.GetCategories().FirstOrDefault(x => x.CategoryId.Equals(l.CategoryId)).Icon,
                Titel = l.Title,
                CategoryID = l.CategoryId,
                Lock = l.Locked,
                Favloc = DBHandler.GetFavourites().Exists(x => x.LocationId.Equals(l.LocationId) && x.Username.Equals(username))
            }).Where(l => l.CategoryID.Equals(id));
            return Json(locations);
        }

        [HttpPost]
        public bool FetchLocked(int id)
        {
            Location location = DBHandler.GetLocations().FirstOrDefault(x => x.LocationId.Equals(id));
            if (location.Locked)
            {
                location.Locked = false;
            }
            else
            {
                location.Locked = true;
            }
            DBHandler.SetLockedLocation(location);
            Location location2 = DBHandler.GetLocations().FirstOrDefault(x => x.LocationId.Equals(id));
            return location2.Locked;
        }

        public List<MapModel> GetMaps()
        {
            //De Haagse Hogeschool map
            var Slinger1 = new AreaModel(0, 0, "Slinger", "Slinger", "SL0", new List<int> { 469, 172, 488, 163, 490, 169, 499, 168, 495, 151, 511, 144, 530, 206, 550, 245, 574, 287, 587, 323, 597, 353, 605, 408, 609, 437, 614, 470, 621, 493, 624, 510, 636, 540, 649, 577, 668, 625, 648, 632, 618, 641, 562, 471, 559, 425, 539, 323, 498, 254 });
            var Ovaal0 = new AreaModel(0, 0, "Ovaal", "Ovaal", "OV0", new List<int> { 278, 377, 312, 334, 370, 307, 410, 306, 454, 318, 483, 338, 501, 368, 498, 396, 489, 426, 475, 447, 442, 476, 403, 491, 362, 492, 322, 486, 286, 458, 272, 426 });
            var Strip1_1 = new AreaModel(0, 0, "Strip", "Strip", "ST0", new List<int> { 70, 399, 115, 426, 288, 351, 376, 298, 441, 306, 486, 333, 520, 302, 475, 210 });
            var Strip1_2 = new AreaModel(0, 0, "Strip", "Strip", "ST0", new List<int> { 527, 181, 560, 261, 619, 192, 596, 152 });
            var Rugzak1_1 = new AreaModel(0, 0, "Rugzak 1", "Rugzak 1", "RZ0", new List<int> { 606, 363, 637, 354, 676, 476, 626, 488, 611, 436 });
            var Rugzak1_2 = new AreaModel(0, 0, "Rugzak 2", "Rugzak 2", "RZ4", new List<int> { 631, 505, 680, 490, 720, 609, 669, 627 });
            var Tuinhuis1 = new AreaModel(0, 0, "Tuinhuis", "Tuinhuis", "TH0", new List<int> { 36, 304, 78, 351, 274, 254, 230, 211 });

            var SchoolMap = new MapModel("School", "/images/HHS_Map.png", new List<AreaModel> { Slinger1, Ovaal0, Strip1_1, Strip1_2, Rugzak1_1, Rugzak1_2, Tuinhuis1 }, "School");

            //Slinger 0-2, 4-5, 7 
            var Slinger0Map = new MapModel("SL0", "/images/Slinger/Slinger.png", new List<AreaModel> { }, "Slinger");
            var Slinger1Map = new MapModel("SL1", "/images/Slinger/Slinger.png", new List<AreaModel> { }, "Slinger");
            var Slinger2Map = new MapModel("SL2", "/images/Slinger/Slinger.png", new List<AreaModel> { }, "Slinger");
            var Slinger4Map = new MapModel("SL4", "/images/Slinger/Slinger.png", new List<AreaModel> { }, "Slinger");
            var Slinger5Map = new MapModel("SL5", "/images/Slinger/Slinger.png", new List<AreaModel> { }, "Slinger");
            var Slinger7Map = new MapModel("SL7", "/images/Slinger/Slinger.png", new List<AreaModel> { }, "Slinger");

            //Slinger 3
            var SL3Kantine = new AreaModel(5, 2, "SL3 Kantine", "SL3 Kantine", "SL3Kantine", new List<int> { 717, 283, 717, 245, 800, 248, 780, 212, 796, 186, 826, 188, 841, 214, 824, 243, 895, 250, 895, 286, 813, 295 });
            var Slinger3Map = new MapModel("SL3", "/images/Slinger/Slinger_3.png", new List<AreaModel> { SL3Kantine }, "Slinger");

            //Slinger 6
            var SL654 = new AreaModel(1, 7, "SL6.54", "SL6.54", "SL654", new List<int> { 766, 293, 768, 249, 797, 248, 786, 234, 776, 200, 805, 188, 829, 195, 838, 216, 825, 243, 846, 248, 844, 295 });
            var SL6Ankerpunt = new AreaModel(3, 6, "Ankerpunt", "Ankerpunt", "SL6Ankerpunt", new List<int> { 424, 264, 428, 214, 448, 212, 425, 174, 435, 152, 455, 142, 481, 156, 484, 177, 467, 211, 517, 210, 519, 258 });
            var SL6ServiceDesk = new AreaModel(4, 8, "Service desk", "Service desk", "SL6ServiceDesk", new List<int> { 617, 266, 621, 219, 591, 214, 621, 172, 601, 148, 578, 142, 562, 162, 558, 181, 522, 209, 522, 255 });
            var Slinger6Map = new MapModel("SL6", "/images/Slinger/Slinger_6.png", new List<AreaModel> { SL654, SL6Ankerpunt, SL6ServiceDesk }, "Slinger");

            //Slinger 8
            var SL827 = new AreaModel(2, 7, "SL8.27", "SL8.27", "SL827", new List<int> { 472, 113, 476, 154, 570, 159, 565, 121, 529, 118, 550, 81, 530, 58, 507, 61, 488, 83, 506, 113 });
            var Slinger8Map = new MapModel("SL8", "/images/Slinger/Slinger_8.png", new List<AreaModel> { SL827 }, "Slinger");

            //Ovaal 0
            var Lighthouse = new AreaModel(7, 2, "Lighthouse", "Lighthouse", "Lighthouse", new List<int> { 647, 857, 728, 871, 837, 861, 941, 813, 859, 700, 820, 711, 843, 655, 812, 622, 761, 628, 743, 672, 768, 719, 694, 719 });
            var Ovaal0Map = new MapModel("OV0", "/images/Ovaal/Ovaal_0.png", new List<AreaModel> { Lighthouse }, "Ovaal");

            //Ovaal 1
            var Bibliotheek = new AreaModel(9, 5, "Bibliotheek", "Bibliotheek", "Library", new List<int> { 864, 312, 881, 208, 952, 223, 937, 183, 958, 146, 1002, 145, 1028, 171, 1026, 203, 1005, 239, 1087, 288, 1044, 379 });
            var FrontOffice = new AreaModel(6, 8, "FrontOffice", "FrontOffice", "Front office", new List<int> { 702, 338, 663, 243, 729, 221, 714, 178, 735, 144, 771, 140, 804, 167, 796, 207, 851, 212, 834, 304 });
            var Ovaal1Map = new MapModel("OV1", "/images/Ovaal/Ovaal_1.png", new List<AreaModel> { Bibliotheek, FrontOffice }, "Ovaal");

            //Ovaal 2-5
            var Ovaal2Map = new MapModel("OV2", "/images/Ovaal/Ovaal.png", new List<AreaModel> { }, "Ovaal");
            var Ovaal3Map = new MapModel("OV3", "/images/Ovaal/Ovaal.png", new List<AreaModel> { }, "Ovaal");
            var Ovaal4Map = new MapModel("OV4", "/images/Ovaal/Ovaal.png", new List<AreaModel> { }, "Ovaal");
            var Ovaal5Map = new MapModel("OV5", "/images/Ovaal/Ovaal.png", new List<AreaModel> { }, "Ovaal");

            ////Rugzak 1
            var Rugzak1_0Map = new MapModel("RZ0", "/images/Rugzak/Rugzak_1.png", new List<AreaModel> { }, "Rugzak 1");
            var Rugzak1_1Map = new MapModel("RZ1", "/images/Rugzak/Rugzak_1.png", new List<AreaModel> { }, "Rugzak 1");
            var Rugzak1_2Map = new MapModel("RZ2", "/images/Rugzak/Rugzak_1.png", new List<AreaModel> { }, "Rugzak 1");
            var Rugzak1_3Map = new MapModel("RZ3", "/images/Rugzak/Rugzak_1.png", new List<AreaModel> { }, "Rugzak 1");

            //Rugzak 2
            var RZ050 = new AreaModel(10, 6, "RZ0.50", "RZ0.50", "RZ050", new List<int> { 249, 154, 243, 265, 484, 263, 483, 157, 394, 154, 406, 116, 367, 90, 332, 107, 327, 134, 338, 156 });
            var Rugzak2_0Map = new MapModel("RZ4", "/images/Rugzak/Rugzak_2-0.png", new List<AreaModel> { RZ050 }, "Rugzak 2");
            var Rugzak2_1Map = new MapModel("RZ5", "/images/Rugzak/Rugzak_2.png", new List<AreaModel> { }, "Rugzak 2");
            var Rugzak2_2Map = new MapModel("RZ6", "/images/Rugzak/Rugzak_2.png", new List<AreaModel> { }, "Rugzak 2");
            var Rugzak2_3Map = new MapModel("RZ7", "/images/Rugzak/Rugzak_2.png", new List<AreaModel> { }, "Rugzak 2");

            //Strip 0
            var MKantine = new AreaModel(8, 2, "Main kantine", "Main kantine", "MKantine", new List<int> { 428, 337, 467, 429, 638, 593, 868, 398, 939, 319, 844, 136, 668, 217, 639, 194, 594, 203, 582, 251 });
            var Strip0Map = new MapModel("ST0", "/images/Strip/Strip_0.png", new List<AreaModel> { MKantine }, "Strip");
            var Strip1Map = new MapModel("ST1", "/images/Strip/Strip.png", new List<AreaModel> { }, "Strip");
            var Strip2Map = new MapModel("ST2", "/images/Strip/Strip.png", new List<AreaModel> { }, "Strip");
            var Strip3Map = new MapModel("ST3", "/images/Strip/Strip.png", new List<AreaModel> { }, "Strip");
            var Strip4Map = new MapModel("ST4", "/images/Strip/Strip.png", new List<AreaModel> { }, "Strip");
            var Strip5Map = new MapModel("ST5", "/images/Strip/Strip.png", new List<AreaModel> { }, "Strip");

            //Tuinhuis 0
            var TH001 = new AreaModel(11, 6, "TH0.01", "TH0.01", "TH001", new List<int> { 711, 64, 710, 256, 945, 259, 947, 65, 869, 62, 851, 29, 813, 26, 789, 40, 778, 69 });
            var Tuinhuis0Map = new MapModel("TH0", "/images/Tuinhuis/Tuinhuis_0.png", new List<AreaModel> { TH001 }, "Tuinhuis");
            var Tuinhuis1Map = new MapModel("TH1", "/images/Tuinhuis/Tuinhuis.png", new List<AreaModel> { }, "Tuinhuis");

            var ModelList = new List<MapModel>() {
                SchoolMap,
                Slinger0Map, Slinger1Map, Slinger2Map, Slinger3Map, Slinger4Map, Slinger5Map, Slinger6Map, Slinger7Map, Slinger8Map,
                Ovaal0Map, Ovaal1Map, Ovaal2Map, Ovaal3Map, Ovaal4Map, Ovaal5Map,
                Rugzak1_0Map, Rugzak1_1Map, Rugzak1_2Map, Rugzak1_3Map,
                Rugzak2_0Map, Rugzak2_1Map, Rugzak2_2Map, Rugzak2_3Map,
                Strip0Map, Strip1Map, Strip2Map, Strip3Map, Strip4Map, Strip5Map,
                Tuinhuis0Map, Tuinhuis1Map
            };
            ViewBag.ModelList = ModelList;

            return ModelList;
        }

        [HttpPost]
        public List<int> GetFloors(string building)
        {
            int Floors = DBHandler.GetBuildingFloors().Count(x => x.Building.Equals(building));
            List<int> FloorList = new List<int>();
            for (var i = 0; i < Floors; i++)
            {
                FloorList.Add(i);
            }
            return FloorList;
        }

        public bool AddFavourite(int id)
        {
            string username = DBHandler.GetTempUsername(this);

            bool check = DBHandler.GetFavourites().Exists(x => x.LocationId.Equals(id) && x.Username.Equals(username));
            if (check)
            {
                Favourite oldFavourite = DBHandler.GetFavourites().FirstOrDefault((x => x.LocationId.Equals(id) && x.Username.Equals(username)));
                DBHandler.RemoveFavourite(oldFavourite);
                return false;
            }
            else
            {
                Favourite newFavourite = new Favourite { LocationId = id, Username = username };
                DBHandler.AddFavourite(newFavourite);
                return true;
            }
        }

        public bool DeleteAccount()
        {
            string username = DBHandler.GetTempUsername(this).ToLower();
            DBHandler.DeleteAccount(username);
            bool exists = DBHandler.GetUsers().Exists(x => x.Username.Equals(username));
            if (!exists)
            {
                DBHandler.SetTempUsername(this, "Guest");
                DBHandler.SetTempRole(this, "Guest");
                return exists;
            }
            else
            {
                return exists;
            }
        }

        public JsonResult GetUserLocations()
        {
            string username = DBHandler.GetTempUsername(this);
            List<Favourite> favouriteList;
            favouriteList = DBHandler.GetFavourites().Where(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase)).ToList();

            List<Location> allLocations = DBHandler.GetLocations();
            List<Location> locationList = new List<Location>();

            foreach (var item in favouriteList)
            {
                locationList.Add(allLocations.FirstOrDefault(x => x.LocationId.Equals(item.LocationId)));
            }

            var locations = locationList.Select(l => new
            {
                ID = l.LocationId,
                CI = DBHandler.GetCategories().FirstOrDefault(x => x.CategoryId.Equals(l.CategoryId)).Icon,
                Titel = l.Title,
                CategoryID = l.CategoryId,
                Lock = l.Locked,
                Favloc = DBHandler.GetFavourites().Exists(x => x.LocationId.Equals(l.LocationId) && x.Username.Equals(username))
            }).ToList();

            return Json(locations);
        }

        public JsonResult GetAllLocations()
        {
            return Json(DBHandler.GetLocations());
        }
    }
}

