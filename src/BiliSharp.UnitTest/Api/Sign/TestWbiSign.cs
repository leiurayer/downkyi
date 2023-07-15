using BiliSharp.Api.Sign;

namespace BiliSharp.UnitTest.Api.Sign
{
    public class TestWbiSign
    {
        [Fact]
        public void TestParametersToQuery_Default()
        {
            var parameters = new Dictionary<string, object>
            {
                { "fourk", 1 },
                { "fnver", 0 },
                { "fnval", 4048 },
                { "cid", 405595939 },
                { "qn", 120 },
                { "bvid", "BV1Sg411F7cb" },
                { "aid", 505421421 }
            };
            string expected = "fourk=1&fnver=0&fnval=4048&cid=405595939&qn=120&bvid=BV1Sg411F7cb&aid=505421421";

            string query = WbiSign.ParametersToQuery(parameters);
            Assert.Equal(expected, query);
        }

        [Fact]
        public void TestParametersToQuery_Empty()
        {
            var parameters = new Dictionary<string, object>();
            string query = WbiSign.ParametersToQuery(parameters);
            Assert.Equal("", query);
        }

        [Fact]
        public void TestKeys_Default()
        {
            var key1 = WbiSign.GetKey();
            Assert.NotNull(key1);

            string imgKey = "34478ba821254d9d93542680e3b86100";
            string subKey = "7e16a90d190a4355a78fd00b32a38de6";
            var keys = new Tuple<string, string>(imgKey, subKey);
            WbiSign.SetKey(keys);

            var key2 = WbiSign.GetKey();
            Assert.Equal(imgKey, key2.Item1);
            Assert.Equal(subKey, key2.Item2);
        }

        [Fact]
        public void TestEncodeWbi_Default()
        {
            var parameters = new Dictionary<string, object>
            {
                { "fourk", 1 },
                { "fnver", 0 },
                { "fnval", 4048 },
                { "cid", 405595939 },
                { "qn", 120 },
                { "bvid", "BV1Sg411F7cb" },
                { "aid", 505421421 }
            };

            var wbi = WbiSign.EncodeWbi(parameters);
            Assert.NotNull(wbi["w_rid"]);
        }

        [Fact]
        public void TestEncodeWbi_Default2()
        {
            var parameters = new Dictionary<string, object>
            {
                { "fourk", 1 },
                { "fnver", 0 },
                { "fnval", 4048 },
                { "cid", 405595939 },
                { "qn", 120 },
                { "bvid", "BV1Sg411F7cb" },
                { "aid", 505421421 }
            };

            var wbi = WbiSign.EncodeWbi(parameters, "653657f524a547ac981ded72ea172057", "6e4909c702f846728e64f6007736a338");
            Assert.NotNull(wbi["w_rid"]);
        }

    }
}