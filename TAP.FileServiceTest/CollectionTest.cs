using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TAP.FileService.Domain;

namespace TAP.FileServiceTest
{
    [TestClass]
    public class CollectionTest
    {
        private PropertyCollection ppy;

        [TestInitialize]
        public void init()
        {
            ppy = new PropertyCollection();
        }

        public void 获取集合()
        {
        }

        [TestMethod]
        public void 增加集合()
        {
            ppy.Add(new FileMetadataEAVProperty { FileId = "1", Name = "K1", Value = "V1" });
            //假设ppy[] 为正确
            var val = ppy["K1"];
            Assert.AreEqual("V1", val);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void 增加重复集合()
        {
            ppy.Add(new FileMetadataEAVProperty { FileId = "1", Name = "K1", Value = "V1" });
            ppy.Add(new FileMetadataEAVProperty { FileId = "1", Name = "K1", Value = "V1" });
        }

        [TestMethod]
        public void 检查集合是否个数()
        {
            ppy.Add(new FileMetadataEAVProperty { FileId = "1", Name = "K1", Value = "V1" });
            ppy.Add(new FileMetadataEAVProperty { FileId = "1", Name = "K2", Value = "V2" });
            Assert.AreEqual(2, ppy.Count);
        }

        [TestMethod]
        public void 清空集合()
        {
            ppy.Add(new FileMetadataEAVProperty { FileId = "1", Name = "K1", Value = "V1" });
            ppy.Add(new FileMetadataEAVProperty { FileId = "1", Name = "K2", Value = "V2" });
            ppy.Clear();
            Assert.AreEqual(0, ppy.Count);
        }

        [TestMethod]
        public void 复制集合()
        {
            ppy.Add(new FileMetadataEAVProperty { FileId = "1", Name = "K1", Value = "V1" });
            ppy.Add(new FileMetadataEAVProperty { FileId = "1", Name = "K2", Value = "V2" });
            FileMetadataEAVProperty[] ppy2 = new FileMetadataEAVProperty[2];
            ppy.CopyTo(ppy2, 0);
            Assert.AreEqual(2, ppy2.Length);
        }

        [TestMethod]
        public void 移除集合制定元素()
        {
            var p1 = new FileMetadataEAVProperty { FileId = "1", Name = "K1", Value = "V1" };
            ppy.Add(p1);
            ppy.Remove(p1);
            Assert.AreEqual(0, ppy.Count);
        }

        [TestMethod]
        public void 检查是否包含元素()
        {
            var p1 = new FileMetadataEAVProperty { FileId = "1", Name = "K1", Value = "V1" };
            ppy.Add(p1);
            Assert.IsTrue(ppy.Contains(p1));
        }

        [TestMethod]
        public void 测试枚举()
        {
            ppy.Add(new FileMetadataEAVProperty { FileId = "1", Name = "K1", Value = "V1" });
            ppy.Add(new FileMetadataEAVProperty { FileId = "1", Name = "K2", Value = "V2" });

            foreach (var p in ppy)
            {
                Assert.AreEqual("1", p.FileId);
            }
        }
    }
}