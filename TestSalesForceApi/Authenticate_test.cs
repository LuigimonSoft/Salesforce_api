using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TestSalesForceApi
{
    [TestClass]
    public class Authenticate_test
    {
        IConfiguration Configuration { get; set; }

        public Authenticate_test()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<Authenticate_test>();

            Configuration = builder.Build();
        }

        [TestMethod]
        public void ctr_Test()
        {
            sfAuthenticate.Authenticate result = new sfAuthenticate.Authenticate("","","","","","");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Login_FromPassword_Test()
        {
            sfAuthenticate.Authenticate authenticateInst = new sfAuthenticate.Authenticate(Configuration["Domain"], Configuration["ClientId"], Configuration["ClientSecret"], Configuration["Username"], Configuration["Password"],"");
            var result =  authenticateInst.getToken(sfAuthenticate.GrantTypes.password);
            Assert.IsTrue(result.Result.isSuccess);
            Assert.IsNotNull(result.Result.Item2);
            Assert.IsNotNull(result.Result.Item2.access_token);
        }

        [TestMethod]
        public void Login_From_Password_fail_Test()
        {
            sfAuthenticate.Authenticate authenticateInst = new sfAuthenticate.Authenticate(Configuration["Domain"], Configuration["ClientId"], Configuration["ClientSecret"], Configuration["Username"], Configuration["Password"]+"test", "");
            var result = authenticateInst.getToken(sfAuthenticate.GrantTypes.password);
            Assert.IsFalse(result.Result.isSuccess);
            Assert.IsNotNull(result.Result.Item2);
        }

        [TestMethod]
        public void Login_From_Password_fail_Connection_Test()
        {
            sfAuthenticate.Authenticate authenticateInst = new sfAuthenticate.Authenticate(Configuration["Domain"]+"/error/", Configuration["ClientId"], Configuration["ClientSecret"], Configuration["Username"], Configuration["Password"] + "test", "");
            var result = authenticateInst.getToken(sfAuthenticate.GrantTypes.password);
            Assert.IsFalse(result.Result.isSuccess);
            Assert.IsNull(result.Result.Item2);
        }
    }
}