using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTask.Tests
{
    [TestFixture]
    public class DataProcessorTests
    {
        Mock<IDataDAL> mockDataDAL;
        Mock<IDataInstantiator> mockDataInstantiator;
        Mock<ICustomLogger> mockLogger;
        Mock<IServiceBLL> mockService;
        Mock<IFileService> mockFileParser;

        [SetUp]
        public void SetUp()
        {
            mockDataDAL = new Mock<IDataDAL>();
            mockDataInstantiator = new Mock<IDataInstantiator>();
            mockLogger = new Mock<ICustomLogger>();
            mockService = new Mock<IServiceBLL>();
            mockFileParser = new Mock<IFileService>();
        }

        [Test]
        public void ProccessFile_SplitLineMethodCalledForEveryLineInFile()
        {
            // arrange
            string[] fileLines = new string[] { "1,sample Airport ", "2,sample Airport" };
            int count = default(int);
            mockFileParser.Setup(x => x.GetAllLinesFromFile(It.IsAny<string>()))
                          .Returns(fileLines);
            mockService.Setup(x => x.SplitLine(It.IsAny<string>())).Returns(new string[] { "1", "Airport"});

            var dataProcessor = new DataProcessor(mockDataDAL.Object, mockDataInstantiator.Object, mockService.Object,
                                                  mockLogger.Object, mockFileParser.Object);

            // act
            dataProcessor.ProccessFile(null, null, ref count);

            // assert
            mockService.Verify(x => x.SplitLine(It.IsAny<string>()), Times.Exactly(fileLines.Length));

        }

        [Test]
        public void ProccessFile_InvalidRow_LogRow()
        {
            // arrange
            string[] fileLines = new string[] { "1,sample Airport " };
            int count = default(int);
            mockFileParser.Setup(x => x.GetAllLinesFromFile(It.IsAny<string>()))
                          .Returns(fileLines);

            mockService.Setup(x => x.SplitLine(It.IsAny<string>()))
                       .Returns(new string[] { "1", "sample Airport" });

            mockService.Setup(x => x.IsValid(It.IsAny<string[]>(), It.IsAny<string>())).Returns(false);

            var dataProcessor = new DataProcessor(mockDataDAL.Object, mockDataInstantiator.Object, mockService.Object,
                                                  mockLogger.Object, mockFileParser.Object);
            // act
            dataProcessor.ProccessFile(null, null, ref count);

            // assert
            mockLogger.Verify(x => x.LogInfo(It.IsAny<string>()), Times.Once);

        }

        [Test]
        public void ProccessFile_InvalidData_RowsIgnoredIncrements()
        {
            // arrange
            string[] fileLines = new string[] { "1,sample Airport ", "1,sample Airport " };
            int rowsIgnored = 0;
            mockFileParser.Setup(x => x.GetAllLinesFromFile(It.IsAny<string>()))
                          .Returns(fileLines);

            mockService.Setup(x => x.SplitLine(It.IsAny<string>()))
                       .Returns(new string[] { "1", "sample Airport" });

            mockService.Setup(x => x.IsValid(It.IsAny<string[]>(), It.IsAny<string>()))
                                    .Returns(false);

            var dataProcessor = new DataProcessor(mockDataDAL.Object, mockDataInstantiator.Object, mockService.Object,
                                                  mockLogger.Object, mockFileParser.Object);

            // act
            dataProcessor.ProccessFile(null, null, ref rowsIgnored);

            // assert
            Assert.AreEqual(fileLines.Length, rowsIgnored);
        }

        //[Test]
        //public void ProccessFile_NoTimeZoneInfo_IsInvalidRow()
        //{
        //    // arrange
        //    string[] fileLines = new string[] { "1,sample Airport ", "1,sample Airport " };
        //    int rowsIgnored = 0;
        //    mockFileParser.Setup(x => x.GetAllLinesFromFile(It.IsAny<string>()))
        //                  .Returns(fileLines);

        //    mockService.Setup(x => x.SplitLine(It.IsAny<string>()))
        //               .Returns(new string[] { "1", "sample Airport" });

        //    mock.Setup(x => x.GetAirportsTimeZone(null, 0)).Returns((string)null);

        //    var dataProcessor = new DataProcessor(mockDataDAL.Object,mockDataInstantiator.Object, mockService.Object,
        //                                          mockLogger.Object, mockFileParser.Object);
        //    // act
        //    dataProcessor.ProccessFile(null, null, ref rowsIgnored);

        //    // assert
        //    mockService.Verify(x => x.IsValid(It.IsAny<string[]>(), null));
        //}

        [Test]
        public void ProccessFile_ExceptionThrown_LogErrorMessage()
        {
            // arrange
            string[] fileLines = new string[] { "1,sample Airport "};

            int rowsIgnored = 0;
            mockFileParser.Setup(x => x.GetAllLinesFromFile(It.IsAny<string>()))
                          .Returns(fileLines);

            mockService.Setup(x => x.SplitLine(It.IsAny<string>()))
                       .Throws<NullReferenceException>();


            var dataProcessor = new DataProcessor(mockDataDAL.Object, mockDataInstantiator.Object, mockService.Object,
                                                  mockLogger.Object, mockFileParser.Object);

            // act
            dataProcessor.ProccessFile(null, null, ref rowsIgnored);

            // assert
            mockLogger.Verify(x => x.LogError(It.IsAny<string>()), Times.Once);
            
        }
    }
}
