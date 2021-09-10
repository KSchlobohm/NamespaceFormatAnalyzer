using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using VerifyCS = NamespaceFormatAnalyzer.Test.CSharpAnalyzerVerifier<
    NamespaceFormatAnalyzer.NamespaceFormatAnalyzerAnalyzer>;

namespace NamespaceFormatAnalyzer.Test
{
    [TestClass]
    public class NamespaceFormatAnalyzerUnitTest
    {
        //No diagnostics expected to show up
        [TestMethod]
        public async Task TestMethod1()
        {
            var test = @"";

            await VerifyCS.VerifyAnalyzerAsync(test);
        }

        //Diagnostic and CodeFix both triggered and checked for
        [TestMethod]
        public async Task TestMethod2()
        {
            var test = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    // namespace is not alphabetically sorted
    namespace {|#0:A.F.C.D|}
    {
        class TypeName
        {   
        }
    }";

            var expected = VerifyCS.Diagnostic("NamespaceFormatAnalyzer").WithLocation(0).WithArguments("A.F.C.D");
            await VerifyCS.VerifyAnalyzerAsync(test, expected);
        }
    }
}
