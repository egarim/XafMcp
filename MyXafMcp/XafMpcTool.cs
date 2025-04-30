using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using ModelContextProtocol.Server;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using XafMcp.Module.BusinessObjects;

namespace MyXafMcp
{
    [McpServerToolType]
    public static class XafMpcTool
    {
        public static XPObjectSpaceProvider osProvider;


        static XafMpcTool()
        {
            XpoTypesInfoHelper.GetXpoTypeInfoSource();
            XafTypesInfo.Instance.RegisterEntity(typeof(Customer));
            osProvider = new XPObjectSpaceProvider(
            @"XpoProvider=Postgres;Server=127.0.0.1;User ID=postgres;Password=1234567890;Database=XafMpc;Encoding=UNICODE", null);
            IObjectSpace objectSpace = osProvider.CreateObjectSpace();
        }
        [McpServerTool, Description("Takes as DevExpress criteria and returns a list of customer that match this criteria., the property names should be enclosed in  [] , for example [Name]")]
        public static string QueryCustomers(string Criteria)
        {

            var CriteriaObject = CriteriaOperator.Parse(Criteria);  
            var os = osProvider.CreateObjectSpace();
            Console.WriteLine($"QueryCustomers: {Criteria}");
            Debug.WriteLine($"QueryCustomers: {Criteria}");
            var customers = os.GetObjects<Customer>(CriteriaObject);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Customer customer in customers)
            {
                
                stringBuilder.AppendLine(customer.ToString());
            }



            return stringBuilder.ToString();
        }
        [McpServerTool, Description("Takes the name of an entity and retive a comma separated list of their properties")]
        public static string GetEntityProperties(string EntityName)
        {

          



            return "Name,Address,Active";
        }

        [McpServerTool, Description("Create a customer and returns its Oid")]
        public static string CreateCustomer(string Name,string Address)
        {

            var os = osProvider.CreateObjectSpace();
            var customer = os.CreateObject<Customer>();
            customer.Name = Name;
            customer.Address = Address;
            customer.Active = true;

            os.CommitChanges();


            return customer.Oid.ToString();
        }
    }
}
