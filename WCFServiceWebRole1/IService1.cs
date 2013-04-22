using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Wa2.DaoClasses;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [WebInvoke(UriTemplate="/putStrings",RequestFormat=WebMessageFormat.Json,Method="PUT",ResponseFormat=WebMessageFormat.Json,BodyStyle=WebMessageBodyStyle.Bare)]
        String putJob(DiffRequest request);
        // TODO: Add your service operations here

        [WebGet(UriTemplate = "/getResult/{hash}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        DiffResult getResult(String hash);
    }



}
