using System;
using System.Collections.Generic;
using MarsRover;
using MarsRover.Enums;
using MarsRover.Rovers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Action = MarsRover.Enums.Action;

namespace MarsRoverTests
{
    /// <summary>
    /// Test class for testing the functionality of the <see cref="Rover"/> class
    /// </summary>
    [TestClass]
    public class RoverTests
    {
        /// <summary>
        /// Tests that a valid next move is returned when only one exists
        /// </summary>
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

        /// <summary>
        /// Tests that a valid next move is returned when more than one exists
        /// </summary>
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

        /// <summary>
        /// Tests that a Nothing value is returned when the Rover has no next move
        /// </summary>
        [TestMethod]
        public void GetNextMove_WithNullActions_ReturnsNothingAction()
        {
            // Arrange
            var rover = new Rover(new Point(0, 0), Direction.North);

            // Act
            // Assert
            Assert.AreEqual(Action.Nothing, rover.GetNextMove());
        }

        /// <summary>
        /// Tests that a Nothing value is returned when the Rover has no next move
        /// </summary>
        [TestMethod]
        public void GetNextMove_WithEmptyActionsQueue_ReturnsNothingAction()
        {
            // Arrange
            var rover = new Rover(new Point(0, 0), Direction.East, new Queue<Action>());

            // Act
            // Assert
            Assert.AreEqual(Action.Nothing, rover.GetNextMove());
        }

        /// <summary>
        /// Tests that moving forward when facing north will update the Y coordinate of the Rover
        /// </summary>
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

        /// <summary>
        /// Tests that moving forward when facing east will update the X coordinate of the Rover
        /// </summary>
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

        /// <summary>
        /// Tests that moving forward when facing west will update the X coordinates of the Rover
        /// </summary>
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

        /// <summary>
        /// Tests that moving forward when facing south will update the Y coordinates of the Rover
        /// </summary>
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

        /// <summary>
        /// Test that attempting to move the Rover below the Y boundary will throw an exception
        /// </summary>
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

        /// <summary>
        /// Tests that attempting to move the Rover below the X boundary will throw and exception
        /// </summary>
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

        /// <summary>
        /// Tests that rotating the Rover right will change the direction
        /// </summary>
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

        /// <summary>
        /// Tests that rotating the Rover left will change the direction
        /// </summary>
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

        /// <summary>
        /// Tests that rotating the Rover right with a different direction will change the direction
        /// </summary>
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

        /// <summary>
        /// Tests that rotating the Rover left with a different direction will change the direction
        /// </summary>
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

        /// <summary>
        /// Tests that rotating with an invalid action throws an exception
        /// </summary>
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

        /// <summary>
        /// Tests that moving the Rover forward updates the coordinates
        /// </summary>
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

        /// <summary>
        /// Tests that rotating the Rover updates the direction
        /// </summary>
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

        /// <summary>
        /// Tests that rotating the Rover updates the direction
        /// </summary>
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

        /// <summary>
        /// Tests that perfoming a series of actions updates the coordinates and direction as expected
        /// </summary>
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

        /// <summary>
        /// Tests that perfoming a movement with no next movement throws an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Move_WithNoActions_ThrowsException()
        {
            var rover = new Rover(new Point(0, 0), Direction.North, new Queue<Action>());

            rover.Move();
        }
    }
}