using MarsRover;
using MarsRover.Planets;
using MarsRover.Rovers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRoverTests
{
    /// <summary>
    /// Test class to test the overall functionality of the Simulation
    /// </summary>
    [TestClass]
    public class SimulationRunTests
    {

        [TestMethod]
        public void Run_WithNoRovers_ReturnsEmptyStringList()
        {
            // Arrange
            var mockPlanet = new Mock<IPlanet>(1, 1);
            mockPlanet.Setup(s => s.BuildPlanet(It.IsAny<IEnumerable<IRover>>()));

            var simulation = new Simulation(mockPlanet.Object, new List<IRover>());

            // Act
            var results = simulation.Run().ToList();

            // Assert
            Assert.IsTrue(results.Count == 0);
        }

        [TestMethod]
        public void Run_WithOneRoverAndOneMovement_ReturnsExpectedPosition()
        {
            // Arrange
            var planet = new Planet(2, 2);

            var actions = new Queue<MarsRover.Enums.Action>();
            actions.Enqueue(MarsRover.Enums.Action.MoveForward);

            var rovers = new List<IRover>
            {
                new Rover(new Point(0, 0), MarsRover.Enums.Direction.North, actions)
            };

            var simulation = new Simulation(planet, rovers);

            var expectedResults = new List<string>
            {
                "0 1 N"
            };

            // Act
            var results = simulation.Run().ToList();

            // Assert
            CollectionAssert.AreEquivalent(expectedResults, results);
        }

        [TestMethod]
        public void Run_WithTwoRoversWithBothOneMovement_ReturnsExpectedPositions()
        {
            // Arrange
            var planet = new Planet(2, 2);

            var firstActions = new Queue<MarsRover.Enums.Action>();
            firstActions.Enqueue(MarsRover.Enums.Action.MoveForward);

            var secondActions = new Queue<MarsRover.Enums.Action>();
            secondActions.Enqueue(MarsRover.Enums.Action.MoveForward);

            var rovers = new List<IRover>
            {
                new Rover(new Point(0, 0), MarsRover.Enums.Direction.North, firstActions),
                new Rover(new Point(1, 0), MarsRover.Enums.Direction.North, secondActions)
            };

            var simulation = new Simulation(planet, rovers);

            var expectedResults = new List<string>
            {
                "0 1 N",
                "1 1 N"
            };

            // Act
            var results = simulation.Run().ToList();

            // Assert
            CollectionAssert.AreEquivalent(expectedResults, results);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Run_WithTwoRoversAndConflictingPath_ThrowsException()
        {
            // Arrange
            var planet = new Planet(2, 2);

            var firstActions = new Queue<MarsRover.Enums.Action>();
            firstActions.Enqueue(MarsRover.Enums.Action.MoveForward);

            var rovers = new List<IRover>
            {
                new Rover(new Point(0, 0), MarsRover.Enums.Direction.East, firstActions),
                new Rover(new Point(1, 0), MarsRover.Enums.Direction.North)
            };

            var simulation = new Simulation(planet, rovers);

            // Act
            simulation.Run();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Run_WithTwoRoversAndSecondRoverConflictingPath_ThrowsException()
        {
            // Arrange
            var planet = new Planet(2, 2);

            var firstActions = new Queue<MarsRover.Enums.Action>();
            firstActions.Enqueue(MarsRover.Enums.Action.MoveForward);

            var secondActions = new Queue<MarsRover.Enums.Action>();
            secondActions.Enqueue(MarsRover.Enums.Action.MoveForward);

            var rovers = new List<IRover>
            {
                new Rover(new Point(0, 0), MarsRover.Enums.Direction.North, firstActions),
                new Rover(new Point(0, 1), MarsRover.Enums.Direction.West, secondActions)
            };

            var simulation = new Simulation(planet, rovers);

            // Act
            simulation.Run();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Run_WithOneRoverMovingOutOfBounds_ThrowsException()
        {
            // Arrange
            var planet = new Planet(2, 2);

            var firstActions = new Queue<MarsRover.Enums.Action>();
            firstActions.Enqueue(MarsRover.Enums.Action.MoveForward);
            firstActions.Enqueue(MarsRover.Enums.Action.MoveForward);

            var rovers = new List<IRover>
            {
                new Rover(new Point(0, 0), MarsRover.Enums.Direction.East, firstActions)
            };

            var simulation = new Simulation(planet, rovers);

            // Act
            simulation.Run();

            // Assert
            Assert.Fail();
        }

        [TestMethod]
        public void Run_WithOneRoverRotatingAndMoving_ReturnsExpectedPosition()
        {
            // Arrange
            var planet = new Planet(2, 2);

            var firstActions = new Queue<MarsRover.Enums.Action>();
            firstActions.Enqueue(MarsRover.Enums.Action.MoveForward);
            firstActions.Enqueue(MarsRover.Enums.Action.RotateRight);
            firstActions.Enqueue(MarsRover.Enums.Action.MoveForward);

            var rovers = new List<IRover>
            {
                new Rover(new Point(0, 0), MarsRover.Enums.Direction.North, firstActions),
            };

            var simulation = new Simulation(planet, rovers);

            var expectedResults = new List<string>
            {
                "1 1 E",
            };

            // Act
            var results = simulation.Run().ToList();

            // Assert
            CollectionAssert.AreEquivalent(expectedResults, results);
        }
    }
}
