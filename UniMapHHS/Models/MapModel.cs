using System.Collections.Generic;

namespace UniMapHHS.Models
{
    public class MapModel
    {
        public string MapId { get; set; }
        public string MapLink { get; set; }
        public List<AreaModel> AreaList { get; set; }

        public string BuildingName { get; set; }

        public MapModel(string MapId, string MapLink, List<AreaModel> AreaList, string BuildingName)
        {
            this.MapId = MapId;
            this.MapLink = MapLink;
            this.AreaList = AreaList;
            this.BuildingName = BuildingName;
        }

        public class AreaModel
        {
            public int LocationID { get; set; }
            public int CategoryID { get; set; }
            public string AreaTitle { get; set; }
            public string AreaAlt { get; set; }

            public string DestinationId { get; set; }
            public List<int> AreaCoords {get; set;}


            public AreaModel(int LocationID, int CategoryID, string AreaTitle, string AreaAlt, string DestinationId, List<int> AreaCoords)
            {
                this.LocationID = LocationID;
                this.CategoryID = CategoryID;
                this.AreaTitle = AreaTitle;
                this.AreaAlt = AreaAlt;
                this.DestinationId = DestinationId;
                this.AreaCoords = AreaCoords;
            }
        }
    }

}

   
