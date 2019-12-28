using System;
using System.IO;
using NUnit.Framework;

namespace HH.TestUtils
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public class DeploymentItem : Attribute
    {
        public DeploymentItem(string fileProjectRelativePath)
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory + fileProjectRelativePath);
            var destination = Path.GetFileName(filePath);
            if (File.Exists(destination))
            {
                File.Delete(destination);
            }
            File.Copy(filePath, destination);
        }
    }
}
