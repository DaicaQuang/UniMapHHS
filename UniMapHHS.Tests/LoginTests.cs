using System.Collections.Generic;
using UniMapHHS.Controllers;
using UniMapHHS.Mocking;
using UniMapHHS.Models;
using Xunit;

namespace UniMapHHS.Tests
{
    public class LoginTests
    {
        [Fact]
        public void Test_GetGlossary()
        {
            //Arrange
            MockHandler mock = new MockHandler();
            Glossary glossary = new Glossary() { GlossaryId = 1, Dutch = "Hallo", English = "Hello", Spanish = "Ola" };
            mock.Glossaries = new List<Glossary>() { glossary };
            LoginController cont = new LoginController(null, mock);

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
        public void Test_CheckUserexistence()
        {
            //Arrange
            MockHandler mock = new MockHandler();
            User user1 = new User() { Username = "Jan", Password = "12345", langPref = "SP" };
            mock.Users = new List<User>() { user1 };
            LoginController cont = new LoginController(null, mock);

            //Act
            var result1 = cont.CheckUserexistence(user1.Username, user1.Password);

            //Assert
            Assert.Equal("SP", result1);
        }
    }
}
