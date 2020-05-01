using System.Collections.Generic;
using System.Linq;
using UniMapHHS.Controllers;
using UniMapHHS.Mocking;
using UniMapHHS.Models;
using Xunit;

namespace UniMapHHS.Tests
{
    public class HomeTests
    {
        [Fact]
        public void Test_Index()
        {
            //Arrange
            MockHandler mock = new MockHandler();
            User user = new User() { Username = "17032229@student.hhs.nl", langPref = "NL" };
            Glossary glos = new Glossary() { GlossaryId = 1, Dutch = "Hallo", English = "Hello", Spanish = "Ola" };
            mock.Glossaries = new List<Glossary>() { glos };
            mock.Users = new List<User>() { user };
            mock.SetTempUsername(null, "17032229@student.hhs.nl");
            mock.SetTempRole(null, "Student");
            HomeController cont = new HomeController(null, mock);

            //Act
            cont.Index("SP");
            string role = mock.GetTempRole(null);
            var result = mock.GetUsers().FirstOrDefault();

            //Assert
            Assert.Equal("Student", role);
            Assert.Equal("SP", result.langPref);
        }

        [Fact]
        public void Test_GetGlossary()
        {
            //Arrange
            MockHandler mock = new MockHandler();
            Glossary glossary = new Glossary() { GlossaryId = 1, Dutch = "Hallo", English = "Hello", Spanish = "Ola" };
            mock.Glossaries = new List<Glossary>() { glossary };
            HomeController cont = new HomeController(null, mock);

            //Act
            var result1 = cont.GetGlossary("NL");
            var result2 = cont.GetGlossary("EN");
            var result3 = cont.GetGlossary("SP");

            //Assert
            Assert.Equal("Hallo", result1[1]);
            Assert.Equal("Hello", result2[1]);
            Assert.Equal("Ola", result3[1]);
        }

        [Fact]
        public void Test_GetUserRole()
        {
            //Arrange
            MockHandler mock = new MockHandler();
            mock.SetTempRole(null, "Student");
            HomeController cont = new HomeController(null, mock);

            //Act
            var result1 = mock.GetTempRole(null);

            //Assert
            Assert.Equal("Student", result1);
        }

        [Fact]
        public void Test_FetchLocked()
        {
            //Arrange
            MockHandler mock = new MockHandler();
            Location location1 = new Location() { LocationId = 1, Locked = true };
            Location location2 = new Location() { LocationId = 2, Locked = false };
            mock.Locations = new List<Location>() { location1, location2 };
            HomeController cont = new HomeController(null, mock);

            //Act
            var result1 = cont.FetchLocked(1);
            var result2 = cont.FetchLocked(2);

            //Assert
            Assert.False(result1);
            Assert.True(result2);
        }

        [Fact]
        public void Test_GetMaps()
        {
            //Arrange
            MockHandler mock = new MockHandler();
            HomeController cont = new HomeController(null, mock);

            //Act
            var maps = cont.GetMaps();
            int result = maps.Count;

            //Assert
            Assert.Equal(32, result);
        }

        [Fact]
        public void Test_GetFloors()
        {
            //Arrange
            MockHandler mock = new MockHandler();
            BuildingFloor b1 = new BuildingFloor() { BuildingFloorId = 1, Building = "Strip", Floor = 4 };
            BuildingFloor b2 = new BuildingFloor() { BuildingFloorId = 2, Building = "Strip", Floor = 2 };
            BuildingFloor b3 = new BuildingFloor() { BuildingFloorId = 2, Building = "Slinger", Floor = 1902 };
            mock.BuildingFloors = new List<BuildingFloor>() { b1, b2, b3 };
            HomeController cont = new HomeController(null, mock);

            //Act
            int result = cont.GetFloors("Strip").Count;

            //Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void Test_AddFavourite()
        {
            //Arrange
            MockHandler mock = new MockHandler();
            mock.SetTempUsername(null, "Jan");
            Location l1 = new Location() { LocationId = 150 };
            Location l2 = new Location() { LocationId = 1 };
            Favourite f1 = new Favourite() { LocationId = l1.LocationId, Username = "Jan" };
            Favourite f2 = new Favourite() { LocationId = l2.LocationId, Username = "Arthur" };
            mock.Locations = new List<Location>() { l1, l2 };
            mock.Favourites = new List<Favourite>() { f1, f2 };
            HomeController cont = new HomeController(null, mock);

            //Act
            bool falseResult = cont.AddFavourite(150);
            bool trueResult = cont.AddFavourite(1);

            //Assert
            Assert.True(trueResult);
            Assert.False(falseResult);
        }

        [Fact]
        public void Test_DeleteAccount()
        {
            //Arrange
            MockHandler mock = new MockHandler();
            mock.SetTempUsername(null, "Jan");
            User user = new User() { Username = "Jan" };
            mock.Users = new List<User>() { user };
            HomeController cont = new HomeController(null, mock);

            //Act
            bool falseResult = cont.DeleteAccount();

            //Assert
            Assert.False(falseResult);
        }
    }
}
