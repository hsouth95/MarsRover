﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRover;
using System.Collections.Generic;
using System.Collections;
using MarsRover.Enums;
using MarsRover.Planets;
using MarsRover.Rovers;

namespace MarsRoverTests
{
    /// <summary>
    /// Test class to test the functionality of the <see cref="Planet"/> class
    /// </summary>
    [TestClass]
    public class PlanetTests
    {
        /// <summary>
        /// Tests that a valid grid is build if one coordinate is provided for the Planet
        /// </summary>
        [TestMethod]
        public void GetGrid_OneCoordinate_BuildsGrid()
        {
            // Arrange
            var planet = new Planet(1, 1);

            var expectedGrid = new Dictionary<Point, bool>
            {
                { new Point(0, 0), false }
            };

            // Act
            // Assert
            CollectionAssert.AreEquivalent(expectedGrid, (ICollection)planet.Grid);
        }

        /// <summary>
        /// Tests that a valid grid is built with correct Points as keys for the Planet
        /// </summary>
        [TestMethod]
        public void GetGrid_LazyLoaded_BuildsGrid()
        {
            // Arrange
            var planet = new Planet(2, 2);

            var expectedGrid = new Dictionary<Point, bool>
            {
                { new Point(0, 0), false },
                { new Point(0, 1), false },
                { new Point(1, 0), false },
                { new Point(1, 1), false }
            };

            // Act
            // Assert
            CollectionAssert.AreEquivalent(expectedGrid, (ICollection)planet.Grid);
        }

        /// <summary>
        /// Tests that the key accessor works on the grid after being built
        /// </summary>
        [TestMethod]
        public void GetGrid_WithBuiltGrid_AllowsKeyAccess()
        {
            // Arrange
            var planet = new Planet(2, 2);

            // Fake value to trigger lazy loading
            var tempValue = planet.Grid;

            // Act
            // Assert            
            Assert.IsFalse(planet.Grid[new Point(0, 0)]);
        }

        /// <summary>
        /// Tests that a null value is returned if a Planet has a height and a width of zero
        /// </summary>
        [TestMethod]
        public void GetGrid_ZeroHeightAndWidthPlanet_ReturnsNull()
        {
            // Arrange
            var planet = new Planet(0, 0);

            // Act
            // Assert
            Assert.IsNull(planet.Grid);
        }

        /// <summary>
        /// Tests that a null value is returned if a Planet has a height of zero, with a valid width
        /// </summary>
        [TestMethod]
        public void GetGrid_ZeroHeight_ReturnsNull()
        {
            // Arrange
            var planet = new Planet(0, 1);

            // Act
            // Assert
            Assert.IsNull(planet.Grid);
        }

        /// <summary>
        /// Tests that a null value is returned if a Planet has a width of zero, with a valid height
        /// </summary>
        [TestMethod]
        public void GetGrid_ZeroWidth_ReturnsNull()
        {
            // Arrange
            var planet = new Planet(1, 0);

            // Act
            // Assert
            Assert.IsNull(planet.Grid);
        }

        /// <summary>
        /// Tests that a null value will cause the BuildPlanet method to throw an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuildPlanet_WithNullRovers_ThrowsException()
        {
            // Arrange
            var planet = new Planet(1, 1);

            // Act
            planet.BuildPlanet(null);
        }

        /// <summary>
        /// Tests that an empty collection passed into the BuildPlanet method will not cause any changes to the grid
        /// </summary>
        [TestMethod]
        public void BuildPlanet_WithEmptyRovers_DoesNotSetGridTrue()
        {
            // Arrange
            var planet = new Planet(2, 2);

            var expectedGrid = new Dictionary<Point, bool>
            {
                { new Point(0, 0), false },
                { new Point(0, 1), false },
                { new Point(1, 0), false },
                { new Point(1, 1), false }
            };

            // Act
            planet.BuildPlanet(new List<IRover>());

            // Assert
            CollectionAssert.AreEquivalent(expectedGrid, (ICollection)planet.Grid);
        }

        /// <summary>
        /// Tests that a Rover with an invalid coordinate causes an Exception to be thrown
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BuildPlanet_WithInvalidRoverCoordinates_ThrowsException()
        {
            // Arrange
            var planet = new Planet(2, 2);

            var rovers = new List<IRover>
            {
                new Rover(new Point(3, 3), Direction.North)
            };

            // Act
            planet.BuildPlanet(rovers);

            Assert.Fail();
        }

        /// <summary>
        /// Tests that two Rovers at the same coordinates causes an Exception to be thrown
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void BuildPlanet_WithDuplicateRoverCoordinates_ThrowsException()
        {
            // Arrange
            var planet = new Planet(2, 2);

            var rovers = new List<IRover>
            {
                new Rover(new Point(0, 1), Direction.North),
                new Rover(new Point(0, 1), Direction.North)
            };

            // Act
            planet.BuildPlanet(rovers);

            Assert.Fail();
        }

        /// <summary>
        /// Tests that the valid grid is built when a valid collection of Rovers is input
        /// </summary>
        [TestMethod]
        public void BuildPlanet_WithValidCoordinates_ReturnsValidGrid()
        {
            var planet = new Planet(2, 2);

            var rovers = new List<IRover>
            {
                new Rover(new Point(0, 0), Direction.North),
                new Rover(new Point(1, 1), Direction.North)
            };

            var expectedGrid = new Dictionary<Point, bool>
            {
                { new Point(0, 0), true },
                { new Point(0, 1), false },
                { new Point(1, 0), false },
                { new Point(1, 1), true }
            };

            // Act
            planet.BuildPlanet(rovers);

            CollectionAssert.AreEquivalent(expectedGrid, (ICollection)planet.Grid);
        }

        /// <summary>
        /// Tests that the valid grid is built when a height that is different to the width is used
        /// </summary>
        [TestMethod]
        public void BuildPlanet_WithDifferentDimensions_ReturnsValidGrid()
        {
            var planet = new Planet(1, 2);

            var rovers = new List<IRover>
            {
                new Rover(new Point(0, 0), Direction.North),
                new Rover(new Point(1, 0), Direction.North)
            };

            var expectedGrid = new Dictionary<Point, bool>
            {
                { new Point(0, 0), true },
                { new Point(1, 0), true }
            };

            // Act
            planet.BuildPlanet(rovers);

            CollectionAssert.AreEquivalent(expectedGrid, (ICollection)planet.Grid);
        }

        /// <summary>
        /// Tests that attempting to update the Planet's grid with a null Rover value will throw an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateGridPosition_WithNullRover_ThrowsException()
        {
            // Arrange
            var planet = new Planet(1, 1);

            // Act
            planet.UpdateGridPosition(null, new Point(0, 0));

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// Tests that attempting to update the Planet's grid with null coordinates throws an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateGridPosition_WithNullPreviousCoordinates_ThrowsException()
        {
            // Arrange
            var planet = new Planet(1, 1);
            var rover = new Rover(new Point(0, 0), Direction.North);

            // Act
            planet.UpdateGridPosition(rover, null);

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// Tests that updating the Planet's grid with no changes will cause nothing to occur
        /// </summary>
        [TestMethod]
        public void UpdateGridPosition_WithNoChanges_DoesNothing()
        {
            // Arrange
            var planet = new Planet(1, 1);
            var rover = new Rover(new Point(0, 0), Direction.North);

            planet.BuildPlanet(new List<IRover> { rover });

            var expectedGrid = new Dictionary<Point, bool>
            {
                { new Point(0, 0), true }
            };

            // Act
            planet.UpdateGridPosition(rover, new Point(0, 0));

            // Assert
            CollectionAssert.AreEquivalent(expectedGrid, (ICollection)planet.Grid);
        }

        /// <summary>
        /// Tests that attempting to update the Planet's grid with an area out of bounds will throw an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateGridPosition_WithCoordinatesOutOfPlanetGrid_ThrowsException()
        {
            // Arrange
            var planet = new Planet(1, 1);
            var rover = new Rover(new Point(0, 0), Direction.North);

            planet.BuildPlanet(new List<IRover> { rover });

            rover.Coordinates.Y = 1;

            // Act
            planet.UpdateGridPosition(rover, new Point(0, 0));

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// Tests that attempting to update the Planet's grid with an area where a Rover exists will throw an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UpdateGridPosition_WithConflictingPosition_ThrowsException()
        {
            // Arrange
            var planet = new Planet(2, 2);
            var rovers = new List<IRover> {
                new Rover(new Point(0, 0), Direction.North)
            };

            planet.BuildPlanet(rovers);

            // This rover will try to move into another rover's area
            var movedRover = new Rover(new Point(0, 0), Direction.North);

            // Act
            planet.UpdateGridPosition(movedRover, new Point(0, 1));

            // Assert
            Assert.Fail();
        }

        /// <summary>
        /// Tests that updating the Planet's grid with valid values will update the grid as expected
        /// </summary>
        [TestMethod]
        public void UpdateGridPosition_WithValidValues_UpdatesGrid()
        {
            // Arrange
            var planet = new Planet(2, 2);
            var rover = new Rover(new Point(0, 0), Direction.North);

            planet.BuildPlanet(new List<IRover> { rover });

            var expectedGrid = new Dictionary<Point, bool>
            {
                { new Point(0, 0), false },
                { new Point(0, 1), false },
                { new Point(1, 0), true },
                { new Point(1, 1), false }
            };

            var currentCoordinates = rover.Coordinates;

            rover.Coordinates = new Point(1, 0);

            // Act
            planet.UpdateGridPosition(rover, currentCoordinates);

            // Assert
            CollectionAssert.AreEquivalent(expectedGrid, (ICollection)planet.Grid);
        }
    }
}
