using UniMapHHS.Models;
using UniMapHHS.Mocking;
using UniMapHHS.Controllers;
using Xunit;
using System.Collections.Generic;
using System;

namespace UniMapHHS.Tests
{
    public class AnalyticsTests
    {
        [Fact]
        public void Test_GetAmountOfFavourites()
        {
            //Arrange
            MockHandler mock = new MockHandler();
            Favourite fav1 = new Favourite() { LocationId = 1, Username = "Jan" };
            Favourite fav2 = new Favourite() { LocationId = 1, Username = "Arthur" };
            Favourite fav3 = new Favourite() { LocationId = 1, Username = "Justin" };
            mock.Favourites = new List<Favourite> { fav1, fav2, fav3 };
            AnalyticsController cont = new AnalyticsController(null, mock);

            //Act
            var result = cont.GetAmountOfFavourites(1);

            //Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void Test_GetCurrectAmountOfPeople()
        {
            //Arrange
            MockHandler mock = new MockHandler();
            History his1 = new History() { LocationId = 1, Quantity = 10 };
            History his2 = new History() { LocationId = 1, Quantity = 15 };
            mock.Histories = new List<History>() { his1, his2 };
            AnalyticsController cont = new AnalyticsController(null, mock);

            //Act
            var result = cont.GetCurrectAmountOfPeople(1);
            var resultEmpty = cont.GetCurrectAmountOfPeople(2);

            //Assert
            Assert.Equal(15, result);
            Assert.Equal(0, resultEmpty);
        }

        [Fact]
        public void Test_GetMaxAmountOfPeople()
        {
            //Arrange
            MockHandler mock = new MockHandler();
            Location loc1 = new Location() { LocationId = 1, MaxCapacity = 75 };
            Location loc2 = new Location() { LocationId = 2, MaxCapacity = 310 };
            mock.Locations = new List<Location>() { loc1, loc2 };
            AnalyticsController cont = new AnalyticsController(null, mock);

            //Act
            var result1 = cont.GetMaxAmountOfPeople(1);
            var result2 = cont.GetMaxAmountOfPeople(2);

            //Assert
            Assert.Equal(75, result1);
            Assert.Equal(310, result2);
        }

        [Fact]
        public void Test_GetGlossary()
        {
            //Arrange
            MockHandler mock = new MockHandler();
            Glossary glossary = new Glossary() { GlossaryId = 1, Dutch = "Hallo", English = "Hello", Spanish = "Ola" };
            mock.Glossaries = new List<Glossary>() { glossary };
            AnalyticsController cont = new AnalyticsController(null, mock);

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
        public void Test_GetExptectedActivity()
        {
            //Arrange
            MockHandler mock = new MockHandler();
            mock.Histories = new List<History>();
            for (int i = 8; i < 21; i++)
            {
                DateTime dt = new DateTime(2014, 6, 1, i, 32, 30);
                History h1 = new History() { HistoryId = 1, LocationId = 5, Quantity = 10, TimeStamp = dt };
                History h2 = new History() { HistoryId = 2, LocationId = 5, Quantity = 20, TimeStamp = dt };
                mock.Histories.Add(h1);
                mock.Histories.Add(h2);
            }

            AnalyticsController cont = new AnalyticsController(null, mock);

            //Act
            List<int> result = cont.GetExptectedActivity(5);

            //Assert
            foreach (int i in result)
            {
                Assert.Equal(15, i);
            }
        }

        [Fact]
        public void Test_GetMonthlyData()
        {
            //Arrange
            MockHandler mock = new MockHandler();
            mock.Histories = new List<History>();

            for (int i = 6; i < 11; i++)
            {
                DateTime dt = new DateTime(2020, 4, i, 10, 32, 30);
                History h1 = new History() { HistoryId = 1, LocationId = 5, Quantity = 10, TimeStamp = dt };
                History h2 = new History() { HistoryId = 2, LocationId = 5, Quantity = 20, TimeStamp = dt };
                mock.Histories.Add(h1);
                mock.Histories.Add(h2);
            }

            AnalyticsController cont = new AnalyticsController(null, mock);

            //Act
            List<int> result = cont.GetMonthlyData(5, 0);

            //Assert
            foreach (int i in result)
            {
                Assert.Equal(15, i);
            }
        }
    }
}
