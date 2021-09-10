using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace NamespaceFormatAnalyzer.Test
{
    [TestClass]
    public class NamespaceFormatValidatorTests
    {
        [TestMethod]
        public void ValidatorCanHandleNull()
        {
            var validator = new NamespaceFormatValidator();
            string validNamespace = null;

            var result = validator.IsValid(validNamespace);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidatorCanHandleEmptyString()
        {
            var validator = new NamespaceFormatValidator();
            string validNamespace = "";

            var result = validator.IsValid(validNamespace);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidatorCanHandleMalformedNamespace()
        {
            var validator = new NamespaceFormatValidator();
            var malformedNamespace = "A.B.";

            var result = validator.IsValid(malformedNamespace);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SingleNamespaceIsValid()
        {
            var validator = new NamespaceFormatValidator();
            var validNamespace = "A";

            var result = validator.IsValid(validNamespace);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FourNodeSequentialNamespaceIsValid()
        {
            var validator = new NamespaceFormatValidator();
            var validNamespace = "A.B.C.D";

            var result = validator.IsValid(validNamespace);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RandomNamespaceIsValid()
        {
            var validator = new NamespaceFormatValidator();
            var validNamespace = "A.F.C.D";

            var result = validator.IsValid(validNamespace);

            Assert.IsFalse(result);
        }
    }
}
