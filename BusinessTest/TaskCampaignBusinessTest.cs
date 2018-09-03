using System;
using System.Diagnostics;
using Mardis.Engine.Business.MardisCore;
using Mardis.Engine.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessTest
{

    [TestClass]
    public class TaskCampaignBusinessTest
    {
        private static readonly DbContextOptions<MardisContext> ContextOption =
            new DbContextOptionsBuilder<MardisContext>()
                .UseSqlServer(
                    @"Server=tcp:mardisenginetestbd.database.windows.net,1433;Initial Catalog=MardisEngine_Test;
                                Persist Security Info=False;User ID=mardisengine;Password=Mard!s3ngin3;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
                                MultipleActiveResultSets=True;App=EntityFramework;Connection Lifetime=120; Max Pool Size=100; Min Pool Size = 10; Pooling=True;")
                .Options;

        private readonly TaskCampaignBusiness t = new TaskCampaignBusiness(new MardisContext(ContextOption), new Mardis.Engine.Framework.RedisCache());

        [TestMethod]
        public void GetSectionsPollTest()
        {
            var myWarch = new Stopwatch();
            myWarch.Start();
            var n = t.GetSectionsPoll(Guid.Parse("D308AB3B-D3F2-4FB0-B163-0010584015BC"), Guid.Parse("3BBFAAF6-C41D-4161-056B-08D4805CE060"));
            myWarch.Stop();
            Assert.IsTrue(n != null);
        }

        [TestMethod]
        public void GetQuestionsFromSectionTest()
        {
            var myWarch = new Stopwatch();
            myWarch.Start();
            var n = t.GetQuestionsFromSection(Guid.Parse("084C1EDE-4813-41CF-BC4A-03040FEFA0FA"));
            myWarch.Stop();
            Assert.IsNotNull(n);
        }

        [TestMethod]
        public void GetSectiosnFromServiceTest()
        {
            var myWarch = new Stopwatch();
            myWarch.Start();
            var n = t.GetSectionsFromServiceGeo(Guid.Parse("2936EBAA-F8B3-4073-36E0-08D4805F4D91"), Guid.Parse("3BBFAAF6-C41D-4161-056B-08D4805CE060"));
            myWarch.Stop();
            Assert.IsNotNull(n);
            Assert.IsTrue(myWarch.ElapsedMilliseconds<=2000);
        }
    }
}
