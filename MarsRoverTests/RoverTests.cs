using System;
using System.Collections.Generic;
using MarsRover;
using MarsRover.Enums;
using MarsRover.Rovers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Action = MarsRover.Enums.Action;

namespace MarsRoverTests
{
    [TestClass]
    public class RoverTests
    {
        [TestMethod]
        public void GetNextMove_WithValidSingularAction_ReturnsAction()
        {
            // Arrange
            var actions = new Queue<Action>();
            actions.Enqueue(Action.MoveForward);

            var rover = new Rover(new Point(0, 0), Direction.East, actions);

            // Act
            // Assert
            Assert.AreEqual(Action.MoveForward, rover.GetNextMove());
        }

        [TestMethod]
        public void GetNextMove_WithValidMultipleActions_ReturnsFirstAction()
        {
            // Arrange
            var actions = new Queue<Action>();
            actions.Enqueue(Action.MoveForward);
            actions.Enqueue(Action.RotateLeft);

            var rover = new Rover(new Point(0, 0), Direction.East, actions);

            // Act
            // Assert
            Assert.AreEqual(Action.MoveForward, rover.GetNextMove());
        }

        [TestMethod]
        public void GetNextMove_WithNullActions_ReturnsNothingAction()
        {
            // Arrange
            var rover = new Rover(new Point(0, 0), Direction.North);

            // Act
            // Assert
            Assert.AreEqual(Action.Nothing, rover.GetNextMove());
        }

        [TestMethod]
        public void GetNextMove_WithEmptyActionsQueue_ReturnsNothingAction()
        {
            // Arrange
            var rover = new Rover(new Point(0, 0), Direction.East, new Queue<Action>());

            // Act
            // Assert
            Assert.AreEqual(Action.Nothing, rover.GetNextMove());
        }

        [TestMethod()]
        public void MoveForward_WithNorthDirection_MovesCoordinates()
        {
            // Arrange
            var rover = new Rover(new Point(0, 0), Direction.North);

            // Act
            rover.MoveForward();

            // Assert
            Assert.AreEqual(new Point(0, 1), rover.Coordinates);
            Assert.AreEqual(Direction.North, rover.Direction);
        }

        [TestMethod]
        public void MoveForward_WithEastDirection_MovesCoordinates()
        {
            // Arrange
            var rover = new Rover(new Point(0, 0), Direction.East);

            // Act
            rover.MoveForward();

            // Assert
            Assert.AreEqual(new Point(1, 0), rover.Coordinates);
            Assert.AreEqual(Direction.East, rover.Direction);
        }

        [TestMethod]
        public void MoveForward_WithWestDirection_MovesCoordinates()
        {
            // Arrange
            var rover = new Rover(new Point(1, 0), Direction.West);

            // Act
            rover.MoveForward();

            // Assert
            Assert.AreEqual(new Point(0, 0), rover.Coordinates);
            Assert.AreEqual(Direction.West, rover.Direction);
        }

        [TestMethod]
        public void MoveForward_WithSouthDirection_MovesCoordinates()
        {
            // Arrange
            var rover = new Rover(new Point(0, 1), Direction.South);

            // Act
            rover.MoveForward();

            // Assert
            Assert.AreEqual(new Point(0, 0), rover.Coordinates);
            Assert.AreEqual(Direction.South, rover.Direction);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MoveForward_WithSouthDirectionAtYLowerBoundary_ThrowsException()
        {
            // Arrange
            var rover = new Rover(new Point(0, 0), Direction.South);

            // Act
            rover.MoveForward();

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MoveForward_WithWestDirectionAtYLowerBoundary_ThrowsException()
        {
            // Arrange
            var rover = new Rover(new Point(0, 0), Direction.West);

            // Act
            rover.MoveForward();

            Assert.Fail();
        }

        [TestMethod]
        public void Rotate_WithRightAction_ChangesDirectionAccordingly()
        {
            // Arrange
            var rover = new Rover(new Point(0, 0), Direction.North);

            // Act
            rover.Rotate(Action.RotateRight);

            // Assert
            Assert.AreEqual(Direction.East, rover.Direction);
            Assert.AreEqual(new Point(0, 0), rover.Coordinates);
        }

        [TestMethod]
        public void Rotate_WithLeftAction_ChangesDirectionAccordingly()
        {
            // Arrange
            var rover = new Rover(new Point(0, 0), Direction.North);

            // Act
            rover.Rotate(Action.RotateLeft);

            // Assert
            Assert.AreEqual(Direction.West, rover.Direction);
            Assert.AreEqual(new Point(0, 0), rover.Coordinates);
        }

        [TestMethod]
        public void Rotate_WithRightActionAndEastDirection_ChangesDirectionAccordingly()
        {
            // Arrange
            var rover = new Rover(new Point(0, 0), Direction.East);

            // Act
            rover.Rotate(Action.RotateRight);

            // Assert
            Assert.AreEqual(Direction.South, rover.Direction);
            Assert.AreEqual(new Point(0, 0), rover.Coordinates);
        }

        [TestMethod]
        public void Rotate_WithLeftActionAndWestDirection_ChangesDirectionAccordingly()
        {
            // Arrange
            var rover = new Rover(new Point(0, 0), Direction.West);

            // Act
            rover.Rotate(Action.RotateLeft);

            // Assert
            Assert.AreEqual(Direction.South, rover.Direction);
            Assert.AreEqual(new Point(0, 0), rover.Coordinates);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Rotate_WithInvalidAction_ThrowsException()
        {
            // Arrange
            var rover = new Rover(new Point(0, 0), Direction.North);

            // Act
            rover.Rotate(Action.MoveForward);

            Assert.Fail();
        }

        [TestMethod]
        public void Move_WithMoveForwardAction_UpdatesCoordinates()
        {
            var actions = new Queue<Action>();
            actions.Enqueue(Action.MoveForward);

            var rover = new Rover(new Point(0, 0), Direction.North, actions);

            rover.Move();

            Assert.AreEqual(new Point(0, 1), rover.Coordinates);
            Assert.AreEqual(Direction.North, rover.Direction);
        }

        [TestMethod]
        public void Move_WithRotateRightAction_UpdatesDirections()
        {
            var actions = new Queue<Action>();
            actions.Enqueue(Action.RotateRight);

            var rover = new Rover(new Point(0, 0), Direction.North, actions);

            rover.Move();

            Assert.AreEqual(new Point(0, 0), rover.Coordinates);
            Assert.AreEqual(Direction.East, rover.Direction);
        }

        [TestMethod]
        public void Move_WithRotateLeftAction_UpdatesDirections()
        {
            var actions = new Queue<Action>();
            actions.Enqueue(Action.RotateLeft);

            var rover = new Rover(new Point(0, 0), Direction.North, actions);

            rover.Move();

            Assert.AreEqual(new Point(0, 0), rover.Coordinates);
            Assert.AreEqual(Direction.West, rover.Direction);
        }

        [TestMethod]
        public void Move_WithCombinationOfActions_UpdatesCoordinatesAndDirection()
        {
            var actions = new Queue<Action>();
            actions.Enqueue(Action.RotateRight);
            actions.Enqueue(Action.RotateRight);
            actions.Enqueue(Action.MoveForward);
            actions.Enqueue(Action.RotateLeft);
            
            var rover = new Rover(new Point(0, 1), Direction.North, actions);

            rover.Move();
            rover.Move();
            rover.Move();
            rover.Move();

            Assert.AreEqual(new Point(0, 0), rover.Coordinates);
            Assert.AreEqual(Direction.East, rover.Direction);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Move_WithNoActions_ThrowsException()
        {
            var rover = new Rover(new Point(0, 0), Direction.North, new Queue<Action>());

            rover.Move();
        }
    }
}