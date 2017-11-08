using System.Web.Mvc;

namespace CryptoManager.Areas.Api
{
    public class ApiAreaRegistration : AreaRegistration 
    {
        public override string AreaName
        {
            get
            {
                return "Api";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //Nada deve ser feito
        }
    }
}