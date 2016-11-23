using System.Collections.Generic;
using MarsRover;
using MarsRover.Enums;
using MarsRover.Planets;
using MarsRover.Rovers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System;

namespace MarsRoverTests
{
    /// <summary>
    /// Test class to run the core functionality of the <see cref="Simulation"/> class
    /// </summary>
    [TestClass()]
    public class SimulationTests
    {
        /// <summary>
        /// Tests that constructing the Simulation will cause the Planet to build it's grid
        /// </summary>
        [TestMethod]
        public void Simulation_WithValidValues_BuildsPlanet()
        {
            // Arrange
            var mockPlanet = new Mock<IPlanet>(1, 1);
            mockPlanet.Setup(s => s.BuildPlanet(It.IsAny<IEnumerable<IRover>>()));

            var rovers = new List<IRover>
            {
                new Rover(new Point(0, 0), Direction.North)
            };

            // Act
            var simulation = new Simulation(mockPlanet.Object, rovers);

            // Assert
            mockPlanet.Verify(s => s.BuildPlanet(It.IsAny<IEnumerable<IRover>>()), Times.Once);
        }

        /// <summary>
        /// Tests that retrieving the final position of a single rover returns a valid string
        /// </summary>
        [TestMethod]
        public void GetFinishingPositions_WithSingleRover_ReturnsValidStrings()
        {
            // Arrange
            var mockPlanet = new Mock<IPlanet>(1, 1);
            mockPlanet.Setup(s => s.BuildPlanet(It.IsAny<IEnumerable<IRover>>()));

            var rovers = new List<IRover>
            {
                new Rover(new Point(0, 0), Direction.North)
            };

            var simulation = new Simulation(mockPlanet.Object, rovers);

            var expectedResults = new List<string>
            {
                "0 0 N"
            };

            // Act
            var results = simulation.GetFinishingPositions().ToList();

            // Assert
            CollectionAssert.AreEquivalent(expectedResults, results);
        }

        /// <summary>
        /// Tests that retrieving the final positions of multiple rovers returns a valid list of strings
        /// </summary>
        [TestMethod]
        public void GetFinishingPositions_WithMultipleRovers_ReturnsValidStrings()
        {
            // Arrange
            var mockPlanet = new Mock<IPlanet>(2, 2);
            mockPlanet.Setup(s => s.BuildPlanet(It.IsAny<IEnumerable<IRover>>()));

            var rovers = new List<IRover>
            {
                new Rover(new Point(0, 0), Direction.North),
                new Rover(new Point(1, 1), Direction.East)
            };

            var simulation = new Simulation(mockPlanet.Object, rovers);

            var expectedResults = new List<string>
            {
                "0 0 N",
                "1 1 E"
            };

            // Act
            var results = simulation.GetFinishingPositions().ToList();

            // Assert
            CollectionAssert.AreEquivalent(expectedResults, results);
        }

        /// <summary>
        /// Tests that retrieving the final positions of no rovers will return an empty collection
        /// </summary>
        [TestMethod]
        public void GetFinishingPositions_WithNoRovers_ReturnsEmptyCollection()
        {
            // Arrange
            var mockPlanet = new Mock<IPlanet>(2, 2);
            mockPlanet.Setup(s => s.BuildPlanet(It.IsAny<IEnumerable<IRover>>()));

            var simulation = new Simulation(mockPlanet.Object, new List<IRover>());

            // Act
            var results = simulation.GetFinishingPositions().ToList();

            // Assert
            Assert.IsTrue(results.Count == 0);
        }

        /// <summary>
        /// Tests that running a Rover which has no next move will do nothing
        /// </summary>
        [TestMethod]
        public void RunRover_WithNoNextMove_DoesNothing()
        {
            // Arrange
            var mockPlanet = new Mock<IPlanet>(2, 2);
            mockPlanet.Setup(s => s.BuildPlanet(It.IsAny<IEnumerable<IRover>>()));

            var mockRover = new Mock<IRover>(new Point(0, 0), Direction.North);
            mockRover.Setup(s => s.GetNextMove()).Returns(MarsRover.Enums.Action.Nothing);

            var rovers = new List<IRover>
            {
                mockRover.Object
            };

            var simulation = new Simulation(mockPlanet.Object, rovers);

            // Act
            simulation.RunRover(mockRover.Object);

            // Assert
            mockRover.Verify(s => s.Move(), Times.Never);
            mockPlanet.Verify(s => s.UpdateGridPosition(It.IsAny<IRover>(), It.IsAny<Point>()), Times.Never);
        }

        /// <summary>
        /// Tests that running a rover with a movement that does not alter it's coordinates will cause no change in the Planet
        /// </summary>
        [TestMethod]
        public void RunRover_WithEqualCoordinates_DoesNotUpdateGrid()
        {
            // Arrange
            var mockPlanet = new Mock<IPlanet>(2, 2);
            mockPlanet.Setup(s => s.BuildPlanet(It.IsAny<IEnumerable<IRover>>()));

            var mockRover = new Mock<IRover>(new Point(0, 0), Direction.North);
            mockRover.SetupSequence(s => s.GetNextMove())
                .Returns(MarsRover.Enums.Action.MoveForward)
                .Returns(MarsRover.Enums.Action.Nothing);

            // Causes the move to not affect the coordinates of the Rover
            mockRover.Setup(s => s.Move());

            var rovers = new List<IRover>
            {
                mockRover.Object
            };

            var simulation = new Simulation(mockPlanet.Object, rovers);

            // Act
            simulation.RunRover(mockRover.Object);

            // Assert
            mockRover.Verify(s => s.Move(), Times.Once);
            mockPlanet.Verify(s => s.UpdateGridPosition(It.IsAny<IRover>(), It.IsAny<Point>()), Times.Never);
        }

        /// <summary>
        /// Tests that running a rover into an area that is already occupied will throw an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RunRover_AreaAlreadyOccupied_ThrowsException()
        {
            // Arrange
            var mockPlanet = new Mock<IPlanet>(2, 2);
            mockPlanet.Setup(s => s.BuildPlanet(It.IsAny<IEnumerable<IRover>>()));
            mockPlanet.Setup(s => s.IsAreaOccupied(It.IsAny<Point>())).Returns(true);

            var mockRover = new Mock<IRover>(new Point(0, 0), Direction.North);
            mockRover.SetupSequence(s => s.GetNextMove())
                .Returns(MarsRover.Enums.Action.MoveForward)
                .Returns(MarsRover.Enums.Action.Nothing);

            // Causes the move to not affect the coordinates of the Rover
            mockRover.Setup(s => s.Move()).Callback(() => mockRover.Object.Coordinates = new Point(0, 1));

            var rovers = new List<IRover>
            {
                mockRover.Object
            };

            var simulation = new Simulation(mockPlanet.Object, rovers);

            // Act
            simulation.RunRover(mockRover.Object);

            // Assert
            mockRover.Verify(s => s.Move(), Times.Once);
            mockPlanet.Verify(s => s.IsAreaOccupied(It.IsAny<Point>()), Times.Once);
            mockPlanet.Verify(s => s.UpdateGridPosition(It.IsAny<IRover>(), It.IsAny<Point>()), Times.Never);
            Assert.Fail();
        }

        /// <summary>
        /// Tests that running a Rover with a valid movement operation updates the Planet
        /// </summary>
        [TestMethod]
        public void RunRover_WithValidOperation_UpdatesPlanetGrid()
        {
            // Arrange
            var mockPlanet = new Mock<IPlanet>(2, 2);
            mockPlanet.Setup(s => s.BuildPlanet(It.IsAny<IEnumerable<IRover>>()));
            mockPlanet.Setup(s => s.IsAreaOccupied(It.IsAny<Point>())).Returns(false);

            var mockRover = new Mock<IRover>(new Point(0, 0), Direction.North);
            mockRover.SetupSequence(s => s.GetNextMove())
                .Returns(MarsRover.Enums.Action.MoveForward)
                .Returns(MarsRover.Enums.Action.Nothing);

            // Causes the move to not affect the coordinates of the Rover
            mockRover.Setup(s => s.Move()).Callback(() => mockRover.Object.Coordinates = new Point(0, 1));

            var rovers = new List<IRover>
            {
                mockRover.Object
            };

            var simulation = new Simulation(mockPlanet.Object, rovers);

            // Act
            simulation.RunRover(mockRover.Object);

            // Assert
            mockRover.Verify(s => s.Move(), Times.Once);
            mockPlanet.Verify(s => s.IsAreaOccupied(It.IsAny<Point>()), Times.Once);
            mockPlanet.Verify(s => s.UpdateGridPosition(It.IsAny<IRover>(), It.IsAny<Point>()), Times.Once);
        }
    }
}