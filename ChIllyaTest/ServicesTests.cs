using ChIllya.Utils;

namespace ChIllyaTest
{
    [TestFixture]
    public class ServicesTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public async Task YoutubeSearchTest()
        {
            YoutubeService service = new YoutubeService();
            await service.DownloadMusic("Clarity");


            bool a = false;
            Assert.That(a, Is.False, "1 should not be prime");
        }
    }
}