using Moq;
using SearchFight.BL.Implementations;
using SearchFight.BL.Interfaces;
using SearchFight.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SearchFight.Test
{
    public class SearchProxyTests
    {
        public readonly Mock<ISearchEngine> _bingEngine;
        public readonly Mock<ISearchEngine> _googleEngine;
        public readonly Mock<ResultManager> _resultManager;

        public SearchProxyTests()
        {
            _bingEngine = new Mock<ISearchEngine>();
            _googleEngine = new Mock<ISearchEngine>();
            _resultManager = new Mock<ResultManager>();
        }

        [Fact]
        public async Task RunSearch_Returns_Valid_Message()
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

            var searchEngines = new List<ISearchEngine> {
                _bingEngine.Object,
                _googleEngine.Object
            };
            // Act
            var result = await new SearchProxy(searchEngines, _resultManager.Object).RunSearch(new List<string> { "java" });

            // Assert
            Assert.IsType<List<string>>(result);
            Assert.Equal(4, result.Count);
            Assert.True(result.IndexOf("java: Bing 5000 Google 8000") > -1);
            Assert.True(result.IndexOf("Bing winner: java") > -1);
            Assert.True(result.IndexOf("Google winner: java") > -1);
            Assert.True(result.IndexOf("Total winner: java") > -1);
        }
    }
}
