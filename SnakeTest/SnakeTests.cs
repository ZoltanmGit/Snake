using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snake.Model;
using Snake.Persistence;
using System.Threading.Tasks;
using Moq;

namespace SnakeTest
{
    [TestClass]
    public class SnakeTests
    {
        private SnakeGameModel _model;
        private SnakeTable _mockTable;
        private Mock<ISnakeDataAccess> _mock;


        [TestInitialize]
        public void Initialize()
        {
            _mockTable = new SnakeTable(10);
            for (int i = 0; i < _mockTable.GetMapSize(); i++)
            {
                for (int j = 0; j < _mockTable.GetMapSize(); j++)
                {
                    _mockTable.SetMapValue(i, j, 0);
                }
            }
            _mock = new Mock<ISnakeDataAccess>();
            _model = new SnakeGameModel(_mock.Object);
            _model.SetSnakeTable(_mockTable);
            _model.SpawnSnake(5, 4);

            _mock.Setup(mock => mock.LoadAsync(It.IsAny<String>()))
                .Returns(() => Task.FromResult(_mockTable));

            _model.GameOver += new EventHandler<SnakeEventArgs>(Model_GameOver);

        }
        [TestMethod]
        public void TestSnakeInitial()
        {
            //Test if the snake's head and body is spawned in the right location
            Assert.AreEqual(1, _model.GetSnakeTable().GetMapValue(5, 4));
            for (int i = 5; i < 5+4; i++)
            {
                Assert.AreEqual(2, _model.GetSnakeTable().GetMapValue(i + 1, 4));
            }
            //Snake starts with 0 eaten eggs
            Assert.AreEqual(0, _model.GetEggCount());
            //Snake starts in UP direction
            Assert.AreEqual(Direction.Up, _model.GetSnake().GetDirection());
            //Snake starts with 5 bodyparts total
            int testSnakeBodyPartCount = 1; //because of head
            testSnakeBodyPartCount += _model.GetSnake().GetSnakeBody().Count;
            Assert.AreEqual(5, testSnakeBodyPartCount);
        }
        [TestMethod]
        public void TestSnakeMovement()
        {
            _model.AdvanceGame();
            //Head is in a good position after 1 movement
            Assert.AreEqual(1, _model.GetSnakeTable().GetMapValue(4, 4));
            //Body is in a good position after 1 movement
            for (int i = 4; i < 4 + 4; i++)
            {
                Assert.AreEqual(2, _model.GetSnakeTable().GetMapValue(i + 1, 4));
            }
            //The cell where the snake moved away from is now empty
            Assert.AreEqual(0,_model.GetSnakeTable().GetMapValue(9,4));
            //turn right from up
            _model.ChangeDirection(1);
            Assert.AreEqual(Direction.Right, _model.GetSnake().GetDirection());
            //Try changing again without game advancement
            _model.ChangeDirection(1);
            //Should be the same since it's not allowed
            Assert.AreEqual(Direction.Right, _model.GetSnake().GetDirection());
            _model.AdvanceGame();
            Assert.AreEqual(1,_model.GetSnakeTable().GetMapValue(4,5));
            for (int i = 4; i < 8; i++)
            {
                Assert.AreEqual(2, _model.GetSnakeTable().GetMapValue(i, 4));
            }
        }
        [TestMethod]
        public void TestEggConsumption()
        {
            Assert.AreEqual(1, _model.GetSnakeTable().GetMapValue(5, 4));
            //Spawn an egg 2 cells away from the snake
            _model.GetSnakeTable().SetMapValue(3,4,3);
            //Check if the egg is there
            Assert.AreEqual(3, _model.GetSnakeTable().GetMapValue(3, 4));
            //Advance the game
            _model.AdvanceGame();
            //The snake has not yet reached the egg, so eggcount is 0
            Assert.AreEqual(3, _model.GetSnakeTable().GetMapValue(3, 4));
            Assert.AreEqual(0, _model.GetEggCount());
            _model.AdvanceGame();
            //The snake has overriden the egg cell and is now longer
            Assert.AreEqual(1, _model.GetSnakeTable().GetMapValue(3, 4));
            
        }

        [TestMethod]
        public void TestWallHit()
        {
            Assert.AreEqual(1, _model.GetSnakeTable().GetMapValue(5, 4));
            _model.GetSnakeTable().SetMapValue(3, 4, 4);
            _model.AdvanceGame();
            _model.AdvanceGame();
        }

        private void Model_GameOver(Object sender, SnakeEventArgs e)
        {
            Assert.IsTrue(_model.bIsGameOver);
            Assert.AreEqual(_model.GetEggCount(), e.EggCount);
        }
    }
}
