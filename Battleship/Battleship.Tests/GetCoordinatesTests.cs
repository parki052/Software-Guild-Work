using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.UI;
using NUnit.Framework;

namespace Battleship.Tests
{
    [TestFixture]
    public class GetCoordinatesTests
    {

        [TestCase("a1", 1, 1)]
        [TestCase("a01", 1, 1)]
        [TestCase("j10", 10, 10)]
        [TestCase("A4", 1, 4)]
        [TestCase("E8", 5, 8)]
        public void CanGetCoordinateFromString(string input, int xCoord, int yCoord)
        {
            Board board = new Board();

            Coordinate actual = CoordinateWorkflow.CreateCoordinate(input);

            Coordinate expected = new Coordinate(xCoord, yCoord);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("a1")]
        [TestCase("a10")]
        [TestCase("a01")]
        [TestCase("j10")]
        [TestCase("A4")]
        [TestCase("E8")]
        public void CanValidateCoordinateString(string input)
        {
            bool actual = CoordinateWorkflow.ValidateCoordinate(input);
            Assert.AreEqual(true, actual);
        }

        [TestCase("101")]
        [TestCase("")]
        [TestCase("j11")]
        [TestCase("A0")]
        [TestCase("A00")]
        [TestCase("K12")]
        [TestCase("ham")]
        public void CanCatchInvalidCoordinateString(string input)
        {
            bool actual = CoordinateWorkflow.ValidateCoordinate(input);
            Assert.AreEqual(false, actual);
        }

        [TestCase("A", 1)]
        [TestCase("a", 1)]
        [TestCase("B", 2)]
        [TestCase("b", 2)]
        [TestCase("I", 9)]
        [TestCase("i", 9)]
        [TestCase("J", 10)]
        [TestCase("j", 10)]
        public void CanConvertCharToInt(string letter, int expected)
        {
            Assert.AreEqual(expected, CoordinateWorkflow.ConvertCharToInt(letter));
        }
    }
}
