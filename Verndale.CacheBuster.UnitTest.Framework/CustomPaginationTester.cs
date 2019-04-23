using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Verndale.CacheBuster.Services;

namespace Verndale.CacheBuster.UnitTest.Framework
{
    [TestClass]
    public class CustomPaginationTester
    {
        private IPagination<string> _pagination;

        [TestInitialize]
        public void Setup()
        {
            _pagination = new Pagination<string>(new[] { "First", "Second", "Third", "Fourth" }.AsQueryable(), 1, 2);
        }

        [TestMethod]
        public void Should_store_total_items()
        {
            Assert.AreEqual(4, _pagination.TotalItems);
        }

        [TestMethod]
        public void Should_store_page_number()
        {
            Assert.AreEqual(1, _pagination.PageNumber);
        }

        [TestMethod]
        public void Should_store_page_size()
        {
            Assert.AreEqual(2, _pagination.PageSize);
        }

        [TestMethod]
        public void Should_enumerate_over_items()
        {
            Assert.AreEqual(2, _pagination.Count());
            Assert.AreEqual("First", _pagination.First());
            Assert.AreEqual("Second", _pagination.Last());
        }

        [TestMethod]
        public void Should_calculate_total_pages()
        {
            Assert.AreEqual(2, _pagination.TotalPages);
        }

        [TestMethod]
        public void FirstItem_should_return_index_of_first_item_in_current_page()
        {
            Assert.AreEqual(1, _pagination.FirstItem);
        }

        [TestMethod]
        public void LastItem_should_return_index_of_last_item_in_current_page()
        {
            Assert.AreEqual(2, _pagination.LastItem);
        }

        [TestMethod]
        public void HasPreviousPage_should_return_true_when_not_on_first_page()
        {
            _pagination = new Pagination<string>(new[] { "First", "Second", "Third", "Fourth" }.AsQueryable(), 2, 2);
            Assert.AreEqual(true, _pagination.HasPreviousPage);
        }

        [TestMethod]
        public void HasPreviousPage_should_return_false_when_on_first_page()
        {
            Assert.AreEqual(false, _pagination.HasPreviousPage);
        }

        [TestMethod]
        public void HasNextPage_should_return_true_when_on_first_page()
        {
            Assert.AreEqual(true, _pagination.HasNextPage);
        }

        [TestMethod]
        public void HasNextPage_should_return_false_when_on_last_page()
        {
            _pagination = new Pagination<string>(new[] { "First", "Second", "Third", "Fourth" }.AsQueryable(), 2, 2);
            Assert.AreEqual(false, _pagination.HasNextPage);
        }
    }
}
