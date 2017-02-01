using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bling.Repository.Calyx;
using NUnit.Framework.SyntaxHelpers;
using Bling.Repository.Processing;
using Bling.Presenter;

namespace Bling.Tests.Repository.Calyx
{
    [TestFixture, Category("Database")]
    public class PointDaoTests
    {
        [Test]
        public void Test()
        {
            using (IPointDao dao = new PointDao())
            {
                string folderpath = new OrderAppraisalDao(StaticSessionManager.OpenSessionForDMDData())
                    .GetFolderPathInPoint("TESTAPPRAISER");

                //dao.OpenPExportFile(@"P:\Point Data Server\filesync\DataFolders\BakersfieldInProcess\BORROWER\TESTAPPRAISER.brw");
                dao.OpenPExportFile(folderpath);
                dao.UpdateField(330, "330 Test 3");
                dao.UpdateField(331, "331 Test 3");
            }

        }
    }
}
