using Moq;
using SearchFight.BL.Implementations;
using SearchFight.BL.Interfaces;
using SearchFight.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SearchFight.Test
{
    public class ResultManagerTests
    {
        private readonly List<SearchResult> totalResults;
        private readonly List<string> messages;
        public readonly Mock<ISearchEngine> _bingEngine;
        public readonly Mock<ISearchEngine> _googleEngine;

        //private Mock<IResultManager> _resultManager;

        public ResultManagerTests()
        {
            totalResults = new List<SearchResult>();
            messages = new List<string>();
            _bingEngine = new Mock<ISearchEngine>();
            _googleEngine = new Mock<ISearchEngine>();
            
        }

        [Fact]
        public void TotalWinners_Returns_Engine_Message()
        {
            // Arrange
            GenerateTestData();
            // Act
            new ResultManager().TotalWinners(totalResults, messages);
            // Assert
            Assert.Single(messages);
            Assert.True(messages.IndexOf("Total winner: java") > -1);
        }

        [Fact]
        public void EngineWinners_Returns_Engine_Message()
        {
            // Arrange
            GenerateTestData();
            _bingEngine.Setup(c => c.Engine).Returns("Bing");
            _googleEngine.Setup(c => c.Engine).Returns("Google");

            var searchEngines = new List<ISearchEngine> {
                _bingEngine.Object,
                _googleEngine.Object
            };

            var resultManager = new ResultManager();
            resultManager.SearchEngines = searchEngines;

            // Act
            resultManager.EngineWinners(totalResults, messages);
            // Assert
            Assert.Equal(2, messages.Count);
            Assert.True(messages.IndexOf("Bing winner: java") > -1);
            Assert.True(messages.IndexOf("Google winner: java script") > -1);
        }

        [Fact]
        public async Task SearchResults_Returns_Engine_Message()
        {
            // Arrange
            var bingResult = new SearchResult
            {
                Engine = "Bing",
                SearchCount = 5000,
                SearchQuery = "java"
            };

            var googleResult = new SearchResult
            {
                Engine = "Google",
                SearchCount = 8000,
                SearchQuery = "java"
            };

            _bingEngine.Setup(c => c.GetSearchResult(It.IsAny<string>()))
                .ReturnsAsync(bingResult);
            _bingEngine.Setup(c => c.Engine).Returns("Bing");
            _googleEngine.Setup(c => c.GetSearchResult(It.IsAny<string>()))
                .ReturnsAsync(googleResult);
            _googleEngine.Setup(c => c.Engine).Returns("Google");

            var queries = new List<string>
            {
                "java"
            };
            var searchEngines = new List<ISearchEngine> {
                _bingEngine.Object,
                _googleEngine.Object
            };

            var resultManager = new ResultManager();
            resultManager.SearchEngines = searchEngines;

            // Act
            await resultManager.SearchResults(queries, totalResults, messages);
            // Assert
            Assert.Equal(2, totalResults.Count);
            Assert.True(messages.IndexOf("java: Bing 5000 Google 8000") > -1);
        }

        private void GenerateTestData()
        {
            totalResults.Add(new SearchResult { Engine = "Google", SearchQuery = ".NET", SearchCount = 12000 });
            totalResults.Add(new SearchResult { Engine = "Bing", SearchQuery = ".NET", SearchCount = 11000 });
            totalResults.Add(new SearchResult { Engine = "Google", SearchQuery = "java", SearchCount = 14000 });
            totalResults.Add(new SearchResult { Engine = "Bing", SearchQuery = "java", SearchCount = 18000 });
            totalResults.Add(new SearchResult { Engine = "Google", SearchQuery = "java script", SearchCount = 16000 });
            totalResults.Add(new SearchResult { Engine = "Bing", SearchQuery = "java script", SearchCount = 15000 });
        }
    }
}
