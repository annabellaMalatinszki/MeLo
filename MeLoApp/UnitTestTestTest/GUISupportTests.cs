using NUnit.Framework;
using WPFMediaPlayer;
namespace MeLoGUI.Test
{ 
    [TestFixture]
    public class GUISupportTest
    {
        [Test]
        public void ShouldAddTwoNumbers()
        {
            GUISupport sut = new GUISupport();
            int expectedResult = sut.Add(7, 8);
            Assert.That(expectedResult, Is.EqualTo(15));
        }

        [Test]
        public void ShouldMulTwoNumbers()
        {
            GUISupport sut = new GUISupport();
            int expectedResult = sut.Mul(7, 8);
            Assert.That(expectedResult, Is.EqualTo(56));
        }
    }
}