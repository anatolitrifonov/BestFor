using BestFor.Services.Profanity;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BestFor.Services.Tests
{
    /// <summary>
    /// Unit tests for DefaultSuggestions object
    /// </summary>
    public class ServicesProfanityTests : BaseTest
    {
        //[Fact]
        //public void Thetest()
        //{
        //    Assert.True(ProfanityFilter.Match());
        //}

        [Fact]
        public void ProfanityFilter_Character17_NotAllowed()
        {
            Assert.True(!ProfanityFilter.AllChractersAllowed("\"(" + new string(new char[] { (char)17 }) + ")[\\|/"));
        }

        [Fact]
        public void ProfanityFilter_GraveAccent_Allowed()
        {
            Assert.True(ProfanityFilter.AllChractersAllowed("`"));
        }

        [Fact]
        public void ProfanityFilter_Exclamation_Allowed()
        {
            Assert.True(ProfanityFilter.AllChractersAllowed("\r\n!"));
        }

        [Fact]
        public void ProfanityFilter_Space_Allowed()
        {
            Assert.True(ProfanityFilter.AllChractersAllowed(" \r\n "));
        }

        [Fact]
        public void ProfanityFilter_DoubleQuote_Allowed()
        {
            Assert.True(ProfanityFilter.AllChractersAllowed("\"\r\n\""));
        }

        [Fact]
        public void ProfanityFilter_Tilda_Allowed()
        {
            Assert.True(ProfanityFilter.AllChractersAllowed("~\r\n~"));
        }

        [Fact]
        public void ProfanityFilter_AtSymbol_Allowed()
        {
            Assert.True(ProfanityFilter.AllChractersAllowed("@\r\n@"));
        }

        [Fact]
        public void ProfanityFilter_NumberSign_Allowed()
        {
            Assert.True(ProfanityFilter.AllChractersAllowed("#\r\n#"));
        }

        [Fact]
        public void ProfanityFilter_DollarSign_Allowed()
        {
            Assert.True(ProfanityFilter.AllChractersAllowed("$\r\n$"));
        }

        [Fact]
        public void ProfanityFilter_PercentSign_Allowed()
        {
            Assert.True(ProfanityFilter.AllChractersAllowed("%\r\n%"));
        }

        [Fact]
        public void ProfanityFilter_CircumflexAccent_Allowed()
        {
            Assert.True(ProfanityFilter.AllChractersAllowed("^\r\n^"));
        }

        [Fact]
        public void ProfanityFilter_Asterisk_Allowed()
        {
            Assert.True(ProfanityFilter.AllChractersAllowed("*\r\n*a*"));
        }

        [Fact]
        public void ProfanityFilter_Ampersand_Allowed()
        {
            Assert.True(ProfanityFilter.AllChractersAllowed("&\r\n&a*"));
        }

        [Fact]
        public void ProfanityFilter_A_Allowed()
        {
            Assert.True(ProfanityFilter.AllChractersAllowed("A"));
        }

        [Fact]
        public void ProfanityFilter_LeftParenthesis_Allowed()
        {
            Assert.True(ProfanityFilter.AllChractersAllowed("(\r\n("));
        }

        [Fact]
        public void ProfanityFilter_RightParenthesis_Allowed()
        {
            Assert.True(ProfanityFilter.AllChractersAllowed(")\r\n)"));
        }

        [Fact]
        public void ProfanityFilter_Minus_Allowed()
        {
            Assert.True(ProfanityFilter.AllChractersAllowed("-\r\n-"));
        }

        [Fact]
        public void ProfanityFilter_Underscore_Allowed()
        {
            Assert.True(ProfanityFilter.AllChractersAllowed("_\r\n_"));
        }

        [Fact]
        public void ProfanityFilter_LessThan_Allowed()
        {
            Assert.True(ProfanityFilter.AllChractersAllowed("<\r\n<"));
        }

        [Fact]
        public void ProfanityFilter_QuestionMark_Allowed()
        {
            Assert.True(ProfanityFilter.AllChractersAllowed("?\r\n?"));
        }

        [Fact]
        public void ProfanityFilter_All_Allowed()
        {
            // var result = ProfanityFilter.AllChractersAllowed("a");
            // result = ProfanityFilter.AllChractersAllowed("%");
            string data = "` ~ ! @ # $ % ^ & * ()- _+\"={}[];:'/ < > .` \\ | ~ ! @ # $ % ^ & * ( ) - _ + = { [ } ] ; : ' \" < , > . ? /";
            string tomatch = data + "\r\na\r\na" + data;
            var result = ProfanityFilter.AllChractersAllowed(tomatch);
            Assert.True(result);
        }
    }
}
