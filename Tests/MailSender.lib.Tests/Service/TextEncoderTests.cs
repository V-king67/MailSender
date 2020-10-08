using MailSender.lib.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailSender.lib.Tests.Service
{
    [TestClass]
    public class TextEncoderTests
    {
        [TestMethod]
        public void Encode_ABC_to_BCD_with_key_1()
        {
            // A-A-A = Arrange - Act - Assert

            #region Arrange
            const string str = "ABC";
            const int key = 1;
            const string expectedStr = "BCD";
            #endregion

            #region Act
            var actualStr = TextEncoder.Encode(str, key);
            #endregion

            #region Assert
            Assert.AreEqual(expectedStr, actualStr);
            #endregion
        }

        [TestMethod]
        public void Decode_BCD_to_ABC_with_key_1()
        {
            const string str = "BCD";
            const int key = 1;
            const string expectedStr = "ABC";

            var actualStr = TextEncoder.Decode(str, key);

            Assert.AreEqual(expectedStr, actualStr);
        }
    }
}
